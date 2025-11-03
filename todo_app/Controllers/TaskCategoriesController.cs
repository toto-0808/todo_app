using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using todo_app.Infrastructure.EntityFramework;
using todo_app.Domain.Entities;

namespace todo_app.Controllers
{
    /// <summary>
    /// タスクカテゴリコントローラー
    /// </summary>
    public class TaskCategoriesController : Controller
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private TodoAppContext db = new TodoAppContext();

        /// <summary>
        /// Index画面
        /// </summary>
        /// <returns>タスクカテゴリ一覧</returns>
        public ActionResult Index()
        {
            return View(db.TaskCategories.ToList());
        }

        /// <summary>
        /// タスクカテゴリの詳細画面
        /// </summary>
        /// <param name="id">タスクカテゴリID</param>
        /// <returns>タスクカテゴリ</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskCategory taskCategory = db.TaskCategories.Find(id);
            if (taskCategory == null)
            {
                return HttpNotFound();
            }
            return View(taskCategory);
        }

        /// <summary>
        /// タスクカテゴリの作成画面
        /// </summary>
        /// <returns>なし</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskGroups/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        /// <summary>
        /// タスクカテゴリを作成します。
        /// </summary>
        /// <param name="taskCategory">作成するタスクカテゴリ</param>
        /// <returns>タスクカテゴリ</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Color")] TaskCategory taskCategory)
        {
            if (ModelState.IsValid)
            {
                db.TaskCategories.Add(taskCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskCategory);
        }

        // GET: TaskGroups/Edit/5
        /// <summary>
        /// タスクカテゴリの編集画面
        /// </summary>
        /// <param name="id">タスクカテゴリID</param>
        /// <returns>タスクカテゴリ</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskCategory taskCategory = db.TaskCategories.Find(id);
            if (taskCategory == null)
            {
                return HttpNotFound();
            }
            return View(taskCategory);
        }

        // POST: TaskGroups/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        /// <summary>
        /// タスクカテゴリを更新します。
        /// </summary>
        /// <param name="taskCategory">タスクカテゴリ</param>
        /// <returns>タスクカテゴリ</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Color")] TaskCategory taskCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskCategory);
        }

        /// <summary>
        /// タスクカテゴリの削除確認画面
        /// </summary>
        /// <param name="id">タスクカテゴリID</param>
        /// <returns>タスクカテゴリ</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskCategory taskCategory = db.TaskCategories.Find(id);
            if (taskCategory == null)
            {
                return HttpNotFound();
            }
            return View(taskCategory);
        }

        /// <summary>
        /// タスクカテゴリを削除します。
        /// </summary>
        /// <param name="id">タスクカテゴリID</param>
        /// <returns>Index画面に遷移</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskCategory taskCategory = db.TaskCategories.Find(id);
            db.TaskCategories.Remove(taskCategory);
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
