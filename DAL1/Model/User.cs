using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public User()
        {
            Groups = new List<Group>();
            UserAnswers = new List<UserAnswer>();
            Results = new List<Result>();
        }
    }
}
