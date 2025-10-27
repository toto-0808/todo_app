using System.Data.Entity;

namespace todo_app.Models
{
    /// <summary>
    /// TodoAppDBコンテキスト
    /// </summary>
    public class TodoAppContext : DbContext
    {
        /// <summary>
        /// タスク
        /// </summary>
        public DbSet<Task> Tasks { get; set; }

        /// <summary>
        /// タスクカテゴリ
        /// </summary>
        public DbSet<TaskCategory> TaskCategories { get; set; }
    }
}