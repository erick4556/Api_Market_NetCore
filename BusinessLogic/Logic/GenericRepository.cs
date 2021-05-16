﻿using BusinessLogic.Data;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ClaseBase
    {

        private readonly MarketDbContext _context;

        public GenericRepository(MarketDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> getAllAsync()
        {
           return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> getByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
