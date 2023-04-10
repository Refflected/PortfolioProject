using Domain.Entities.Generic;
using Domain.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Generic
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly PortfolioContext _context;
        private readonly DbSet<TEntity> _table;

        public BaseRepository(PortfolioContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> ReadAllAsync()
        {
            return await _table.ToListAsync();
        }

        public async Task<TEntity?> ReadByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            TEntity? existing = _table.Find(id);
            if (existing != null)
            {
                _table.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }
    }
}
