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
    public class SubjectRepository : IBaseRepository<Subject>
    {
        private readonly AppDbContext _db;

        public SubjectRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Subject entity)
        {
            await _db.Subjects.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Subject entity)
        {
            _db.Subjects.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Subject entity)
        {
            _db.Subjects.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Subject> Get(int id)
        {
            return await _db.Subjects.FirstOrDefaultAsync(u => u.id == id);
        }

        public IQueryable<Subject> GetAll()
        {
            return _db.Subjects;
        }
    }
}
