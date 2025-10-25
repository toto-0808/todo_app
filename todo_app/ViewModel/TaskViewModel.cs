using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using todo_app.Models;

namespace todo_app.ViewModel
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
        /// タスクグループID
        /// </summary>
        [DisplayName("タスクグループID")]
        public int TaskGroupId { get; set; }

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
        public DateTime DueDate { get; set; }

        /// <summary>
        /// 着手フラグ
        /// </summary>
        [DisplayName("着手したか？")]
        public bool IsStart { get; set; }

        /// <summary>
        /// 完了フラグ
        /// </summary>
        [DisplayName("完了したか？")]
        public bool IsCompleted { get; set; }

        /// <summary>
        /// タスクグループ一覧
        /// </summary>
        public IEnumerable<SelectListItem> TaskGroupList { get; set; } = new List<SelectListItem>();
    }
}