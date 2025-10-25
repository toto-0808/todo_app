using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
        /// タスクグループ
        /// </summary>
        public DbSet<TaskGroup> TaskGroups { get; set; }
    }
}