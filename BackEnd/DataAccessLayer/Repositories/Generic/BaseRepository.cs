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
        private readonly DbSet<TEntity> table;

        public BaseRepository(PortfolioContext context)
        {
            this._context = context;
            table = _context.Set<TEntity>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            TEntity? existing = table.Find(id);
            if (existing != null)
            {
                table.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            else
                return false;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public async Task<bool> InsertAsync(TEntity entity)
        {
            table.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
