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
    public class UserRepository : IBaseRepository<User>
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<User> Get(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task<User> GetByLogin(string login)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.login == login);
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }
    }
}
