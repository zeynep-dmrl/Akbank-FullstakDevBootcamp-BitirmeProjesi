using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string tcNo  { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string carInfo { get; set; }
        //arac bilgisi plakaNO

        public string password { get; set; }

    }
}
