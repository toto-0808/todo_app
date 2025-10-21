using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace todo_app.Models
{
    public class TodoAppInitializer : DropCreateDatabaseIfModelChanges<TodoAppContext>
    {
        protected override void Seed(TodoAppContext context)
        {
            var taskGroups = new List<TaskGroup>
            {
                new TaskGroup { Name = "Work", Color = TaskGroupColor.Blue },
                new TaskGroup { Name = "Personal", Color = TaskGroupColor.Blue },
                new TaskGroup { Name = "Fitness", Color = TaskGroupColor.Blue }
            };

            var tasks = new List<Task>
            {
                new Task { Title = "Finish report", Group = taskGroups.Single(x => x.Name == "Work"),  Detail = "Complete the quarterly report", DueDate = DateTime.Now.AddDays(3), IsStart = false, IsCompleted = false },
                new Task { Title = "Grocery shopping", Detail = "Buy ingredients for dinner", DueDate = DateTime.Now.AddDays(1), IsStart = false, IsCompleted = false },
                new Task { Title = "Gym session", Detail = "Attend yoga class", DueDate = DateTime.Now.AddDays(2), IsStart = false, IsCompleted = false }
            };

            taskGroups.ForEach(tg => context.TaskGroups.Add(tg));
            tasks.ForEach(t => context.Tasks.Add(t));
            context.SaveChanges();
        }
    }
}