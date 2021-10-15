using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1.Model
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public DateTime DateUserAnswer { get; set; }
        public virtual User User { get; set; }
        public virtual Answer Answers { get; set; }
        public int? UserId { get; set; }
        public int? AnswerId { get; set; }
       
    }
}
