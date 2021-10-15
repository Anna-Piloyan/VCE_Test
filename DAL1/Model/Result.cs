using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL1.Model
{
    public class Result
    {
        public int Id { get; set; }
        public DateTime DateUserResult { get; set; }
        public int Mark { get; set; }
        public virtual User User { get; set; }
        public virtual Test Tests { get; set; }
        public int? UserId { get; set; }
        public int? TestId { get; set; }
       

    }
}
