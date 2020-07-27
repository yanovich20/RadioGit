using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace Core.Repositories.Impl
{
    public class Repository<T> : IRepository<T> where T : TEntity
    {
        private RadioContext Database { get; }

        public Repository(RadioContext context) 
        {
            this.Database=context;
        }

        protected IQueryable<T> Queryable() {
            return Database.Set<T>().Where(t => !t.Deleted);
        }

        public virtual List<T> All()
        {
            return Database.Set<T>().Where(t => !t.Deleted).ToList();
        }

        public virtual T Get(long id)
        {
            return Database.Set<T>().SingleOrDefault(t => t.Id == id);
        }

        public virtual void Delete(long Id)
        {
            T entity = Get(Id);
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            Database.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            entity.Modified = DateTime.Now;
            Database.Set<T>().Update(entity);
        }

        public virtual void Insert(T entity)
        {
            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;
            Database.Set<T>().Add(entity);
        }

        public virtual void SoftDelete(T entity)
        {
            entity.Deleted = true;
            entity.Modified = DateTime.Now;
            Edit(entity);
        }

        public virtual void SoftDelete(long id)
        {
            T entity = Get(id);
            SoftDelete(entity);
        }

        public void SaveChanges() {
            Database.SaveChanges();
        }
    }
}
