namespace HireMe.Data.Repository
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using HireMe.Data.Repository.Interfaces;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    public class DbBaseRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly FeaturesDbContext context;
        protected DbSet<TEntity> dbSet;

        public DbBaseRepository(FeaturesDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.dbSet = this.context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) => await this.dbSet.AddAsync(entity);

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }
        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }
        public void Update(TEntity entity)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
        public TEntity GetById(int id) => this.context.Set<TEntity>().Find(id);
        public TEntity GetByIdString(string id) => this.context.Set<TEntity>().Find(id);
        public async Task<TEntity> GetByIdAsync(int id) => await this.context.Set<TEntity>().FindAsync(id);

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
