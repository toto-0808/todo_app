using System.Collections.Generic;
using System.ComponentModel;

namespace todo_app.Models
{
    /// <summary>
    /// タスクカテゴリ
    /// </summary>
    public class TaskCategory
    {
        /// <summary>
        /// タスクカテゴリID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// カテゴリ名
        /// </summary>
        [DisplayName("カテゴリ名")]
        public string Name { get; set; }

        /// <summary>
        /// 色
        /// </summary>
        [DisplayName("カテゴリカラー")]
        public TaskCategoryColor Color { get; set; } = TaskCategoryColor.Gray;

        /// <summary>
        /// タスク一覧
        /// </summary>
        [DisplayName("タスク一覧")]
        public virtual ICollection<Task> Tasks { get; set; }
    }

    /// <summary>
    /// タスクカテゴリの色
    /// </summary>
    public enum TaskCategoryColor : byte
    {
        Gray = 0,
        Red = 1,
        Blue = 2,
        Green = 3,
        Yellow = 4,
        Purple = 5
    }
}