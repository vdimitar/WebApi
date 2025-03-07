using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.IServices;

namespace WebApi.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                return await _dbSet
                    .Where(e => EF.Property<bool>(e, "IsDeleted") == false)
                    .ToListAsync();
            }
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) 
                return false;

            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
                _context.Entry(entity).Property("IsDeleted").IsModified = true;
            }

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
