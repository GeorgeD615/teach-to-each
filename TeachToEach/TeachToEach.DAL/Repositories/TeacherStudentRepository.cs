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
    public class TeacherStudentRepository : IBaseRepository<TeacherStudent>
    {
        private readonly AppDbContext _db;

        public TeacherStudentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(TeacherStudent entity)
        {
            await _db.TeacherStudentRelation.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(TeacherStudent entity)
        {
            _db.TeacherStudentRelation.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(TeacherStudent entity)
        {
            _db.TeacherStudentRelation.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<TeacherStudent> Get(int id)
        {
            return await _db.TeacherStudentRelation.FirstOrDefaultAsync(u => u.id == id);
        }

        public IQueryable<TeacherStudent> GetAll()
        {
            return _db.TeacherStudentRelation;
        }
    }
}
