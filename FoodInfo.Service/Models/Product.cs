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
        public string BarcodeId { get; set; }
        public string ProductName { get; set; }
        public int ? ProductGroupId { get; set; }
        //
        public byte  [] FirstImage { get; set; }
        public byte [] SecondImage { get; set; }
        public byte [] ThirdImage { get; set; }

       // public ICollection<ProductContent> ProductContents { get; set; }
        //
        public virtual ProductCategory ProductCategory { get; set; }

        //public ICollection<Comment> Comments{ get; set; }
       // public ICollection<Vote> Votes { get; set; }



    }
}
