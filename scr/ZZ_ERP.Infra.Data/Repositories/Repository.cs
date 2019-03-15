using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZZ_ERP.Domain.Entities;
using ZZ_ERP.Domain.Interfaces;
using ZZ_ERP.Infra.CrossCutting.Connections.Functions;
using ZZ_ERP.Infra.Data.Contexts;

namespace ZZ_ERP.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        internal ZZContext Context;
        internal DbSet<TEntity> DbSet;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public Repository(ZZContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param> 
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = DbSet;

                if (filter != null)
                {
                    query = query.Where(filter).Where(e => e.IsActive);
                }
                else
                {
                    query = query.Where(e => e.IsActive);
                }

                var properties = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
      
                foreach (var includeProperty in properties)
                {
                    query = query.Include(includeProperty);
                }
                

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }

                var qry = await query.ToListAsync();

                return qry;
                //return await orderBy?.Invoke(query).ToListAsync() ?? query.ToListAsync();
            }
            catch (Exception e)
            {
                ConsoleEx.WriteError(e);
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetById(object id)
        {
            TEntity entity = await DbSet.FindAsync(id);
            return entity;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task InsertList(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await DbSet.AddAsync(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public virtual async Task Delete(object id)
        {
            TEntity entityToDelete = await DbSet.FindAsync(id);
            await Delete(entityToDelete);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToDelete"></param>
        public virtual async Task Delete(TEntity entityToDelete)
        {
            entityToDelete.IsActive = false;
        }
        
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToUpdate"></param>
        public virtual async Task Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await Context.SaveChangesAsync();
        }
    }
}
