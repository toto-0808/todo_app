using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using todo_app.Domain.Entities;

namespace todo_app.Infrastructure.EntityFramework
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
            var taskCategores = new List<TaskCategory>
            {
                new TaskCategory { Name = "Work", Color = TaskCategoryColor.Gray },
                new TaskCategory { Name = "Personal", Color = TaskCategoryColor.Red },
                new TaskCategory { Name = "Fitness", Color = TaskCategoryColor.Blue },
                new TaskCategory { Name = "Hobbies", Color = TaskCategoryColor.Green },
                new TaskCategory { Name = "Errands", Color = TaskCategoryColor.Yellow },
                new TaskCategory { Name = "Others", Color = TaskCategoryColor.Purple }
            };

            var tasks = new List<Task>
            {
                new Task { Title = "Finish report", Category = taskCategores.Single(x => x.Name == "Work"),  Detail = "Complete the quarterly report", DueDate = DateTime.Now.AddDays(3), IsStarted = false, IsCompleted = false },
                new Task { Title = "Grocery shopping", Category = taskCategores.Single(x => x.Name == "Personal"), Detail = "Buy ingredients for dinner", DueDate = DateTime.Now.AddDays(1), IsStarted = false, IsCompleted = false },
                new Task { Title = "Gym session", Category = taskCategores.Single(x => x.Name == "Fitness"),Detail = "Attend yoga class", DueDate = DateTime.Now.AddDays(2), IsStarted = false, IsCompleted = false },
                new Task { Title = "Read a book", Category = taskCategores.Single(x => x.Name == "Hobbies"), Detail = "Finish reading 'The Great Gatsby'", DueDate = DateTime.Now.AddDays(5), IsStarted = false, IsCompleted = false },
                new Task { Title = "Pick up dry cleaning", Category = taskCategores.Single(x => x.Name == "Errands"), Detail = "Collect clothes from the dry cleaner", DueDate = DateTime.Now.AddDays(1), IsStarted = false, IsCompleted = false },
                new Task { Title = "Plan weekend trip", Category = taskCategores.Single(x => x.Name == "Others"), Detail = "Research destinations and accommodations", DueDate = DateTime.Now.AddDays(7), IsStarted = false, IsCompleted = false }
            };

            taskCategores.ForEach(tg => context.TaskCategories.Add(tg));
            tasks.ForEach(t => context.Tasks.Add(t));
            context.SaveChanges();
        }
    }
}