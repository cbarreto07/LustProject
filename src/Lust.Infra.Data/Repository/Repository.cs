using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lust.Domain.Interfaces;
using Lust.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Lust.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly LustContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(LustContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        
        
        public virtual Task<TEntity> GetByIdAsync(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return null;

            return DbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

       

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void Remove(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

       

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Db.SaveChangesAsync(cancellationToken);
        }

        protected string StringParaLike(string s)
        {
            return "%" + s.Replace("[", "[[]").Replace("%", "[%]") + "%";
        }


    }
}
