using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DAL.Model
{
    public class Message
    {
        [Key]
        public int messageId { get; set; }
        public string message { get; set; }
        public DateTime timestamp { get; set; }
        public bool isRead { get; set; }

        [ForeignKey("userId")]
        public int userId { get; set; }
    }
}
