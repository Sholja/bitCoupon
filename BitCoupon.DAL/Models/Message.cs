using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    public class Message
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Sender { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(500)]
        public string Content { get; set; }

        public DateTime Time { get; set; }

        public string Title { get; set; }

        public bool IsRead { get; set; }

        public int CommentId { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
