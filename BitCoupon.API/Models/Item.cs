using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.API.Models
{
    public class Item
    {

        public string id { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int total { get; set; }

        public string name { get; set; }

        public Data2 data { get; set; }
    }
}
