using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CinemaASP.Controllers
{
    public class ActorsController : Controller
    {
        private slabEntities db = new slabEntities();

        public ActionResult Index()
        {
            return View(db.Actors.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actors actors = db.Actors.Find(id);
            if (actors == null)
            {
                return HttpNotFound();
            }
            return View(actors);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,firstName,lastName")] Actors actors)
        {
            if (ModelState.IsValid)
            {
                db.Actors.Add(actors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actors);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actors actors = db.Actors.Find(id);
            if (actors == null)
            {
                return HttpNotFound();
            }
            return View(actors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,firstName,lastName")] Actors actors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actors);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actors actors = db.Actors.Find(id);
            if (actors == null)
            {
                return HttpNotFound();
            }
            return View(actors);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actors actors = db.Actors.Find(id);
            db.Actors.Remove(actors);
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
