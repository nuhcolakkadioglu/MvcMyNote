using MyNote.Common;
using MyNote.DataAccessLayer.Abstract;
using MyNote.Enties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MyNote.DataAccessLayer.EntityFramework
{
    public class Repository<T>:RepositoryBase,IRepository<T> where T:class
    {

        private DbSet<T> _dbSet;

        public Repository()
        {
            _dbSet = context.Set<T>();
        }
        public List<T> List()
        {
           return _dbSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        public int Insert(T model)
        {
           
            if(model is MyEntityBase)
            {
                MyEntityBase o = model as MyEntityBase;
              
                o.CreatedOn = DateTime.Now;
                o.Modified = DateTime.Now;
                o.ModifiedUsername = App.Common.GetUserName();// TODO : işlem yapan user yazılmalı
            }
            _dbSet.Add(model);

            return Save();
        }

        public int Update(T model)
        {
            if (model is MyEntityBase)
            {
                MyEntityBase o = model as MyEntityBase;

                o.Modified = DateTime.Now;
                o.ModifiedUsername = App.Common.GetUserName(); // TODO : işlem yapan user yazılmalı
            }
            return Save();
        }

        public int Delete(T model)
        {
         
            _dbSet.Remove(model);

            return Save();
        }

        public List<T> List(Expression<Func<T,bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public T Find(Expression<Func<T,bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public int Save()
        {
            return context.SaveChanges();
        }


    }
}
