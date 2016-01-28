using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitCoupon.DAL.Models
{
    public class Avatar
    {
        public int Id { get; set; }

        [Required]
        public string PictureUrl { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
