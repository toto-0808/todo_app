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
            var tasks = new List<Task>
            {
                new Task { GroupName = "Work", Title = "Finish report", Detail = "Complete the quarterly report", DueDate = DateTime.Now.AddDays(3), IsStart = false, IsCompleted = false },
                new Task { GroupName = "Home", Title = "Grocery shopping", Detail = "Buy ingredients for dinner", DueDate = DateTime.Now.AddDays(1), IsStart = false, IsCompleted = false },
                new Task { GroupName = "Personal", Title = "Gym session", Detail = "Attend yoga class", DueDate = DateTime.Now.AddDays(2), IsStart = false, IsCompleted = false }
            };
            tasks.ForEach(t => context.Tasks.Add(t));
            context.SaveChanges();
        }
    }
}