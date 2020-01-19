    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using admin.Models;


namespace admin.Controllers
{
    [Route("FuelStations")]
    public class FuelStationsController : Controller
    {
        private string source = "admin.Controllers.FuelStationsController";
        fuelContext db = new fuelContext();


        [HttpGet("AddNewFuelStation")]
        public IActionResult AddNewFuelStation()
        {
            ViewBag.title = "AddNewFuelStation";
            return View();
        }

        [HttpPost("AddNewFuelStation")]
        public IActionResult AddNewFuelStation(MFuelStations station)
        {
            try
            {
                db.MFuelStations.Add(station);
                db.SaveChanges();
                TempData["type"] = "success";
                TempData["msg"] = "Saved";
            }
            catch(Exception ex)
            {
                TempData["type"] = "error";
                TempData["msg"] = ex.Message;
                Globals.log_data_to_file(source+ ".AddNewFuelStation",ex);
            }
            return RedirectToAction("FuelStations", "Home");
        }

        [HttpGet("EditFuelStation/{id}")]
        public IActionResult EditFuelStation(int id)
        {
            ViewBag.title = "EditFuelStation";
            var station = db.MFuelStations.Where(i => i.Id == id).FirstOrDefault();
            ViewBag.station = station;
            return View();
        }

        [HttpPost("EditFuelStation")]
        public IActionResult EditFuelStation(MFuelStations station)
        {
            try
            {
                db.Entry<MFuelStations>(station).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                TempData["type"] = "success";
                TempData["msg"] = "Saved";
            }
            catch (Exception ex)
            {
                TempData["type"] = "error";
                TempData["msg"] = ex.Message;
                Globals.log_data_to_file(source + ".AddNewFuelStation", ex);
            }
            return RedirectToAction("FuelStations","Home");
        }


        [Route("DeleteFuelStation/{id}")]
        public IActionResult DeleteFuelStation(int id)
        {
            try
            {
                //delete the fuel station
                var fs = db.MFuelStations.Where(i => i.Id == id).FirstOrDefault();
                db.Remove(fs);
                db.SaveChanges();
                TempData["type"] = "success";
                TempData["msg"] = "Deleted Station";
            }catch(Exception ex)
            {
                TempData["type"] = "error";
                TempData["msg"] = ex.Message;
                Globals.log_data_to_file(source + ".DeleteFuelStation", ex);
            }
            return RedirectToAction("FuelStations", "Home");

        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
    }
}

