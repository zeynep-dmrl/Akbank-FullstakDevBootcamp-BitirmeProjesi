using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto
{
    public class BillHouseDto
    {
        public int price { get; set; }
        public DateTime monthly { get; set; }
        public string billDesc { get; set; }
        public bool isPaid { get; set; }
    }
}
