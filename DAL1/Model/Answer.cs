using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1.Model
{
    public class Answer
    {     
        public int Id { get; set; }

        public string Description { get; set; }

        public bool isRight { get; set; }

        public int? QuestionId { get; set; }
        public Question Questions { get; set; }
        public virtual ICollection<UserAnswer> UserAnswers { get; set; }
        public Answer()
        {
            UserAnswers = new List<UserAnswer>();
        }
    }
}
