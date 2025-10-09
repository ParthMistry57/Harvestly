using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Harvestly.Models;

namespace Harvestly.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        HarvestlyContext context = new HarvestlyContext();

        public ActionResult Indexes()
        {
            List<MenuItem> see = context.MenuItems.ToList();
            return View(see);
        }

        public ActionResult AddToCart(int menuId)
        {
            ViewBag.MenuItemID = new SelectList(context.MenuItems.ToList(), "Id", "Name");
            MenuItem menu = context.MenuItems.Where(m => m.Id == menuId).FirstOrDefault();
            Cart cart = new Cart();
            cart.menuItemId = menu.Id;
            cart.userId = 1;
            context.Carts.Add(cart);
            context.SaveChanges();

            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            int cartContentsCount = context.Carts.Select(c => c.Id).Count();

            if (cartContentsCount < 1)
                return RedirectToAction("EmptyCart");
            return View(context.Carts.Where(u => u.userId == 1).ToList());
        }

        public ActionResult EmptyCart()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return RedirectToAction("Create","Orders");
        }


        public ActionResult Delete(int? cartId)
        {
            Cart record = context.Carts.Where(k => k.Id == cartId).SingleOrDefault();
            context.Carts.Remove(record);
            context.SaveChanges();
            return RedirectToAction("ViewCart");
        }
        public double ComputeTotalValue()
        {
         
                return context.Carts.Sum(e => e.MenuItem.Price);
            
        }
    }
}