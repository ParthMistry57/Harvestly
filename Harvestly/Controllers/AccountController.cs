using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Harvestly.Models;
using Harvestly.Helpers;
using Harvestly.Filters;

namespace Harvestly.Controllers
{
    public class AccountController : Controller
    {
        private HarvestlyContext db = new HarvestlyContext();

        // GET: Account/Login
        public ActionResult Login(string returnUrl = null, string loginType = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginType = loginType;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View();
            }

            var user = db.Users.FirstOrDefault(u => u.Username == username && u.IsActive);

            if (user == null || !PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }

            // Set user session
            SessionHelper.SetUserSession(user);

            // Redirect based on role
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            if (user.Role == UserRole.Admin)
            {
                return RedirectToAction("AdminIndex", "Home");
            }
            else if (user.Role == UserRole.Farmer)
            {
                return RedirectToAction("AdminIndex", "Home"); // Farmers can also manage products
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            SessionHelper.ClearSession();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/CreateFarmer (Admin only)
        [AuthorizeAdmin]
        public ActionResult CreateFarmer()
        {
            return View();
        }

        // POST: Account/CreateFarmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin]
        public ActionResult CreateFarmer(User user, string password)
        {
            try
            {
                // Validate password
                if (string.IsNullOrEmpty(password) || password.Length < 6)
                {
                    ModelState.AddModelError("Password", "Password must be at least 6 characters long.");
                }

                // Validate username
                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    ModelState.AddModelError("Username", "Username is required.");
                }
                else if (db.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Username already exists.");
                }

                // Remove PasswordHash from ModelState validation since we set it manually
                ModelState.Remove("PasswordHash");
                ModelState.Remove("Role");
                ModelState.Remove("CreatedDate");
                ModelState.Remove("IsActive");

                if (ModelState.IsValid)
                {
                    // Set all required fields
                    user.PasswordHash = PasswordHelper.HashPassword(password);
                    user.Role = UserRole.Farmer;
                    user.CreatedDate = DateTime.Now;
                    user.IsActive = true;

                    // Ensure Username is trimmed
                    if (user.Username != null)
                    {
                        user.Username = user.Username.Trim();
                    }

                    db.Users.Add(user);
                    int result = db.SaveChanges();

                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = $"Farmer account '{user.Username}' created successfully.";
                        return RedirectToAction("ManageFarmers");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to save the farmer account. Please try again.");
                        TempData["ErrorMessage"] = "Failed to save the farmer account. Please try again.";
                    }
                }
                else
                {
                    // Log validation errors
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        System.Diagnostics.Debug.WriteLine("Validation Error: " + error.ErrorMessage);
                    }
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                ModelState.AddModelError("", "Database error: " + dbEx.Message);
                TempData["ErrorMessage"] = "Database error occurred. Please check if the User table exists in the database.";
                System.Diagnostics.Debug.WriteLine("Database Error: " + dbEx.ToString());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the farmer account: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
                // Log the exception for debugging
                System.Diagnostics.Debug.WriteLine("Error creating farmer: " + ex.ToString());
            }

            return View(user);
        }

        // GET: Account/ManageFarmers (Admin only)
        [AuthorizeAdmin]
        public ActionResult ManageFarmers()
        {
            var farmers = db.Users.Where(u => u.Role == UserRole.Farmer).ToList();
            return View(farmers);
        }

        // GET: Account/DeleteFarmer/5
        [AuthorizeAdmin]
        public ActionResult DeleteFarmer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            User user = db.Users.Find(id);
            if (user == null || user.Role != UserRole.Farmer)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Account/DeleteFarmer/5
        [HttpPost, ActionName("DeleteFarmer")]
        [ValidateAntiForgeryToken]
        [AuthorizeAdmin]
        public ActionResult DeleteFarmerConfirmed(int id)
        {
            User user = db.Users.Find(id);
            if (user != null && user.Role == UserRole.Farmer)
            {
                // Soft delete - deactivate instead of removing
                user.IsActive = false;
                db.SaveChanges();
                TempData["SuccessMessage"] = $"Farmer account '{user.Username}' has been deactivated.";
            }

            return RedirectToAction("ManageFarmers");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

