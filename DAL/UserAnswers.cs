using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserAnswers
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Answers Answers { get; set; }
        public int? UserId { get; set; }
        public int? AnswersId { get; set; }
        public DateTime DateUserAnswer { get; set; }
    }
}
