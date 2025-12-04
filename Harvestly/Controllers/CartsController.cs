using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Harvestly.Models;
using Harvestly.Helpers;

namespace Harvestly.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        HarvestlyContext context = new HarvestlyContext();

        public ActionResult Indexes()
        {
            // Only show active products
            List<MenuItem> see = context.MenuItems.Where(m => m.Active == true).ToList();
            return View(see);
        }

        public ActionResult AddToCart(int menuId)
        {
            // Get current user ID or use default guest ID (1)
            int userId = SessionHelper.GetUserId() ?? 1;

            ViewBag.MenuItemID = new SelectList(context.MenuItems.ToList(), "Id", "Name");
            // Only allow adding active products to cart
            MenuItem menu = context.MenuItems.Where(m => m.Id == menuId && m.Active == true).FirstOrDefault();
            
            if (menu == null)
            {
                TempData["ErrorMessage"] = "This product is no longer available.";
                return RedirectToAction("Indexes");
            }

            Cart cart = new Cart();
            cart.menuItemId = menu.Id;
            cart.userId = userId;
            context.Carts.Add(cart);
            context.SaveChanges();

            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            // Get current user ID or use default guest ID (1)
            int userId = SessionHelper.GetUserId() ?? 1;

            // Only show cart items with active products
            var userCarts = context.Carts
                .Include(c => c.MenuItem)
                .Where(u => u.userId == userId && u.MenuItem.Active == true)
                .ToList();

            // Remove inactive products from cart (cleanup)
            var inactiveCartItems = context.Carts
                .Include(c => c.MenuItem)
                .Where(u => u.userId == userId && u.MenuItem.Active == false)
                .ToList();
            
            if (inactiveCartItems.Any())
            {
                foreach (var inactiveItem in inactiveCartItems)
                {
                    context.Carts.Remove(inactiveItem);
                }
                context.SaveChanges();
            }

            if (userCarts.Count < 1)
                return RedirectToAction("EmptyCart");
            
            return View(userCarts);
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