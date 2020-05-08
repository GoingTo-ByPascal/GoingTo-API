﻿using GoingTo_API.Domain.Models;
using GoingTo_API.Domain.Persistence.Context;
using GoingTo_API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoingTo_API.Persistence.Repositories
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(AppDbContext context ): base(context) { }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await _context.Cities.Include(p=>p.Country).Include(p=>p.Locatable).ToListAsync();
        }
        public async Task<City> FindById(int id)
        {
            return await _context.Cities.FindAsync(id);
        }
    }
}