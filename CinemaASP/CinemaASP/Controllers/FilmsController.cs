using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaASP.Controllers
{
    public class FilmsController : Controller
    {
        private slabEntities db = new slabEntities();

        public ActionResult Index()
        {
            return View(db.Films.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Films films = db.Films.Find(id);
            if (films == null)
            {
                return HttpNotFound();
            }
            return View(films);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Year")] Films films)
        {
            if (ModelState.IsValid)
            {
                db.Films.Add(films);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(films);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Films films = db.Films.Find(id);
            if (films == null)
            {
                return HttpNotFound();
            }
            return View(films);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Year")] Films films)
        {
            if (ModelState.IsValid)
            {
                db.Entry(films).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(films);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Films films = db.Films.Find(id);
            if (films == null)
            {
                return HttpNotFound();
            }
            return View(films);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Films films = db.Films.Find(id);
            db.Films.Remove(films);
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
