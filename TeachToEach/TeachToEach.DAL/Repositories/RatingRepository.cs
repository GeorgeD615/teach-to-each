using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.DAL.Interfaces;
using TeachToEach.Domain.Entity;

namespace TeachToEach.DAL.Repositories
{
    public class RatingRepository : IBaseRepository<Rating>
    {
        private readonly AppDbContext _db;

        public RatingRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Rating entity)
        {
            await _db.Ratings.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Rating entity)
        {
            _db.Ratings.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Rating entity)
        {
            _db.Ratings.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Rating> Get(int id)
        {
            return await _db.Ratings.FirstOrDefaultAsync(r => r.id == id);
        }

        public IQueryable<Rating> GetAll()
        {
            return _db.Ratings;
        }
    }
}
