using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Group
    {
        public Group()
        {
            Users = new List<User>();
            TestGroups = new List<TestGroup>();
        }

        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<TestGroup> TestGroups { get; set; }
    }
}
