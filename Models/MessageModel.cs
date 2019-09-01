using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example.Models
{
    public class MessageModel
    {          
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
        public MessageStatus Status { get; set; } // Not really needed. Kind of a false start.
        public DateTime CreateDate { get; set; }
        public MessageModel() // False start. Went a different direction.
        {
            Status = MessageStatus.Sent;
        }            
        public enum MessageStatus // Falst start. Went a different direction.
        {
            Sent, 
            Delivered
        }  
    }
}