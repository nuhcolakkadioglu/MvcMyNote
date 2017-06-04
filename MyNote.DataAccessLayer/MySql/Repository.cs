
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MyNote.Core.DataAccess;
using System.Data.Entity;
using MyNote.Enties;
using MyNote.Common;

namespace MyNote.DataAccessLayer.MySql
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
    {
        private DbSet<T> _dbSet;

        public Repository()
        {
            _dbSet = context.Set<T>();
        }

        public int Delete(T model)
        {
            _dbSet.Remove(model);

            return Save();
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public int Insert(T model)
        {
            if (model is MyEntityBase)
            {
                MyEntityBase o = model as MyEntityBase;

                o.CreatedOn = DateTime.Now;
                o.Modified = DateTime.Now;
                o.ModifiedUsername = App.Common.GetUserName();// TODO : işlem yapan user yazılmalı
            }
            _dbSet.Add(model);

            return Save();
        }

        public List<T> List()
        {
            return _dbSet.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _dbSet.AsQueryable<T>();
        }

        public int Save()
        {
            return context.SaveChanges();
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
    }
}
