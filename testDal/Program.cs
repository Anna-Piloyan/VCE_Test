using DAL1;
using DAL1.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testDal
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyDbContext context = new MyDbContext("conStr"))
            {
                Group group = new Group() { GroupName = "PapaGod" };
                context.Groups.Add(group);
                context.SaveChanges();
            }
        }
    }
}
