using EducationPortal.DAL.SQL.DataContext;
using EducationPortal.DAL.SQL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationPortal.DAL.SQL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public void Create(T item)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                db.Set<T>().Add(item);
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                var entity = db.Set<T>().Find(id);

                if(entity != null)
                {
                    db.Set<T>().Remove(entity);
                    db.SaveChanges();
                }
            }
        }

        public T Get(int id)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                return db.Set<T>().Find(id);
            }
        }

        public List<T> GetAll()
        {
            //think about this method
            using(ApplicationContext db = new ApplicationContext())
            {
                return db.Set<T>().ToList();
            }
        }

        public void Update(T item)
        {
            using(ApplicationContext db = new ApplicationContext())
            {
                db.Entry<T>(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
