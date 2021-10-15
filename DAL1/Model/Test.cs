using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1.Model
{
    public class Test
    {
       
        public int Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }
        public int QtyOfQuestions { get; set; }
        public int Difficulty { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ICollection<TestGroup> TestGroups { get; set; }
        public ICollection<Result> Results { get; set; }
        public Test()
        {
            Questions = new List<Question>();
            TestGroups = new List<TestGroup>();
            Results = new List<Result>();
        }

    }
}
