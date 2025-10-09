using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Harvestly.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        public string Quantity { get; set; }
        public Boolean Active { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Of Launch")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
       public DateTime dateOfLaunch { get; set; }
        [Display(Name = "Category")]
        public int categoryId { get; set; }

       [Display(Name = "Free Delivery")]
        public Boolean freeDelivery { get; set; }

        public virtual Category Category { get; set; }
    }
}