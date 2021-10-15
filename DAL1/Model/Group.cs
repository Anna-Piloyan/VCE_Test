using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1.Model
{
    public class Group
    {
       
        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Group()
        {
            Users = new List<User>();
        }

    }
}
