using Domain.Entities.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Generic
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> ReadAllAsync();
        Task<TEntity?> ReadByIdAsync(int id);
        Task<bool> InsertAsync(TEntity obj);
        Task<bool> UpdateAsync(TEntity obj);
        Task<bool> DeleteAsync(int id);
    }
}
