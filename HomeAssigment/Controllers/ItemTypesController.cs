 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Dropbox.Api;
using Dropbox.Api.Files;
using HomeAssigment.Models;
using Microsoft.Ajax.Utilities;

namespace HomeAssigment.Controllers
{
    [Authorize(Roles = "Admin,RegisteredUser")]
    public class ItemTypesController : Controller
    {
        public static string ImgUrl;
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemTypes
        public ActionResult Index()
        {

            var itemTypes = db.ItemTypes.Include(i => i.category);
            return View(itemTypes.ToList());
        }
      
      
        // GET: ItemTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = db.ItemTypes.Find(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            return View(itemType);
        }


        // GET: ItemTypes/Create
   
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Category");
            return View();
        }

        // POST: ItemTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,ItemName")] ItemType itemType, HttpPostedFileBase file)
        {
            string check = UploadImage(file);
            if (check  != null)
            {
                if (ModelState.IsValid)
                {
                    itemType.ImagePath = check;
                    db.ItemTypes.Add(itemType);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Category", itemType.CategoryId);
            return View(itemType);
        }

        static string ApplicationName = "Enterprise-Home-Assigment";


        public string UploadImage(HttpPostedFileBase file)
        {
            string accessToken = "SW-I0OEdJxAAAAAAAAAAuG6uoYzI4hu-S2eQunotCGQEtvtlgW5y2q0pYq4jFD-I";
            //   HttpPostedFileBase file;
            using (DropboxClient client =
                new DropboxClient(accessToken, new DropboxClientConfig(ApplicationName)))
            {
            

                    string[] splitInputFileName = file.FileName.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
                    string fileNameAndExtension = splitInputFileName[splitInputFileName.Length - 1];

                    string[] fileNameAndExtensionSplit = fileNameAndExtension.Split('.');
                    string originalFileName = fileNameAndExtensionSplit[0];
                    string originalExtension = fileNameAndExtensionSplit[1];
                    System.Diagnostics.Debug.WriteLine(originalExtension);

                    if (originalExtension == "jpg" || originalExtension == "jpeg")
                    {
                        string fileName = @"/Images/" + originalFileName + Guid.NewGuid().ToString().Replace("-", "") + "." + originalExtension;



                        var updated = client.Files.UploadAsync(fileName,
                                                                mode: WriteMode.Overwrite.Instance,
                                                                body: file.InputStream).Result;

                        var result = client.Sharing.CreateSharedLinkWithSettingsAsync(fileName).Result;
                        ;
                        //System.Diagnostics.Debug.WriteLine(ImgUrl);

                        // ImageUrl = result.Url
                        //  return View(itemType, new { ImageUrl = result.Url });
                        return result.Url.Replace("?dl=0", "?raw=1");
                    }
                    else
                    {
                        return null;
                    }
        
            }

        }





        public ActionResult ViewImage(string imageUrl)
        {

            throw new NotImplementedException();
        }



        // GET: ItemTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = db.ItemTypes.Find(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Category", itemType.CategoryId);
            return View(itemType);
        }

        // POST: ItemTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,ItemName,ImagePath")] ItemType itemType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Category", itemType.CategoryId);
            return View(itemType);
        }

        // GET: ItemTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemType itemType = db.ItemTypes.Find(id);
            if (itemType == null)
            {
                return HttpNotFound();
            }
            return View(itemType);
        }

        // POST: ItemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemType itemType = db.ItemTypes.Find(id);
            db.ItemTypes.Remove(itemType);
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
