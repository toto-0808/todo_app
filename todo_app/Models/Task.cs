using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace todo_app.Models
{
    /// <summary>
    /// タスク
    /// </summary>
    public class Task
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// グループ名
        /// </summary>
        [DisplayName("タスクグループ")]
        public virtual TaskGroup Group { get; set; }

        /// <summary>
        /// タスクグループ
        /// </summary>
        public long? TaskGroupId { get { return Group != null ? Group.Id : (long?)null; } }

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
        public DateTime DueDate { get; set; }

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
    }
}