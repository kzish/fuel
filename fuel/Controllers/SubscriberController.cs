using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fuelApi.Models;

/// <summary>
/// one subscription per fuel type
/// </summary>
namespace fuelApi.Controllers
{
    [Route("Subscriber")]
    public class SubscriberController : Controller
    {
        private string source = "fuelApi.Controllers.SubscriberController.";
        fuelContext db = new fuelContext();
        [HttpPost("SubScribeForFuel")]
        public JsonResult SubScribeForFuel([FromBody]MSubscription subscription)
        {
            try
            {
                //check this person is not already subscribed
                var exist = db.MSubscription.Where(i => i.Mobile == subscription.Mobile && i.FuelType==subscription.FuelType).FirstOrDefault();
                if (exist == null)
                {
                    subscription.Date = DateTime.Now;
                    subscription.State = (int)Globals.eState.pending;
                    db.MSubscription.Add(subscription);
                    db.SaveChanges();
                    int id = subscription.Id;
                    return Json(new
                    {
                        res = "ok",
                        msg = @"You have subscribed, you will be notified when your fuel is ready",
                        subscription_number=id
                    });
                }
                else
                {
                    return Json(new
                    {
                        res = "err",
                        msg = @"You have already subscribed for this fuel type, cancel existing subscription or subscribe for another fuel type."
                    });
                }
                
            }
            catch (Exception ex)
            {
                Globals.log_data_to_file(source + "SubScribeForFuel", ex);
                return Json(new
                {
                    res = "err",
                    msg = "Error occurred"
                });
            }

        }

        [HttpPost("CancelSubScription/{id}")]
        public JsonResult CancelSubScription(int id)
        {
            try
            {
                //only one subscription at a time
                var sub = db.MSubscription.Where(i => i.Id == id).FirstOrDefault();
                db.Remove(sub);
                db.SaveChanges();
                return Json(new
                {
                    res = "ok",
                    msg = "Subscription cancelled"
                });
            }
            catch (Exception ex) {
                Globals.log_data_to_file(source + "SubScribeForFuel", ex);
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
