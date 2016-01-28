using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    public class PayPalRefund
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Finished { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; }

        [Required]
        public string Option { get; set; }

        [MaxLength(500)]
        public string Content { get; set; }

        public string PaymentId { get; set; }
    }
}
