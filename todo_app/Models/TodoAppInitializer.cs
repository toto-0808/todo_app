using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace todo_app.Models
{
    /// <summary>
    /// DB初期化クラス
    /// </summary>
    public class TodoAppInitializer : DropCreateDatabaseIfModelChanges<TodoAppContext>
    {
        /// <summary>
        /// 初期化します。
        /// </summary>
        /// <param name="context">DBコンテキスト</param>
        protected override void Seed(TodoAppContext context)
        {
            var taskGroups = new List<TaskGroup>
            {
                new TaskGroup { Name = "Work", Color = TaskGroupColor.Gray },
                new TaskGroup { Name = "Personal", Color = TaskGroupColor.Red },
                new TaskGroup { Name = "Fitness", Color = TaskGroupColor.Blue },
                new TaskGroup { Name = "Hobbies", Color = TaskGroupColor.Green },
                new TaskGroup { Name = "Errands", Color = TaskGroupColor.Yellow },
                new TaskGroup { Name = "Others", Color = TaskGroupColor.Purple }
            };

            var tasks = new List<Task>
            {
                new Task { Title = "Finish report", Group = taskGroups.Single(x => x.Name == "Work"),  Detail = "Complete the quarterly report", DueDate = DateTime.Now.AddDays(3), IsStarted = false, IsCompleted = false },
                new Task { Title = "Grocery shopping", Group = taskGroups.Single(x => x.Name == "Personal"), Detail = "Buy ingredients for dinner", DueDate = DateTime.Now.AddDays(1), IsStarted = false, IsCompleted = false },
                new Task { Title = "Gym session", Group = taskGroups.Single(x => x.Name == "Fitness"),Detail = "Attend yoga class", DueDate = DateTime.Now.AddDays(2), IsStarted = false, IsCompleted = false },
                new Task { Title = "Read a book", Group = taskGroups.Single(x => x.Name == "Hobbies"), Detail = "Finish reading 'The Great Gatsby'", DueDate = DateTime.Now.AddDays(5), IsStarted = false, IsCompleted = false },
                new Task { Title = "Pick up dry cleaning", Group = taskGroups.Single(x => x.Name == "Errands"), Detail = "Collect clothes from the dry cleaner", DueDate = DateTime.Now.AddDays(1), IsStarted = false, IsCompleted = false },
                new Task { Title = "Plan weekend trip", Group = taskGroups.Single(x => x.Name == "Others"), Detail = "Research destinations and accommodations", DueDate = DateTime.Now.AddDays(7), IsStarted = false, IsCompleted = false }
            };

            taskGroups.ForEach(tg => context.TaskGroups.Add(tg));
            tasks.ForEach(t => context.Tasks.Add(t));
            context.SaveChanges();
        }
    }
}