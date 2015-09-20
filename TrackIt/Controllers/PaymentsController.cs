using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrackIt.Models;

namespace TrackIt.Controllers
{
    public class PaymentsController : Controller
    {
        private FellowshipEntities db = new FellowshipEntities();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.PersonBank).Include(p => p.Person);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.PersonBankId = new SelectList(db.PersonBanks, "PersonBankId", "PersonBankId");
            ViewBag.RecipientId = new SelectList(db.People, "PersonId", "FirstName");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,RecipientId,PersonBankId,Amount,Description,CheckNumber,CheckedSignedById,CheckDate,PaymentSentBy,PaymentById,IsCleared,IsClearedDate,IsAcknowledged,IsAcknowledgedDate,Notes,CreatedByUserId,CreatedByDate,UpdatedByUserId,UpdatedByDate,DeletedByUserId,DeletedByDate,IsDeleted")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonBankId = new SelectList(db.PersonBanks, "PersonBankId", "PersonBankId", payment.PersonBankId);
            ViewBag.RecipientId = new SelectList(db.People, "PersonId", "FirstName", payment.RecipientId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonBankId = new SelectList(db.PersonBanks, "PersonBankId", "PersonBankId", payment.PersonBankId);
            ViewBag.RecipientId = new SelectList(db.People, "PersonId", "FirstName", payment.RecipientId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,RecipientId,PersonBankId,Amount,Description,CheckNumber,CheckedSignedById,CheckDate,PaymentSentBy,PaymentById,IsCleared,IsClearedDate,IsAcknowledged,IsAcknowledgedDate,Notes,CreatedByUserId,CreatedByDate,UpdatedByUserId,UpdatedByDate,DeletedByUserId,DeletedByDate,IsDeleted")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonBankId = new SelectList(db.PersonBanks, "PersonBankId", "PersonBankId", payment.PersonBankId);
            ViewBag.RecipientId = new SelectList(db.People, "PersonId", "FirstName", payment.RecipientId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
