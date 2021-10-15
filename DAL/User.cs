using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(50), MinLength(2) ]
        public string FirstName { get; set; }
        [Required, MaxLength(50), MinLength(2)]
        public string LastName { get; set; }
        [Required, MaxLength(50), MinLength(1), Index(IsUnique = true)]
        public string Login { get; set; }
        [Required, MaxLength(50), MinLength(5)]
        public string Password { get; set; }
        [Required]
        public bool isAdmin { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<UserAnswers> UserAnswers { get; set; }
        public virtual ICollection<Result> Results { get; set; }

        public User()
        {
            Groups = new List<Group>();
            UserAnswers = new List<UserAnswers>();
            Results = new List<Result>();
        }
    }
}
