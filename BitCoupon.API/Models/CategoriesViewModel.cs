using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitCoupon.DAL.Models;

namespace BitCoupon.API.Models
{
    public class CategoriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Approved { get; set; }

        public int CouponsNumber { get; set; }

        private ApplicationDbContext db = new ApplicationDbContext();

        public CategoriesViewModel(Category category)
        {
            this.Id = category.Id;
            this.Name = category.Name;
            this.Approved = category.Approved;
            if (category.Name == "All Categories")
            {
                this.CouponsNumber = db.Coupons.Where(x => x.IsEdited == false && x.IsDeleted == false && x.Acitve == true && x.Approved == true).ToList().Count;
            }
            else
            {
                this.CouponsNumber = db.Coupons.Where(x => x.CategoryId == category.Id && x.IsEdited == false && x.IsDeleted == false && x.Acitve == true && x.Approved == true).ToList().Count;
            }
        }
    }
}
