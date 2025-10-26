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
    /// <summary>
    /// タスクコントローラー
    /// </summary>
    public class TasksController : Controller
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private TodoAppContext db = new TodoAppContext();

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <returns>タスク一覧</returns>
        public ActionResult Index()
        {
            return View(db.Tasks.ToList());
        }

        /// <summary>
        /// タスクの詳細画面
        /// </summary>
        /// <param name="id">タスクID</param>
        /// <returns>タスク</returns>
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

        /// <summary>
        /// タスクの作成画面
        /// </summary>
        /// <returns>空のタスクとタスクグループ一覧</returns>
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
        /// <summary>
        /// タスクを作成します。
        /// </summary>
        /// <param name="vm">追加するタスク</param>
        /// <returns>成功：Index画面へのリダイレクト、失敗：ViewModel</returns>
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
                    IsStarted = vm.IsStart,
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

        /// <summary>
        ///タスクの編集画面
        /// </summary>
        /// <param name="id">タスクID</param>
        /// <returns>編集対象のタスクとタスクグループ一覧</returns>
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
                IsStart = task.IsStarted,
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
        /// <summary>
        /// タスクを更新します。
        /// </summary>
        /// <param name="vm">編集後のタスク</param>
        /// <returns>成功：Index画面に遷移、失敗：ViewModel</returns>
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
                    IsStarted = vm.IsStart,
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

        /// <summary>
        /// タスクの削除確認画面
        /// </summary>
        /// <param name="id">削除対象のタスクID</param>
        /// <returns>削除対象のタスク</returns>
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

        /// <summary>
        /// タスクを削除します。
        /// </summary>
        /// <param name="id">削除対象のタスクID</param>
        /// <returns>Index画面に遷移</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// DBコンテキストを破棄します。
        /// </summary>
        /// <param name="disposing">破棄するかどうか</param>
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
