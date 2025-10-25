using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using todo_app.Models;
using todo_app.ViewModel;

namespace todo_app.Controllers
{
    public class TasksController : Controller
    {
        private TodoAppContext db = new TodoAppContext();

        // GET: Tasks
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View(new TaskViewModel {
                TaskGroupList = db.TaskGroups.Select(g => new SelectListItem {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToList()
            });
        }

        // POST: Tasks/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var task = new Task
                {
                    Group = db.TaskGroups.Find(vm.TaskGroupId),
                    Title = vm.Title,
                    Detail = vm.Detail,
                    DueDate = vm.DueDate,
                    IsStart = vm.IsStart,
                    IsCompleted = vm.IsCompleted
                };

                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            vm.TaskGroupList = db.TaskGroups.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();
            return View(vm);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }

            return View(new TaskViewModel
            {
                Id = task.Id,
                TaskGroupId = task.Group == null ? 0 : task.Group.Id,
                Title = task.Title,
                Detail = task.Detail,
                DueDate = task.DueDate,
                IsStart = task.IsStart,
                TaskGroupList = db.TaskGroups.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToList()
            });
        }

        // POST: Tasks/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Group,Title,Detail,DueDate,IsStart,IsCompleted")] TaskViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var task = new Task
                {
                    Id = vm.Id,
                    // Todo: バグ - 編集時にグループが更新できない
                    Group = db.TaskGroups.Find(vm.TaskGroupId),
                    Title = vm.Title,
                    Detail = vm.Detail,
                    DueDate = vm.DueDate,
                    IsStart = vm.IsStart,
                    IsCompleted = vm.IsCompleted
                };

                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            vm.TaskGroupList = db.TaskGroups.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();
            return View(vm);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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
