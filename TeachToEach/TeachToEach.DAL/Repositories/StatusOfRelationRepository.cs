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
    public class StatusOfRelationRepository : IBaseRepository<StatusOfRelation>
    {
        private readonly AppDbContext _db;

        public StatusOfRelationRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(StatusOfRelation entity)
        {
            await _db.StatusOfRelations.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(StatusOfRelation entity)
        {
            _db.StatusOfRelations.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Edit(StatusOfRelation entity)
        {
            _db.StatusOfRelations.Update(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<StatusOfRelation> Get(int id)
        {
            return await _db.StatusOfRelations.FirstOrDefaultAsync(r => r.id == id);
        }

        public IQueryable<StatusOfRelation> GetAll()
        {
            return _db.StatusOfRelations;
        }
    }
}
