﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyNote.Enties;
using MyNote.BussinessLayer;
using MyNote.Web.Models;
using MyNote.Web.Filters;

namespace MyNote.Web.Controllers
{
    [Auth, AuthAdmin, ErrorFil]
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager();

        public ActionResult Index()
        {
            return View(categoryManager.List());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(m=>m.Id==id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Category category)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                categoryManager.Insert(category);
                CacheHelper.Remove("category-cahce");
                return RedirectToAction("Index");
            }

            return View(category);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(m => m.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Category category)
        {
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Category cat = categoryManager.Find(m=>m.Id==category.Id);
                cat.Description = category.Description;
                cat.Title = category.Title;

                categoryManager.Update(cat);
                CacheHelper.Remove("category-cahce");
                return RedirectToAction("Index");
            }
            return View(category);
        }

      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(m=>m.Id==id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryManager.Find(m => m.Id == id);
            categoryManager.Delete(category);
            CacheHelper.Remove("category-cahce");
            return RedirectToAction("Index");
        }

    }
}
