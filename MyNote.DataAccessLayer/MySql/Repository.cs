using MyNote.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MyNote.DataAccessLayer.MySql
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        public int Delete(T model)
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Insert(T model)
        {
            throw new NotImplementedException();
        }

        public List<T> List()
        {
            throw new NotImplementedException();
        }

        public List<T> List(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> ListQueryable()
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public int Update(T model)
        {
            throw new NotImplementedException();
        }
    }
}
