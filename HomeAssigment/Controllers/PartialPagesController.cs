using HomeAssigment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeAssigment.Controllers
{
    public class PartialPagesController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PartialPages

     /*   public ActionResult Items()
        {
            var ite
           ms = db.Items.Include(i => i.itemName).Include(i => i.qualityType); return View(db.Items.OrderBy(a => a.ItemDate).ToList()); 
        }

       */
    }
}
