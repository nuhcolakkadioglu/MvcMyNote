using MyNote.DataAccessLayer.EntityFramework;
using MyNote.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNote.BussinessLayer
{
    public class CategoryManager
    {
        private static Repository<Category> repo_category = new Repository<Category>();

        public static List<Category> GetCategories()
        {
            return repo_category.List(); 
        }

        public static Category GetCategoryById(int id)
        {
            return repo_category.Find(m=>m.Id==id);
        }

    }
}
