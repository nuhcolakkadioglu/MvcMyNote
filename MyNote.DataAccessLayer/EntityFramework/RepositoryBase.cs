using MyNote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.DataAccessLayer.EntityFramework
{
    public class RepositoryBase
    {
        protected static DatabaseContext context;
        private static object _obj = new object();
        protected RepositoryBase()
        {
            context= CreateContext();
        }

        private static DatabaseContext CreateContext()
        {
            if (context == null)
            {
                lock (_obj)
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }

            return context;
        }
    }
}
