using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Dto
{
    public class MessageUserDto
    {
        public string message { get; set; }
        public bool isRead { get; set; }
        public string user { get; set; }
    }
}
