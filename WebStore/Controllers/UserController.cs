using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class UserController : Controller
    {
        private StoreDbContext db = new StoreDbContext();

        // GET: User
        public ActionResult Index()
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Index", "Error");
            }
            // Fetch users with roles other than Admin
            var users = db.User.Where(u => u.Role != "Admin").ToList();
            return View(users);
        }

        // POST: User/Ban/5
        [HttpPost]
        public ActionResult Ban(int id)
        {
            var user = db.User.Find(id);
            if (user != null && user.Role == "Customer")
            {
                user.Role = "Banned";
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // POST: User/Unban/5
        [HttpPost]
        public ActionResult Unban(int id)
        {
            var user = db.User.Find(id);
            if (user != null && user.Role == "Banned")
            {
                user.Role = "Customer";
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: User/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user, string password)
        {
            if (ModelState.IsValid)
            {
                // Check if the username is already taken
                var existingUser = db.User.FirstOrDefault(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "This username is already taken. Please choose another one.");
                }

                // Validate the password
                if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                {
                    ModelState.AddModelError("Password", "Password must be at least 6 characters long.");
                }

                if (ModelState.IsValid)
                {
                    // Hash the password using MD5
                    user.PasswordHash = GetMd5Hash(password);
                    user.Role = "Customer"; // Default role

                    db.User.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }

            return View(user);
        }

        // GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            var user = db.User.FirstOrDefault(u => u.Username == username);
            if (user != null && user.PasswordHash == GetMd5Hash(password)) // Hash and compare
            {
                if (user.Role == "Banned")
                {
                    ModelState.AddModelError("", "Your account is banned.");
                    return View();
                }

                // Store UserId and Role in session
                Session["UserId"] = user.UserId;
                Session["Role"] = user.Role;

                return RedirectToAction("Index", "Home"); // Redirect to a desired page after login
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View();
        }

        // GET: User/Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Clear session
            return RedirectToAction("Index", "Home"); // Redirect to home after logout
        }

        static string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}