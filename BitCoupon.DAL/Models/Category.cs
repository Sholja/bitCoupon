using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    /// <summary>
    /// Represents category of coupon
    /// </summary>
	public class Category
	{
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Required]
        public bool Approved { get; set; }
    }
}
