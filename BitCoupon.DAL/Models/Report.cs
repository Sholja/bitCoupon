using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    /// <summary>
    /// Represesnts report of coupon
    /// </summary>
    public class Report
    {
        public int Id { get; set; }

        public int CommentId { get; set; }

        public virtual Comment Comment { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
