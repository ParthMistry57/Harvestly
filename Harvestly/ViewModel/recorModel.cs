using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Harvestly.Models;
namespace Harvestly.ViewModel
{
    public class recorModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Name!")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "Name must be at most 50 characters !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Price!")]
        [Display(Name = "Price")]
        public double Price { get; set; }

        public Boolean Active { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateOfLaunch { get; set; }

        [Required(ErrorMessage = "Enter Category!")]
        [Display(Name = "Category")]
        public int categoryId { get; set; }

        [Display(Name = "Free Delivery")]
        public Boolean freeDelivery { get; set; }

        public virtual Category Category { get; set; }
        public double ComputeTotalValue()
        {
            HarvestlyContext context = new HarvestlyContext();
            return context.Carts.Sum(e => e.MenuItem.Price);
        }
    }
}