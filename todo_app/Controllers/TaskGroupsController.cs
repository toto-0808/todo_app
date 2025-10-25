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
    /// <summary>
    /// タスクグループコントローラー
    /// </summary>
    public class TaskGroupsController : Controller
    {
        /// <summary>
        /// DBコンテキスト
        /// </summary>
        private TodoAppContext db = new TodoAppContext();

        /// <summary>
        /// Index画面
        /// </summary>
        /// <returns>タスクグループ一覧</returns>
        public ActionResult Index()
        {
            return View(db.TaskGroups.ToList());
        }

        /// <summary>
        /// タスクグループの詳細画面
        /// </summary>
        /// <param name="id">タスクグループID</param>
        /// <returns>タスクグループ</returns>
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

        /// <summary>
        /// タスクグループの作成画面
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
        /// タスクグループを作成します。
        /// </summary>
        /// <param name="taskGroup">作成するタスクグループ</param>
        /// <returns>タスクグループ</returns>
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
        /// <summary>
        /// タスクグループの編集画面
        /// </summary>
        /// <param name="id">タスクグループID</param>
        /// <returns>タスクグループ</returns>
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
        /// <summary>
        /// タスクグループを更新します。
        /// </summary>
        /// <param name="taskGroup">タスクグループ</param>
        /// <returns>タスクグループ</returns>
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

        /// <summary>
        /// タスクグループの削除確認画面
        /// </summary>
        /// <param name="id">タスクグループID</param>
        /// <returns>タスクグループ</returns>
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

        /// <summary>
        /// タスクグループを削除します。
        /// </summary>
        /// <param name="id">タスクグループID</param>
        /// <returns>Index画面に遷移</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskGroup taskGroup = db.TaskGroups.Find(id);
            db.TaskGroups.Remove(taskGroup);
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
