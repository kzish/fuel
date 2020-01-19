using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using admin.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using admin.Models;
using static admin.Models.Globals;

namespace admin.Controllers
{
    [Authorize]
    [Route("Home")]
    [Route("")]
    public class HomeController : Controller
    {

        fuelContext db = new fuelContext();
        [Route("Dashboard")]
        [Route("")]
        public IActionResult Dashboard()
        {
            ViewBag.title = "Dashboard";

            //pending orders
            ViewBag.pending_petrol_orders = db.MOrders.Where(i => i.FuelType == (int)eFuelType.petrol && i.State==(int)eState.pending).Count();
            ViewBag.pending_diesel_orders = db.MOrders.Where(i => i.FuelType == (int)eFuelType.diesel && i.State == (int)eState.pending).Count();
            ViewBag.pending_gas_orders = db.MOrders.Where(i => i.FuelType == (int)eFuelType.gas && i.State == (int)eState.pending).Count();
            //delivered orders
            ViewBag.delivered_petrol_orders = db.MOrders.Where(i => i.FuelType == (int)eFuelType.petrol && i.State == (int)eState.closed).Count();
            ViewBag.delivered_diesel_orders = db.MOrders.Where(i => i.FuelType == (int)eFuelType.diesel && i.State == (int)eState.closed).Count();
            ViewBag.delivered_gas_orders = db.MOrders.Where(i => i.FuelType == (int)eFuelType.gas && i.State == (int)eState.closed).Count();
            //enque subs
            ViewBag.pending_petrol_subs = db.MSubscription.Where(i => i.FuelType == (int)eFuelType.petrol && i.State == (int)eState.pending).Count();
            ViewBag.pending_diesel_subs = db.MSubscription.Where(i => i.FuelType == (int)eFuelType.diesel && i.State == (int)eState.pending).Count();
            ViewBag.pending_gas_subs = db.MSubscription.Where(i => i.FuelType == (int)eFuelType.gas && i.State == (int)eState.pending).Count();
            //deque subs
            ViewBag.delivered_petrol_subs = db.MSubscription.Where(i => i.FuelType == (int)eFuelType.petrol && i.State == (int)eState.closed).Count();
            ViewBag.delivered_diesel_subs = db.MSubscription.Where(i => i.FuelType == (int)eFuelType.diesel && i.State == (int)eState.closed).Count();
            ViewBag.delivered_gas_subs = db.MSubscription.Where(i => i.FuelType == (int)eFuelType.gas && i.State == (int)eState.closed).Count();

            return View();
        }

        [HttpGet("FuelStations")]
        public IActionResult FuelStations(string search)
        {
            ViewBag.title = "FuelStations";
            var fuelstations = db.MFuelStations.ToList();
            if(!string.IsNullOrEmpty(search))
            {
                fuelstations = fuelstations.Where(i => i.Name.Contains(search)).ToList();
            }
            ViewBag.fuelstations = fuelstations;
            return View();
        }

       

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }

    }
}
