using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TestGroup
    {
        public int Id { get; set; }
        public int? TestsId { get; set; }

        public virtual Tests Tests { get; set; }
        public int? GroupId { get; set; }
        public virtual Group Groups { get; set; }


        public DateTime DateTestGroup { get; set; }

    }
}
