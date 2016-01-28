using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BitCoupon.DAL.Models
{

    
   public class Mail
    {
        [Key]
        public int Id { get; set; }
        [AllowHtml]
        [MinLength(5)]
       [Required]
        public string Body { get; set; }
        [Required]
        public string Subject { get; set; }

    }
}
