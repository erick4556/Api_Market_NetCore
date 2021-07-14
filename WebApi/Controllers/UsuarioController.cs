using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class UsuarioController : BaseApiController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Usuario> _passwordHasher;
        private readonly IGenericSeguridadRepository<Usuario> _seguridadRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ITokenService tokenService, IMapper mapper, IPasswordHasher<Usuario> passwordHasher, IGenericSeguridadRepository<Usuario> seguridadRepository, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _seguridadRepository = seguridadRepository;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _userManager.FindByEmailAsync(loginDto.Email); //_userManager tiene acceso a todas las entidades de seguridad de la aplicacion roles, usuarios, tokens, etc
            if (User == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, loginDto.Password, false); //false para que no bloquee la cuenta si el password es incorrecto

            if (!resultado.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var roles = await _userManager.GetRolesAsync(usuario);

            return new UsuarioDto
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Username = usuario.UserName,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Imagen = usuario.Imagen,
                Token = _tokenService.createToken(usuario, roles),
                Admin = roles.Contains("ADMIN") ? true : false //Si contiene el rol ADMIN devuelve un true sino un false
            };

        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioDto>> Registrar(RegistrarDto registrarDto)
        {
            var usuario = new Usuario
            {
                Email = registrarDto.Email,
                UserName = registrarDto.Username,
                Nombre = registrarDto.Nombre,
                Apellido = registrarDto.Apellido
            };

            var resultado = await _userManager.CreateAsync(usuario, registrarDto.Password);

            if (!resultado.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400));
            }

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.UserName,
                Token = _tokenService.createToken(usuario, null),
                Admin = false
            };

        }


        [Authorize] //Solos los usuarios registrados van a tener acceso a este endpoint
        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult<UsuarioDto>> Actualizar(string id, RegistrarDto registrarDto)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe"));
            }

            usuario.Nombre = registrarDto.Nombre;
            usuario.Apellido = registrarDto.Apellido;
            usuario.Imagen = registrarDto.Imagen;

            if (!string.IsNullOrEmpty(registrarDto.Password))
            {
                usuario.PasswordHash = _passwordHasher.HashPassword(usuario, registrarDto.Password); //Para cifrar la contraseña que es enviada
            }

            var resultado = await _userManager.UpdateAsync(usuario);

            if (resultado.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(usuario);

                return new UsuarioDto
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    Username = usuario.Email,
                    Imagen = usuario.Imagen,
                    Token = _tokenService.createToken(usuario, roles),
                    Admin = roles.Contains("ADMIN") ? true : false
                };
            }
            else
            {
                return BadRequest(new CodeErrorResponse(400, "No se pudo actualizar el usuario"));
            }

        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("pagination")]
        public async Task<ActionResult<Pagination<UsuarioDto>>> GetUsuarios([FromQuery] UsuarioSpecificationParams usuarioParams)
        {
            var spec = new UsuarioSpecification(usuarioParams);
            var usuarios = await _seguridadRepository.getAllWithSpec(spec); //Obtener los usuarios

            var specCount = new UsuarioForCountingSpecification(usuarioParams);
            var totalUsuarios = await _seguridadRepository.countAsync(specCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalUsuarios) / Convert.ToDecimal(usuarioParams.PageSize)); //Para redondear
            var totalPages = Convert.ToInt32(rounded);

            var data = _mapper.Map<IReadOnlyList<Usuario>, IReadOnlyList<UsuarioDto>>(usuarios);

            return Ok(
                    new Pagination<UsuarioDto>
                    {
                        Count = totalUsuarios,
                        Data = data,
                        PageCount = totalPages,
                        PageIndex = usuarioParams.PageIndex,
                        PageSize = usuarioParams.PageSize
                    }
                );

        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("role/{id}")]
        public async Task<ActionResult<UsuarioDto>> UpdateRole(string id, RoleDto roleParams)
        {
            var role = await _roleManager.FindByNameAsync(roleParams.Nombre);
            if (role == null)
            {
                return NotFound(new CodeErrorResponse(404, "El rol no existe"));
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe"));
            }

            var usuarioDto = _mapper.Map<Usuario, UsuarioDto>(usuario);
            if (roleParams.Status)
            {
                var resultado = await _userManager.AddToRoleAsync(usuario, roleParams.Nombre);
                if (resultado.Succeeded)
                {
                    usuarioDto.Admin = true;
                }

                if (resultado.Errors.Any())
                {
                    if (resultado.Errors.Where(x => x.Code == "UserAlreadyInRole").Any())
                    {
                        usuarioDto.Admin = true;
                    }
                }
            }
            else
            {
                var resultado = await _userManager.RemoveFromRoleAsync(usuario, roleParams.Nombre);
                if (resultado.Succeeded)
                {
                    usuarioDto.Admin = false;
                }
            }


            if (usuarioDto.Admin)
            {
                var roles = new List<string>();
                roles.Add("ADMIN");
                usuarioDto.Token = _tokenService.createToken(usuario, roles);
            }
            else
            {
                usuarioDto.Token = _tokenService.createToken(usuario, null);
            }

            return usuarioDto;

        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("account/{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioBy(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe"));
            }

            var roles = await _userManager.GetRolesAsync(usuario);

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.UserName,
                Imagen = usuario.Imagen,
                Admin = roles.Contains("ADMIN") ? true : false
            };

        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetUsuario()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value; //Para obtener el email desde el token

            //var usuario = await _userManager.FindByEmailAsync(email);

            var usuario = await _userManager.SearchUser(HttpContext.User);

            var roles = await _userManager.GetRolesAsync(usuario);

            return new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.UserName,
                Imagen = usuario.Imagen,
                Admin = roles.Contains("ADMIN") ? true : false,
                Token = _tokenService.createToken(usuario, roles)
            };
        }

        [HttpGet("emailvalido")]
        public async Task<ActionResult<bool>> ValidarEmail([FromQuery] string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);

            if (usuario == null) return false;

            return true;
        }

        [Authorize]
        [HttpGet("direccion")]
        public async Task<ActionResult<DireccionDto>> GetDireccion()
        {
            var usuario = await _userManager.SearchUserWithLocation(HttpContext.User);

            return _mapper.Map<Direccion, DireccionDto>(usuario.Direccion); //Objeto de tipo DireccionDto

        }

        [Authorize]
        [HttpPut("direccion")]
        public async Task<ActionResult<DireccionDto>> UpdateDireccion(DireccionDto direccion)
        {
            var usuario = await _userManager.SearchUserWithLocation(HttpContext.User);

            usuario.Direccion = _mapper.Map<DireccionDto, Direccion>(direccion); //Objeto de tipo Direccion

            var resultado = await _userManager.UpdateAsync(usuario);

            if (resultado.Succeeded) return Ok(_mapper.Map<Direccion, DireccionDto>(usuario.Direccion));

            return BadRequest("No se puedo actualizar la dirección del usuario");

        }

    }
}
