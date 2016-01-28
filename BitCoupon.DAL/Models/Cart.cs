using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public List<Item> Items { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string Purchased { get; set; }

        public string PaymentId { get; set; }

        public DateTime TimeOfPurchase { get; set; }

        public double TotalPrice { get; set; }

    }
}
