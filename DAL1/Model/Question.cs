using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1.Model
{
    public class Question
    {
       
        public int Id { get; set; }

        public string Description { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public int? TestId { get; set; }

        public Test Tests { get; set; }
        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}
