using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using todo_app.Models;

namespace todo_app.Controllers
{
    public class TaskGroupsController : Controller
    {
        private TodoAppContext db = new TodoAppContext();

        // GET: TaskGroups
        public ActionResult Index()
        {
            return View(db.TaskGroups.ToList());
        }

        // GET: TaskGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskGroup taskGroup = db.TaskGroups.Find(id);
            if (taskGroup == null)
            {
                return HttpNotFound();
            }
            return View(taskGroup);
        }

        // GET: TaskGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskGroups/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Color")] TaskGroup taskGroup)
        {
            if (ModelState.IsValid)
            {
                db.TaskGroups.Add(taskGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskGroup);
        }

        // GET: TaskGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskGroup taskGroup = db.TaskGroups.Find(id);
            if (taskGroup == null)
            {
                return HttpNotFound();
            }
            return View(taskGroup);
        }

        // POST: TaskGroups/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Color")] TaskGroup taskGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskGroup);
        }

        // GET: TaskGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskGroup taskGroup = db.TaskGroups.Find(id);
            if (taskGroup == null)
            {
                return HttpNotFound();
            }
            return View(taskGroup);
        }

        // POST: TaskGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskGroup taskGroup = db.TaskGroups.Find(id);
            db.TaskGroups.Remove(taskGroup);
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
