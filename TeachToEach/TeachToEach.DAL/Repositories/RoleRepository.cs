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
    public class RoleRepository : IBaseRepository<Role>
    {
        private readonly AppDbContext _db;

        public RoleRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Role entity)
        {
            await _db.Roles.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Role entity)
        {
            _db.Roles.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(Role entity)
        {
            _db.Roles.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Role> Get(int id)
        {
            return await _db.Roles.FirstOrDefaultAsync(u => u.id == id);
        }

        public IQueryable<Role> GetAll()
        {
            return _db.Roles;
        }
    }
}
