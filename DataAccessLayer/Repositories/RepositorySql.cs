using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EducationPortal.DAL.Repositories
{
    //public class RepositorySql
    //{
    //    private readonly DbContext context;

    //    public Repository(DbContext context)
    //    {
    //        this.context = context;
    //    }

    //    public IList<TEntity> GetAll<TEntity>()
    //    {
    //        return this.context.Set<TEntity>().ToList();
    //    }

    //    public IList<TEntity> GetAll<TEntity>(
    //        params Expresion<Func<TEntity, TJoin>>[] includes)
    //    {
    //        var result = this.context.Set<TEntity>();
    //        foreach (var include in includes)
    //        {
    //            result.Include(include);
    //        }

    //        return result.ToList();
    //    }

    //    public IList<TEntity> Get<TEntity>(
    //        Expresion<Func<TEntity, bool>> predicat,
    //        params Expresion<Func<TEntity, TJoin>>[] includes)
    //    {
    //        var result = this.context.Set<TEntity>();
    //        foreach (var include in includes)
    //        {
    //            result.Include(include);
    //        }

    //        return result.Where(predicat).ToList();
    //    }

    //    public IList<TResult> Get<TEntity, TResult>(
    //        Expresion<Func<TEntity, TResult>> selector)
    //    {
    //        return this.context.Set<TEntity>()
    //                    .Select(selector).ToList();
    //    }

    //    public IList<TResult> Get<TEntity, TResult>(
    //        Expresion<Func<TEntity, TResult>> selector,
    //        Expresion<Func<TEntity, bool>> predicat)
    //    {
    //        return this.context.Set<TEntity>()
    //                    .Where(predicat)
    //                    .Select(selector).ToList();
    //    }

    //    public IList<TEntity> GetPage<TEntity>(PageInfo page)
    //    {
    //        var skip = page.Size * (page.Number - 1);
    //        return this.context.Set<TEntity>()
    //                    .Skip(skip).Take(page.Size).ToList();
    //    }

    //    public IList<TEntity> GetPage<TEntity>(
    //        Expresion<Func<TEntity, bool>> predicat,
    //        PageInfo page)
    //    {
    //        var skip = page.Size * (page.Number - 1);
    //        return this.context.Set<TEntity>()
    //                    .Where(predicat)
    //                    .Skip(skip).Take(page.Size).ToList();
    //    }

    //    public IList<TEntity> GetPage<TEntity>(
    //        Expression<Func<TEntity, TResult>> selector,
    //        Expression<Func<TEntity, bool>> predicat,
    //        PageInfo page)
    //    {
    //        var skip = page.Size * (page.Number - 1);
    //        return this.context.Set<TEntity>()
    //                    .Select(selector)
    //                    .Where(predicat)
    //                    .Skip(skip).Take(page.Size).ToList();
    //    }

    //    public bool Exist<TEntity>(Expression<Func<TEntity, bool>> predicat)
    //    {
    //        return this.context.Set<TEntity>().Any(predicat);
    //    }

    //    public int AddAndSave<TEntity>(TEntity entity)
    //    {
    //        this.context.Set<TEntity>().Add(entity);
    //        this.Save();
    //    }

    //    public int Add<TEntity>(TEntity entity)
    //    {
    //        this.context.Set<TEntity>().Add(entity);
    //    }

    //    public void Save()
    //    {
    //        this.context.SaveChanges();
    //    }
    //}
}
