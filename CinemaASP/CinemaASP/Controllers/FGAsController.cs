using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaASP.Controllers
{
    public class FGAsController : Controller
    {
        private slabEntities db = new slabEntities();

        public ActionResult Index()
        {
            var fGAs = db.FGAs.Include(f => f.Actors).Include(f => f.Films).Include(f => f.Genres);
            return View(fGAs.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FGAs fGAs = db.FGAs.Find(id);
            if (fGAs == null)
            {
                return HttpNotFound();
            }
            return View(fGAs);
        }

        public ActionResult Create()
        {
            ViewBag.Actor_Id = new SelectList(db.Actors, "Id", "firstName");
            ViewBag.Film_Id = new SelectList(db.Films, "Id", "Name");
            ViewBag.Genre_Id = new SelectList(db.Genres, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Film_Id,Genre_Id,Actor_Id")] FGAs fGAs)
        {
            if (ModelState.IsValid)
            {
                db.FGAs.Add(fGAs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Actor_Id = new SelectList(db.Actors, "Id", "firstName", fGAs.Actor_Id);
            ViewBag.Film_Id = new SelectList(db.Films, "Id", "Name", fGAs.Film_Id);
            ViewBag.Genre_Id = new SelectList(db.Genres, "Id", "Name", fGAs.Genre_Id);
            return View(fGAs);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FGAs fGAs = db.FGAs.Find(id);
            if (fGAs == null)
            {
                return HttpNotFound();
            }
            ViewBag.Actor_Id = new SelectList(db.Actors, "Id", "firstName", fGAs.Actor_Id);
            ViewBag.Film_Id = new SelectList(db.Films, "Id", "Name", fGAs.Film_Id);
            ViewBag.Genre_Id = new SelectList(db.Genres, "Id", "Name", fGAs.Genre_Id);
            return View(fGAs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Film_Id,Genre_Id,Actor_Id")] FGAs fGAs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fGAs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Actor_Id = new SelectList(db.Actors, "Id", "firstName", fGAs.Actor_Id);
            ViewBag.Film_Id = new SelectList(db.Films, "Id", "Name", fGAs.Film_Id);
            ViewBag.Genre_Id = new SelectList(db.Genres, "Id", "Name", fGAs.Genre_Id);
            return View(fGAs);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FGAs fGAs = db.FGAs.Find(id);
            if (fGAs == null)
            {
                return HttpNotFound();
            }
            return View(fGAs);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FGAs fGAs = db.FGAs.Find(id);
            db.FGAs.Remove(fGAs);
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
