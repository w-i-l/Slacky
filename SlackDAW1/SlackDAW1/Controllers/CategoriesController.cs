﻿using SlackDAW1.Data;
using SlackDAW1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArticlesApp.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {

        // PASUL 10 - useri si roluri

        private readonly ApplicationDbContext db;

        public CategoriesController(ApplicationDbContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            var categories = from category in db.Categories
                             orderby category.CategoryName
                             select category;
            ViewBag.Categories = categories;

            var channels = from channel in db.Channels
                           join category in db.Categories on channel.CategoryID equals category.CategoryID
                           orderby channel.ChannelName
                           select channel;
            ViewBag.Channels = channels;

            return View();
        }

        public ActionResult Show(int id)
        {
            Category category = db.Categories.Find(id);
            
            var channels = from channel in db.Channels
                           orderby channel.ChannelName
                           select channel;
            ViewBag.Channels = channels;        
            return View(category);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Category cat)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                TempData["message"] = "Categoria a fost adaugata";
                return RedirectToAction("Index");
            }
            else
            {
                return View(cat);
            }
        }

        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(int id, Category requestCategory)
        {
            Category category = db.Categories.Find(id);

            if (ModelState.IsValid)
            {

                category.CategoryName = requestCategory.CategoryName;
                db.SaveChanges();
                TempData["message"] = "Categoria a fost modificata!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(requestCategory);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            TempData["message"] = "Categoria a fost stearsa";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [NonAction]
        public static IEnumerable<SelectListItem> GetAllCategoriesToDisplayForForm(ApplicationDbContext db)
        {
            var selectList = new List<SelectListItem>();

			var categories = from cat in db.Categories
							 orderby cat.CategoryName
							 select cat;

			foreach (var category in categories)
            {
				selectList.Add(new SelectListItem
                {
					Value = category.CategoryID.ToString(),
					Text = category.CategoryName.ToString()
				});
			}
			return selectList;
		}
    }
}