using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1.Model
{
    public class TestGroup
    {
        public int Id { get; set; }
        public DateTime DateTestGroup { get; set; }

        public virtual Test Tests { get; set; }
      
        public virtual Group Groups { get; set; }
        public int? GroupId { get; set; }
        public int? TestId { get; set; }
       

    }
}
