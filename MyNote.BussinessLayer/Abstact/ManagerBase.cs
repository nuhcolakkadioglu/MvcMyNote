using MyNote.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MyNote.DataAccessLayer.MySql;


namespace MyNote.BussinessLayer.Abstact
{
    
    public abstract class ManagerBase<T> : IDataAccess<T> where T:class
    {
        private Repository<T> repo = new Repository<T>();

        public virtual int Delete(T model)
        {
            return repo.Delete(model);
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public virtual int Insert(T model)
        {
            return repo.Insert(model);
        }

        public virtual List<T> List()
        {
            return repo.List();
        }

        public virtual List<T> List(Expression<Func<T, bool>> predicate)
        {
            return repo.List(predicate);
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public virtual int Save()
        {
            return repo.Save();
        }

        public virtual int Update(T model)
        {
            return repo.Update(model);
        }
    }
}
