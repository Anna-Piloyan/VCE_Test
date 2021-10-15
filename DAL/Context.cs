using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Context : DbContext
    {  
        public Context(string conStr)
           : base(conStr)
        { }
        static Context()
        {
            Database.SetInitializer<Context>(new MyContextInitializer());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<UserAnswers> UserAnswers { get; set; }
        public DbSet<Tests> Tests { get; set; }
        public DbSet<TestGroup> TestGroups { get; set; }
    }
}
