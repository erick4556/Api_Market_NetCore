using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Errors
{
    public class CodeErrorResponse
    {

        public CodeErrorResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? getDefaultMessageStatusCode(statusCode); //En caso que sea null, muestre un mensaje por defecto
        }

        private string getDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "La petición enviada tiene errores",
                401 => "No tienes autorización para este recurso",
                404 => "No se encontró el item buscado",
                500 => "Se producieron errores en el servidor",
                _ => null //En caso que no de más detalle, lance un error en null
            };
        }
        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
