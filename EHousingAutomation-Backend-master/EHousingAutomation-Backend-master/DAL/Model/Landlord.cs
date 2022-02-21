using System.ComponentModel.DataAnnotations;


namespace DAL.Model
{
    public class Landlord
    {
        [Key]
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string email { get; set; }


    }
}
