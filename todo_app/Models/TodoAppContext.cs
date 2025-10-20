using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace todo_app.Models
{
    public class TodoAppContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }
}