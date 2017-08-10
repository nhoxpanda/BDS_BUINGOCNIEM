using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PROJECTBDS.Areas.Admin.Models;
using PROJECTBDS.Models;

namespace PROJECTBDS.Areas.Admin.Controllers
{
    public class ProjectDetailsController : Controller
    {
        private LandSoftEntities db = new LandSoftEntities();

        // GET: ProjectDetails
        public ActionResult Index()
        {
            var tblProjectDetail = db.tblProjectDetail.Include(t => t.tblDictionary).Include(t => t.tblProject);
            return View(tblProjectDetail.ToList());
        }

        // GET: ProjectDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblProjectDetail = db.tblProjectDetail.FirstOrDefault(t=>t.ProjectId == id);

            if (tblProjectDetail == null)
            {
                return RedirectToAction("Create", "ProjectDetails", new {id = id});
            }
            return View(tblProjectDetail);
        }

        // GET: ProjectDetails/Create
        public ActionResult Create(int? id)
        {
            var project = db.tblProject.FirstOrDefault(t => t.Id == id);
            if (project == null) return HttpNotFound();
            ViewBag.Project = project;
            ViewBag.DictionaryId = new SelectList(db.tblDictionary.Where(t=>t.CategoryId == (int)EnumCategory.MenuDuAn), "Id", "Title");
            var projectDetail = db.tblProjectDetail.FirstOrDefault(t => t.ProjectId == project.Id && t.DictionaryId == 35);
            return View(projectDetail ?? new tblProjectDetail());
        }

        [ValidateInput(false)]
        // POST: ProjectDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,DictionaryId,Title,Content,Orders")] tblProjectDetail tblProjectDetail)
        {
            using (var d = new LandSoftEntities())
            {
                var detail =
                    d.tblProjectDetail.Where(
                        t =>
                            t.ProjectId == tblProjectDetail.ProjectId &&
                            t.DictionaryId == tblProjectDetail.DictionaryId);

                if (detail.Any())
                {
                    foreach (var item in detail)
                    {
                        d.tblProjectDetail.Remove(item);
                    }

                    d.SaveChanges();
                }
            }
            
                db.tblProjectDetail.Add(tblProjectDetail);
                db.SaveChanges();
               
            

            ViewBag.DictionaryId = new SelectList(db.tblDictionary, "Id", "Title", tblProjectDetail.DictionaryId);
            ViewBag.ProjectId = new SelectList(db.tblProject, "Id", "Title", tblProjectDetail.ProjectId);
            return RedirectToAction("Create", new {id = tblProjectDetail.ProjectId});
        }

        // GET: ProjectDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProjectDetail tblProjectDetail = db.tblProjectDetail.Find(id);
            if (tblProjectDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DictionaryId = new SelectList(db.tblDictionary, "Id", "Title", tblProjectDetail.DictionaryId);
            ViewBag.ProjectId = new SelectList(db.tblProject, "Id", "Title", tblProjectDetail.ProjectId);
            return View(tblProjectDetail);
        }

        // POST: ProjectDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,DictionaryId,Title,Content,Orders")] tblProjectDetail tblProjectDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProjectDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DictionaryId = new SelectList(db.tblDictionary, "Id", "Title", tblProjectDetail.DictionaryId);
            ViewBag.ProjectId = new SelectList(db.tblProject, "Id", "Title", tblProjectDetail.ProjectId);
            return View(tblProjectDetail);
        }

        // GET: ProjectDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProjectDetail tblProjectDetail = db.tblProjectDetail.Find(id);
            if (tblProjectDetail == null)
            {
                return HttpNotFound();
            }
            return View(tblProjectDetail);
        }

        // POST: ProjectDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProjectDetail tblProjectDetail = db.tblProjectDetail.Find(id);
            db.tblProjectDetail.Remove(tblProjectDetail);
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
