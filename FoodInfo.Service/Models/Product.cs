using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace FoodInfo.Service.Models
{
    public class Product : Base 
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int BarkodId { get; set; }
        public string ProductName { get; set; }
        public string ProductPicturePath { get; set;  }
        public int ProductGroupId { get; set; }
        //
        public ICollection<ProductLanguage> ProductLanguages { get; set; }

        //
        public virtual ProductCategory ProductCategory { get; set; }




    }
}
