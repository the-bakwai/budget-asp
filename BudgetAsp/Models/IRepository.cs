using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetAsp.Models
{
    public interface IRepository<TEntity> where TEntity: class 
    {
        Task<TEntity> Get(long id);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAllActiveOnly();
        Task Save(TEntity entity);
        Task Delete(long id);
    }
}