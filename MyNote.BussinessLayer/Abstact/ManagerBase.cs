using MyNote.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MyNote.DataAccessLayer.EntityFramework;

namespace MyNote.BussinessLayer.Abstact
{
    public abstract class ManagerBase<T> : IDataAccess<T> where T:class
    {
        private Repository<T> repo = new Repository<T>();

        public int Delete(T model)
        {
            return repo.Delete(model);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public int Insert(T model)
        {
            return repo.Insert(model);
        }

        public List<T> List()
        {
            return repo.List();
        }

        public List<T> List(Expression<Func<T, bool>> predicate)
        {
            return repo.List(predicate);
        }

        public IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public int Save()
        {
            return repo.Save();
        }

        public int Update(T model)
        {
            return repo.Update(model);
        }
    }
}
