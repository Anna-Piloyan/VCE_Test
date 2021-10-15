using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Tests
    {
        public Tests()
        {
            Questions = new List<Questions>();
            TestGroup = new List<TestGroup>();
            Results = new List<Result>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
        public int QtyOfQuestions { get; set; }
        public int Difficulty { get; set; }

        public ICollection<Questions> Questions { get; set; }
        public ICollection<TestGroup> TestGroup { get; set; }
        public ICollection<Result> Results { get; set; }

    }
}
