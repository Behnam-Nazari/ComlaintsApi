using Common.Utilities;
using Data.Contracts;

using Entity.Complaitns;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly ApplicationDbContext _DbContext;
        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

        public Repository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
            Entities = _DbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await Entities.FindAsync(ids, cancellationToken);
        }


        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await _DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }


        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken,
            bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await _DbContext.SaveChangesAsync(cancellationToken);
        }


        public virtual async Task UpdateAsyc(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            if (saveNow) await _DbContext.SaveChangesAsync(cancellationToken);

        }


        public virtual async Task UpdateRangeAsyc(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow) await _DbContext.SaveChangesAsync(cancellationToken);
        }


        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow) await _DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }


        public virtual async Task DeleteRangeAsyc(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow) await _DbContext.SaveChangesAsync(cancellationToken);
        }


        public virtual void Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (_DbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);

        }


        public virtual void Detach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (_DbContext.Entry(entity) != null)
                _DbContext.Entry(entity).State = EntityState.Detached;

        }


        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var collection = _DbContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded) await collection.LoadAsync(cancellationToken);
        }


        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var reference = _DbContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded) await reference.LoadAsync(cancellationToken);
        }
    }
}
