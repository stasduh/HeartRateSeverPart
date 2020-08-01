using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeartRateWeb.Models;

namespace HeartRateWeb.Controllers
{
    public class HeartRatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Sportsman currentSportsman;

        // GET: HeartRates
        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login");
            }
            return View(db.HeartRates.ToList());
        }

        public string CreateHeartRateValue(string SportsmanId, string FirstName, string LastName, string TeamNumber, string value) 
        {
            Sportsman sportsman = new Sportsman();
            bool firstConnect = false;

            if (SportsmanId == null)
            {
                // Это первое обращение, сначала создадим Спортмена и запишем в базу
                sportsman.FirstName = FirstName;
                sportsman.LastName = LastName;
                sportsman.TeamNumber = TeamNumber;

                SportsmenController sportsmenController = new SportsmenController();
                sportsmenController.Create(sportsman);

                firstConnect = true;
            }
            else
            {
                sportsman = db.Sportsmen.Find(SportsmanId);
            }

            HeartRate heartRate = new HeartRate();
            heartRate.Id = Guid.NewGuid().ToString();
            heartRate.SportsmanId = sportsman.Id;
            heartRate.FirstName = sportsman.FirstName;
            heartRate.LastName = sportsman.LastName;
            heartRate.HeartRateValue = value;

            TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
            DateTime moscowDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, moscowTimeZone);
            heartRate.DateTime = moscowDateTime;

            db.HeartRates.Add(heartRate);
            db.SaveChanges();

            if (firstConnect)
            {
                return sportsman.Id;
            }

            return "Success";
        }

        // GET: HeartRates/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeartRate heartRate = db.HeartRates.Find(id);
            if (heartRate == null)
            {
                return HttpNotFound();
            }
            return View(heartRate);
        }

        // GET: HeartRates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HeartRates/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DateTime,SportsmanId,HeartRateValue")] HeartRate heartRate)
        {
            if (ModelState.IsValid)
            {
                if (heartRate.Id == null)
                {
                    heartRate.Id = Guid.NewGuid().ToString();
                }

                TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
                DateTime moscowDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, moscowTimeZone);
                heartRate.DateTime = moscowDateTime;
 
                //currentSportsman = db.Sportsmen.Find("eb75903d-b05d-42d6-b531-0031ff283bf2");
                
                //heartRate.SportsmanId = currentSportsman.Id;
                //heartRate.FirstName = currentSportsman.FirstName;
                //heartRate.LastName = currentSportsman.LastName;

                db.HeartRates.Add(heartRate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(heartRate);
        }

        // GET: HeartRates/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeartRate heartRate = db.HeartRates.Find(id);
            if (heartRate == null)
            {
                return HttpNotFound();
            }
            return View(heartRate);
        }

        // POST: HeartRates/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateTime,SportsmanId,HeartRateValue")] HeartRate heartRate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(heartRate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(heartRate);
        }

        // GET: HeartRates/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HeartRate heartRate = db.HeartRates.Find(id);
            if (heartRate == null)
            {
                return HttpNotFound();
            }
            return View(heartRate);
        }

        // POST: HeartRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HeartRate heartRate = db.HeartRates.Find(id);
            db.HeartRates.Remove(heartRate);
            db.SaveChanges();
            return RedirectToAction("Index");
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
