using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace todo_app.Models
{
    /// <summary>
    /// タスクグループ
    /// </summary>
    public class TaskGroup
    {
        /// <summary>
        /// タスクグループID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// グループ名
        /// </summary>
        [DisplayName("グループ名")]
        public string Name { get; set; }

        /// <summary>
        /// 色
        /// </summary>
        [DisplayName("グループカラー")]
        public TaskGroupColor Color { get; set; } = TaskGroupColor.Gray;

        /// <summary>
        /// タスク一覧
        /// </summary>
        [DisplayName("タスク一覧")]
        public virtual ICollection<Task> Tasks { get; set; }
    }

    /// <summary>
    /// タスクグループの色
    /// </summary>
    public enum TaskGroupColor : byte
    {
        Gray = 0,
        Red = 1,
        Blue = 2,
        Green = 3,
        Yellow = 4,
        Purple = 5
    }
}