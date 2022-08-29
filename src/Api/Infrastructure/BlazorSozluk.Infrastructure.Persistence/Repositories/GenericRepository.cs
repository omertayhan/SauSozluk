using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlazorSozluk.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext dbContext;

    protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

    public GenericRepository(DbContext dbContext) //dependency injection
    {
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #region Insert Methods

    public virtual async Task<int> AddAsync(TEntity entity)
    {
        await this.entity.AddRangeAsync(entity);
        return await dbContext.SaveChangesAsync();
    }

    public virtual int Add(TEntity entity)
    {
        this.entity.Add(entity);
        return dbContext.SaveChanges();
    }

    public virtual AddAsync(IEnumerable<TEntity> entities)
    {
        if (entities != null && !entities.Any())
            return 0;

        entity.AddRange(entity);
        return dbContext.SaveChanges();
    }

    #endregion

    #region Update Methods

    public virtual async Task<int> UpdateAsync(TEntity entity)
    {
        this.entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;

        return await dbContext.SaveChangesAsync();
    }

    public virtual int Update(TEntity entity)
    {
        this.entity.Attach(entity);
        dbContext.Entry(entity).State = EntityState.Modified;

        return dbContext.SaveChanges();
    }

    #endregion

    #region Delete Methods

    public virtual Task<int> DeleteAsync(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            this.entity.Attach(entity);
        }

        this.entity.Remove(entity);

        return dbContext.SaveChangesAsync();
    }

    public virtual Task<int> DeleteAsync(Guid id)
    {
        var entity = this.entity.Find(id);
        return DeleteAsync(entity);
    }

    public virtual int Delete(Guid id)
    {
        var entity = this.entity.Find(id);
        return Delete(entity);
    }

    public virtual int Delete(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
        {
            this.entity.Attach(entity);
        }

        this.entity.Remove(entity);
        return dbContext.SaveChanges();
    }

    public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return dbContext.SaveChanges() > 0;
    }

    public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity,bool>> predicate)
    {
        dbContext.RemoveRange(entity.Where(predicate));
        return dbContext.SaveChangesAsync() > 0;
    }


    #endregion

    #region AddorUpdate Methods

    public virtual Task<int> AddorUpdateAsync(TEntity entity)
    {
        // check the entity with the id already tracked
        if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
        {
            dbContext.Update(entity);
        }
        return dbContext.SaveChangesAsync();
    }

    public virtual int AddorUpdate(TEntity entity)
    {
        if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
        {
            dbContext.Update(entity);
        }
        return dbContext.SaveChanges();
    }

    #endregion

    #region Get Methods



    #endregion
}
