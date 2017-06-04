using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.DataAccessLayer.MySql
{
   public class RepositoryBase
    {
        protected static DatabaseContextMySQL context;
        private static object _obj = new object();
        protected RepositoryBase()
        {
            context = CreateContext();
        }

        private static DatabaseContextMySQL CreateContext()
        {
            if (context == null)
            {
                lock (_obj)
                {
                    if (context == null)
                    {
                        context = new DatabaseContextMySQL();
                    }
                }
            }

            return context;
        }
    }
}
