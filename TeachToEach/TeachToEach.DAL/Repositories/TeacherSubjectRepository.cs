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
    public class TeacherSubjectRepository : IBaseRepository<TeacherSubject>
    {
        private readonly AppDbContext _db;

        public TeacherSubjectRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(TeacherSubject entity)
        {
            await _db.TeacherSubjects.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(TeacherSubject entity)
        {
            _db.TeacherSubjects.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(TeacherSubject entity)
        {
            _db.TeacherSubjects.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<TeacherSubject> Get(int id)
        {
            return await _db.TeacherSubjects.FirstOrDefaultAsync(r => r.id == id);
        }

        public IQueryable<TeacherSubject> GetAll()
        {
             return _db.TeacherSubjects;
        }
    }
}
