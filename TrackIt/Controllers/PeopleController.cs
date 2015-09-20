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
    public class PeopleController : Controller
    {
        private FellowshipEntities db = new FellowshipEntities();

        // GET: People
        public ActionResult Index()
        {
            var people = db.People.Include(p => p.Address).Include(p => p.PersonBank).Include(p => p.PersonAddress).Include(p => p.PersonOrganization);
            return View(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.PersonCareOfAddressId = new SelectList(db.Addresses, "AddressId", "AddressLine1");
            ViewBag.PersonBankId = new SelectList(db.PersonBanks, "PersonBankId", "PersonBankId");
            ViewBag.PersonAddressId = new SelectList(db.PersonAddresses, "PersonAddressId", "PersonAddressId");
            ViewBag.PersonOrganizationId = new SelectList(db.PersonOrganizations, "PersonOrganizationId", "PersonOrganizationId");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonId,FirstName,MiddleName,LastName,DisplayName,AlternateName,Title,Position,Role,Notes,PersonAddressId,PersonBankId,PersonOrganizationId,CreatedById,CreatedByDate,UpdatedById,UpdatedByDate,DeletedBy,DeletedByDate,IsDeleted,PersonCareOfAddressId")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonCareOfAddressId = new SelectList(db.Addresses, "AddressId", "AddressLine1", person.PersonCareOfAddressId);
            ViewBag.PersonBankId = new SelectList(db.PersonBanks, "PersonBankId", "PersonBankId", person.PersonBankId);
            ViewBag.PersonAddressId = new SelectList(db.PersonAddresses, "PersonAddressId", "PersonAddressId", person.PersonAddressId);
            ViewBag.PersonOrganizationId = new SelectList(db.PersonOrganizations, "PersonOrganizationId", "PersonOrganizationId", person.PersonOrganizationId);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonCareOfAddressId = new SelectList(db.Addresses, "AddressId", "AddressLine1", person.PersonCareOfAddressId);
            ViewBag.PersonBankId = new SelectList(db.PersonBanks, "PersonBankId", "PersonBankId", person.PersonBankId);
            ViewBag.PersonAddressId = new SelectList(db.PersonAddresses, "PersonAddressId", "PersonAddressId", person.PersonAddressId);
            ViewBag.PersonOrganizationId = new SelectList(db.PersonOrganizations, "PersonOrganizationId", "PersonOrganizationId", person.PersonOrganizationId);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonId,FirstName,MiddleName,LastName,DisplayName,AlternateName,Title,Position,Role,Notes,PersonAddressId,PersonBankId,PersonOrganizationId,CreatedById,CreatedByDate,UpdatedById,UpdatedByDate,DeletedBy,DeletedByDate,IsDeleted,PersonCareOfAddressId")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonCareOfAddressId = new SelectList(db.Addresses, "AddressId", "AddressLine1", person.PersonCareOfAddressId);
            ViewBag.PersonBankId = new SelectList(db.PersonBanks, "PersonBankId", "PersonBankId", person.PersonBankId);
            ViewBag.PersonAddressId = new SelectList(db.PersonAddresses, "PersonAddressId", "PersonAddressId", person.PersonAddressId);
            ViewBag.PersonOrganizationId = new SelectList(db.PersonOrganizations, "PersonOrganizationId", "PersonOrganizationId", person.PersonOrganizationId);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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
