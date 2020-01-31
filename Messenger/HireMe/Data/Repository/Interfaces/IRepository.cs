using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HireMe.Data.Repository.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        TEntity GetById(int id);

        TEntity GetByIdString(string id);

        Task<TEntity> GetByIdAsync(int id);

        void Update(TEntity entity);

        Task<int> SaveChangesAsync();


        int SaveChanges();


    }
}
