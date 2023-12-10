using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachToEach.DAL.Interfaces;
using TeachToEach.Domain.Entity;

namespace TeachToEach.DAL.Repositories
{
    public class HomeworkRepository : IBaseRepository<Homework>
    {
        private readonly AppDbContext _db;

        public HomeworkRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Homework entity)
        {
            await _db.Homeworks.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Homework entity)
        {
            _db.Homeworks.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Homework entity)
        {
            _db.Homeworks.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Homework> Get(int id)
        {
            return await _db.Homeworks.FirstOrDefaultAsync(r => r.id == id);
        }

        public IQueryable<Homework> GetAll()
        {
            return _db.Homeworks;
        }
    }
}
