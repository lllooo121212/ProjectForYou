﻿using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class OrderItemController : Controller
    {
        private StoreDbContext db = new StoreDbContext();

        // GET: OrderItem
        public ActionResult Index()
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Index", "Error");
            }
            // Retrieve all OrderItems
            var orderItems = db.OrderItem.Include("User").Include("Voucher").ToList();
            return View(orderItems);
        }

        // GET: OrderItem/OrderDetails/5
        public ActionResult OrderDetails(int id)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                return RedirectToAction("Index", "Error");
            }
            // Find the OrderItem by id and include related OrderDetails and Vouchers
            var orderItem = db.OrderItem
                .Include(oi => oi.OrderDetail.Select(od => od.Voucher)) // Include the related OrderDetails and Vouchers
                .FirstOrDefault(oi => oi.OrderId == id);

            if (orderItem == null)
            {
                return HttpNotFound();
            }

            // Retrieve OrderDetails related to this OrderItem, ensuring Vouchers are included
            var orderDetails = db.OrderDetail
                .Where(od => od.OrderId == orderItem.OrderId)
                .Include(od => od.Voucher) // Include the Voucher for each OrderDetail
                .ToList();

            return View(orderDetails);
        }
    }
}