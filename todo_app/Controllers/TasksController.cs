using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using todo_app.Models;
using todo_app.ViewModels;

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
        public ActionResult Index(SearchTaskViewModel searchTaskViewModel)
        {
            if(searchTaskViewModel == null)
            {
                return View(new SearchTaskViewModel
                {
                    TaskList = db.Tasks.ToList(),
                    TaskCategoryList = db.TaskCategories.Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name
                    }).ToList(),
                });
            }

            searchTaskViewModel.TaskList = this.FilterTasks(searchTaskViewModel).ToList();
            searchTaskViewModel.TaskCategoryList = db.TaskCategories.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            return View(searchTaskViewModel);
        }

        /// <summary>
        /// タスクを検索します。
        /// </summary>
        /// <param name="searchTaskViewModel">タスク検索用のViewModel</param>
        /// <returns>クエリ</returns>
        private IQueryable<Task> FilterTasks(SearchTaskViewModel searchTaskViewModel)
        {
            var query = db.Tasks.AsQueryable();
            if (searchTaskViewModel.TaskCategoryId != null)
            {
                query = query.Where(t => t.Category.Id == searchTaskViewModel.TaskCategoryId);
            }

            if (!string.IsNullOrEmpty(searchTaskViewModel.Title))
            {
                query = query.Where(t => t.Title.Contains(searchTaskViewModel.Title));
            }

            if (!string.IsNullOrEmpty(searchTaskViewModel.Detail))
            {
                query = query.Where(t => t.Detail.Contains(searchTaskViewModel.Detail));
            }

            if (searchTaskViewModel.DueDateFrom.HasValue)
            {
                query = query.Where(t => t.DueDate >= searchTaskViewModel.DueDateFrom.Value.DateTime);
            }

            if (searchTaskViewModel.DueDateTo.HasValue)
            {
                query = query.Where(t => t.DueDate <= searchTaskViewModel.DueDateTo.Value.DateTime);
            }

            if (searchTaskViewModel.IsStarted.HasValue)
            {
                query = query.Where(t => t.IsStarted == searchTaskViewModel.IsStarted.Value);
            }

            if (searchTaskViewModel.IsCompleted.HasValue)
            {
                query = query.Where(t => t.IsCompleted == searchTaskViewModel.IsCompleted.Value);
            }

            return query;
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
        /// <returns>空のタスクとタスクカテゴリ一覧</returns>
        public ActionResult Create()
        {
            return View(new TaskViewModel {
                TaskCategoryList = db.TaskCategories.Select(g => new SelectListItem {
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
                    Category = db.TaskCategories.Find(vm.TaskCategoryId),
                    Title = vm.Title,
                    Detail = vm.Detail,
                    DueDate = vm.DueDate,
                    IsStarted = vm.IsStarted,
                    IsCompleted = vm.IsCompleted
                };

                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            vm.TaskCategoryList = db.TaskCategories.Select(g => new SelectListItem
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
        /// <returns>編集対象のタスクとタスクカテゴリ一覧</returns>
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
                TaskCategoryId = task.Category == null ? 0 : task.Category.Id,
                Title = task.Title,
                Detail = task.Detail,
                DueDate = task.DueDate,
                IsStarted = task.IsStarted,
                TaskCategoryList = db.TaskCategories.Select(g => new SelectListItem
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
        public ActionResult Edit(TaskViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // カテゴリを更新するためにデータを明示的に更新する
                var updateTask = db.Tasks.Find(vm.Id);
                updateTask.Category = db.TaskCategories.Find(vm.TaskCategoryId);
                updateTask.Title = vm.Title;
                updateTask.Detail = vm.Detail;
                updateTask.DueDate = vm.DueDate;
                updateTask.IsStarted = vm.IsStarted;
                updateTask.IsCompleted = vm.IsCompleted;

                db.Tasks.AddOrUpdate(updateTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            vm.TaskCategoryList = db.TaskCategories.Select(g => new SelectListItem
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
