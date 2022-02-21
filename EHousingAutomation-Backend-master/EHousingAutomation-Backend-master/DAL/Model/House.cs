using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class House
    {
        [Key]
        public int HouseID { get; set; }
        public string block { get; set; }
        public int floor { get; set; }
        public int aptNo { get; set; }
        public string isFull { get; set; }

        [ForeignKey("userId")]
        public int userId { get; set; }//owner-tenant
        //string status { get; set; } 
    }
}
