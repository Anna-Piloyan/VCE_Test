using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL1.Model;

namespace DAL1
{
    class MyContextInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            var admin = context.Users.Add(new User() { FirstName = "Anna", LastName = "Ahopshuk", Login = "admin", Password = "admin", isAdmin = true });
            var groupAdmin = context.Groups.Add(new Group() { GroupName = "Admin_Group" });
            groupAdmin.Users.Add(admin);
            context.SaveChanges();

        }
    }
}
