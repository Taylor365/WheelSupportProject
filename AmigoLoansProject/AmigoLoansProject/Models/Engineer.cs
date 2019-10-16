using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmigoLoansProject.Models
{
    public class Engineer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int ShiftCount { get; set; }
        public DateTime? LastWorked { get; set; }
    }
}
