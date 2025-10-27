using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace todo_app.ViewModels
{
    /// <summary>
    /// タスク作成用のViewModel
    /// </summary>
    public class TaskViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// タスクカテゴリID
        /// </summary>
        public int TaskCategoryId { get; set; }

        /// <summary>
        /// 概要
        /// </summary>
        [DisplayName("概要")]
        public string Title { get; set; }

        /// <summary>
        /// 詳細
        /// </summary>
        [DisplayName("詳細")]
        public string Detail { get; set; }

        /// <summary>
        /// 期限日
        /// </summary>
        [DisplayName("期限日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        /// <summary>
        /// 着手フラグ
        /// </summary>
        [DisplayName("着手したか？")]
        public bool IsStarted { get; set; }

        /// <summary>
        /// 完了フラグ
        /// </summary>
        [DisplayName("完了したか？")]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// タスクカテゴリ一覧
        /// </summary>
        [DisplayName("カテゴリ")]
        public IEnumerable<SelectListItem> TaskCategoryList { get; set; } = new List<SelectListItem>();
    }
}