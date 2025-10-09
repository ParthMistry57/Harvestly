using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Harvestly.Models;

namespace Harvestly.Controllers
{
    public class HomeController : Controller
    {
        HarvestlyContext db = new HarvestlyContext();

        public static string controllerName;
        public static string actionName;
        protected override void OnException(ExceptionContext exceptionContext)
        {
            controllerName = (string)exceptionContext.RouteData.Values["Controller"];
            actionName = (string)exceptionContext.RouteData.Values["action"];

            ViewResult view = new ViewResult();
            view.ViewName = "~/Views/Shared/Error.cshtml";
            exceptionContext.Result = view;
            exceptionContext.ExceptionHandled = true;

        }
        public ActionResult AdminValidation(bool isAdmin = false)
        {
            if (isAdmin == true)
                return RedirectToAction("AdminIndex");
            else
                return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminIndex()
        {
            var menuItemList = (from m in db.MenuItems.Include("Category") select m).ToList();
            return View(menuItemList);
        }

        public ActionResult newRecord()
        {
            ViewBag.CategoryList = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult newRecord(MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = new SelectList(db.Categories.ToList(), "Id", "Name");
                return View("newRecord");
            }
            db.MenuItems.Add(menuItem);
            db.SaveChanges();
            return RedirectToAction("AdminIndex", "Home");
        }
        public ActionResult deleteRecord(int? id)
        {
            MenuItem record = db.MenuItems.Where(k => k.Id == id).SingleOrDefault();
            db.MenuItems.Remove(record);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        public ActionResult updateRecord(int id)
        {
            MenuItem menuItem = new MenuItem();
            menuItem = db.MenuItems.Include("Category").SingleOrDefault(c => c.Id == id);
            if (menuItem == null)
                return HttpNotFound();

            ViewBag.CategoryList = new SelectList(db.Categories.ToList(), "Id", "Name");
            return View(menuItem);
        }
      

        [HttpPost]
        public ActionResult updateRecord(MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryList = new SelectList(db.Categories.ToList(), "Id", "Name");
                return View("updateRecord");
            }

            if (menuItem.Id == 0)
                db.MenuItems.Add(menuItem);
            else
            {
                var selectedMenuItem = db.MenuItems.Single(i => i.Id == menuItem.Id);

                selectedMenuItem.Name = menuItem.Name;
                selectedMenuItem.Price = menuItem.Price;
                selectedMenuItem.Quantity = menuItem.Quantity;
                selectedMenuItem.Active = menuItem.Active;
                selectedMenuItem.dateOfLaunch = menuItem.dateOfLaunch;
                selectedMenuItem.categoryId = menuItem.categoryId;
                selectedMenuItem.freeDelivery = menuItem.freeDelivery;
            }
            db.SaveChanges();
            return RedirectToAction("AdminIndex", "home");
        }
    }
}