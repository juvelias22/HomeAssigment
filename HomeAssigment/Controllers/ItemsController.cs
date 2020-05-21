using HomeAssigment.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using MvcPaging;

using PagedList.Mvc;
namespace HomeAssigment.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.itemName).Include(i => i.qualityType);
            return View(items.ToList());
        }

        public ActionResult PartialPageNewFirst(int? page)
        {

            var items = db.Items.Include(i => i.itemName).Include(i => i.qualityType).OrderByDescending(a => a.ItemDate);

            /*
                        int pageSize = 3;
                        int pageIndex = (page ?? 1);
                        pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                         */
            var pageNumber = page ?? 1;
            //  var onePageOfItems = items.ToPagedList(pageNumber, 25);
            //  return View(items.ToPagedList(pageIndex, pageSize));
            return View(items.ToPagedList(pageNumber, 10));
            // OrderBy(a => a.ItemDate).ToList()
           // return View(items.ToList());
        }

        public ActionResult PartialAddedByUser(int? page)
        {


            var items = db.Items.Include(i => i.itemName).Include(i => i.qualityType).OrderByDescending(a => a.ItemOwner);

            /*
                        int pageSize = 3;
                        int pageIndex = (page ?? 1);
                        pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

                         */
            var pageNumber = page ?? 1;
            //  var onePageOfItems = items.ToPagedList(pageNumber, 25);
            //  return View(items.ToPagedList(pageIndex, pageSize));
            return View(items.ToPagedList(pageNumber, 10));
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // GET: Items/Create
        [Authorize(Roles = "Admin,RegisteredUser")]
        public ActionResult Create()
        {
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "ItemName");
            ViewBag.QualityId = new SelectList(db.Quality, "Id", "QualityType");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,RegisteredUser")]
        public ActionResult Create([Bind(Include = "Id,ItemTypeId,QualityId,ItemQuantity,ItemPrice")] Items items)
        {
            if (ModelState.IsValid)
            {
                items.ItemOwner = User.Identity.Name;
                items.ItemDate = DateTime.Now;
                db.Items.Add(items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "ItemName", items.ItemTypeId);
            ViewBag.QualityId = new SelectList(db.Quality, "Id", "QualityType", items.QualityId);
            return View(items);
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Admin,RegisteredUser")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "ItemName", items.ItemTypeId);
            ViewBag.QualityId = new SelectList(db.Quality, "Id", "QualityType", items.QualityId);
            return View(items);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,RegisteredUser")]
        public ActionResult Edit([Bind(Include = "Id,ItemTypeId,QualityId,ItemQuantity,ItemPrice")] Items items)
        {
            if (ModelState.IsValid)
            {
                items.ItemOwner = User.Identity.Name;
                items.ItemDate = DateTime.Now;
                db.Entry(items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemTypeId = new SelectList(db.ItemTypes, "Id", "ItemName", items.ItemTypeId);
            ViewBag.QualityId = new SelectList(db.Quality, "Id", "QualityType", items.QualityId);
            return View(items);
        }

        // GET: Items/Delete/5
        [Authorize(Roles = "Admin,RegisteredUser")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,RegisteredUser")]
        public ActionResult DeleteConfirmed(int id)
        {
            Items items = db.Items.Find(id);
            db.Items.Remove(items);
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
