using DAL1.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(string conStr)
           : base(conStr)
        { }
        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(new MyContextInitializer());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestGroup> TestGroups { get; set; }
    }
}
