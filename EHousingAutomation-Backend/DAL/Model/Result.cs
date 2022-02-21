using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Result
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<House> houseList { get; set; }
        public List<User> userList { get; set; }
        public List<Bill> billList { get; set; }
        public List<Dues> duesList { get; set; }
        public List<Message> messageList { get; set; }

    }
}
