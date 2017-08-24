using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rivals;

namespace Rivals.Controllers
{
    public class TrainingsController : Controller
    {
        private RivalsEntities db = new RivalsEntities();

        // GET: Trainings
        public ActionResult Index()
        {
            var training = db.Training.Include(t => t.Locatie).Include(t => t.Trainer);
            return View(training.ToList());
        }

        // GET: Trainings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Training.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // GET: Trainings/Create
        public ActionResult Create()
        {
            ViewBag.FK_LocationId = new SelectList(db.Locatie, "LocationId", "LocatieNaam");
            ViewBag.FK_TrainerId = new SelectList(db.Trainer, "TrainerId", "DisplayName");
            return View();
        }

        // POST: Trainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainingId,DisplayTitle,Description,FK_TrainerId,FK_LocationId,TrainingDate,StartTime,EndTime,AllowedMembers")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Training.Add(training);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_LocationId = new SelectList(db.Locatie, "LocationId", "LocatieNaam", training.FK_LocationId);
            ViewBag.FK_TrainerId = new SelectList(db.Trainer, "TrainerId", "DisplayName", training.FK_TrainerId);
            return View(training);
        }

        // GET: Trainings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Training.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_LocationId = new SelectList(db.Locatie, "LocationId", "LocatieNaam", training.FK_LocationId);
            ViewBag.FK_TrainerId = new SelectList(db.Trainer, "TrainerId", "DisplayName", training.FK_TrainerId);
            return View(training);
        }

        // POST: Trainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainingId,DisplayTitle,Description,FK_TrainerId,FK_LocationId,TrainingDate,StartTime,EndTime,AllowedMembers")] Training training)
        {
            if (ModelState.IsValid)
            {
                db.Entry(training).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_LocationId = new SelectList(db.Locatie, "LocationId", "LocatieNaam", training.FK_LocationId);
            ViewBag.FK_TrainerId = new SelectList(db.Trainer, "TrainerId", "DisplayName", training.FK_TrainerId);
            return View(training);
        }

        // GET: Trainings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Training training = db.Training.Find(id);
            if (training == null)
            {
                return HttpNotFound();
            }
            return View(training);
        }

        // POST: Trainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Training training = db.Training.Find(id);
            db.Training.Remove(training);
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
