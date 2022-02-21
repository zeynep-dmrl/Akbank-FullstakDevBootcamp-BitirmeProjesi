using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto
{
    public class HouseUserDto
    {
        public int Id { get; set; }
        public string block { get; set; }
        public int floor { get; set; }
        public int aptNo { get; set; }
        public string isFull { get; set; }
        public string ownerOrtenant { get; set; }
    }
}
