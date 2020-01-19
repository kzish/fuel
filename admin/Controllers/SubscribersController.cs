using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using admin.Models;

namespace admin.Controllers
{
    [Route("Subscribers")]
    public class SubscribersController : Controller
    {
        private string source = "admin.Controllers.SubscribersController";
        fuelContext db = new fuelContext();
        
        [Route("Petrol")]
        public IActionResult Petrol()
        {
            ViewBag.title = "Petrol Subscribers";
            var petrol_subs = db.MSubscription.Where(i => i.FuelType == (int)Globals.eFuelType.petrol).ToList();
            ViewBag.petrol_subs = petrol_subs;
            return View();
        }

        [Route("Diesel")]
        public IActionResult Diesel()
        {
            ViewBag.title = "Diesel Subscribers";
            var diesel_subs = db.MSubscription.Where(i => i.FuelType == (int)Globals.eFuelType.diesel).ToList();
            ViewBag.diesel_subs = diesel_subs;
            return View();
        }

        [Route("Gas")]
        public IActionResult Gas()
        {
            ViewBag.title = "Gas Subscribers";
            var gas_subs = db.MSubscription.Where(i => i.FuelType == (int)Globals.eFuelType.gas).ToList();
            ViewBag.gas_subs = gas_subs;
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}
