using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class MyContextInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            var admin = context.Users.Add(new User() { FirstName = "Anna", LastName = "Ahopshuk", Login = "admin", Password = "admin", isAdmin = true });
            var groupAdmin = context.Groups.Add(new Group() { GroupName = "Admin_Group" });
            groupAdmin.Users.Add(admin);
            context.SaveChanges();

        }
    }
}
