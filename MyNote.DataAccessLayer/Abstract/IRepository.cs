using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        List<T> List();
        int Insert(T model);
        int Update(T model);
        int Delete(T model);
        List<T> List(Expression<Func<T, bool>> predicate);
        T Find(Expression<Func<T, bool>> predicate);
        int Save();
         IQueryable<T> ListQueryable();
        


    }
}
