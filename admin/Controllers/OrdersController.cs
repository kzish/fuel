using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using admin.Models;

namespace admin.Controllers
{
    [Route("Orders")]
    public class OrdersController : Controller
    {
        private string source = "admin.Controllers.OrdersController";
        fuelContext db = new fuelContext();
        
        [Route("Petrol")]
        public IActionResult Petrol()
        {
            ViewBag.title = "Petrol Orders";
            var petrol_orders = db.MOrders.Where(i => i.FuelType == (int)Globals.eFuelType.petrol).ToList();
            ViewBag.petrol_orders = petrol_orders;
            return View();
        }

        [Route("Diesel")]
        public IActionResult Diesel()
        {
            ViewBag.title = "Diesel Orders";
            var diesel_orders= db.MOrders.Where(i => i.FuelType == (int)Globals.eFuelType.diesel).ToList();
            ViewBag.diesel_orders = diesel_orders;
            return View();
        }

        [Route("Gas")]
        public IActionResult Gas()
        {
            ViewBag.title = "Gas Orders";
            var gas_orders = db.MOrders.Where(i => i.FuelType == (int)Globals.eFuelType.gas).ToList();
            ViewBag.gas_orders = gas_orders;
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
