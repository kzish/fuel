using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fuelApi.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// all orders are valid until cancelled or delivered
/// </summary>
namespace fuelApi.Controllers
{
    [Route("Orders")]
    public class OrdersController : Controller
    {
        private string source = "fuelApi.Controllers.OrdersController.";
        fuelContext db = new fuelContext();
        [HttpPost("OrderForFuel")]
        public JsonResult OrderForFuel([FromBody]MOrders order)
        {
            try
            {
                //client may have as many orders as they want
                order.Date = DateTime.Now;
                order.State = (int)Globals.eState.pending;
                db.MOrders.Add(order);
                db.SaveChanges();
                int id = order.Id;
                return Json(new
                {
                    res = "ok",
                    msg = "Your order has been recieved",
                    order_number=id
                });

            }
            catch (Exception ex)
            {
                Globals.log_data_to_file(source + "OrderForFuel", ex);
                return Json(new
                {
                    res = "err",
                    msg = "Error occurred"
                });
            }

        }

        [HttpPost("CancelOrder/{id}")]
        public JsonResult CancelOrder(int id)
        {
            try
            {
                var order = db.MOrders.Where(i => i.Id == id).FirstOrDefault();
                db.Remove(order);
                db.SaveChanges();
                return Json(new
                {
                    res = "ok",
                    msg = "Order cancelled"
                });
            }
            catch (Exception ex)
            {
                Globals.log_data_to_file(source + "CancelOrder", ex);
                return Json(new
                {
                    res = "err",
                    msg = "Error occured"
                });
            }
        }

       protected override void Dispose(bool disposing)
       {
           base.Dispose(disposing);
           db.Dispose();
       }

    }
}
