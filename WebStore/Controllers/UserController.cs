using System.Linq;
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
    }
}