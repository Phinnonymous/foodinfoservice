using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodInfo.Service.Models;

namespace FoodInfo.Service.DTOs
{
    public class ProductDTO : BaseDTO
    {

        
        public string BarcodeId { get; set; }
        public string ProductName { get; set; }

        //Proje açılışında diger isminde category olustur

        public int?  ProductGroupId { get; set; } 
        //
        public byte[] FirstImage { get; set; }
        public byte[] SecondImage { get; set; }
        public byte[] ThirdImage { get; set; }

        
       // public ICollection<ProductContent> ProductContents { get; set; }
        //
        public virtual ProductCategory ProductCategory { get; set; }

        //public ICollection<Comment> Comments { get; set; }
        //public ICollection<Vote> Votes { get; set; }
    }
}
