using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lust.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(Guid id);
        void Remove(TEntity obj);

        Task<TEntity> GetByIdAsync(Guid? id);
        IQueryable<TEntity> GetAll();
                
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken =  default(CancellationToken));
    }
}
