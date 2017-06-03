using MyNote.BussinessLayer;
using MyNote.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MyNote.Web.Models
{
    public class CacheHelper
    {
        public static List<Category> GetCategoryesFromCache()
        {
            var result = WebCache.Get("category-cahce");
            if(result==null)
            {
                CategoryManager catmanager = new CategoryManager();
                result = catmanager.List();
                WebCache.Set("category-cahce", result, 20,true);
            }

            return result;
           
        }

        public static void Remove(string key)
        {
            WebCache.Remove(key);
        }
    }
}