using System;
using System.ComponentModel.DataAnnotations;

namespace SlackDAW1.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        [Required(ErrorMessage = "Message body is required")]
        [StringLength(1000, ErrorMessage = "Message cannot be longer than 1000 characters")]
        [MinLength(10, ErrorMessage = "Minim 10 caractere")]
        public string Body { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "Sender is required")]
        public int SenderID { get; set; }

        [Required(ErrorMessage = "Channel is required")]
        public int? ChannelID { get; set; }
        public virtual Channel? Channel { get; set; }
    }
}
