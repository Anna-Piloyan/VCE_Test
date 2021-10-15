using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Result
    {
        public int Id { get; set; }
        public DateTime DateUserAnswer { get; set; }
        public virtual User User { get; set; }
        public virtual Tests Tests { get; set; }
        public int? UserId { get; set; }
        public int? TestsId { get; set; }
        public int Mark { get; set; }

    }
}
