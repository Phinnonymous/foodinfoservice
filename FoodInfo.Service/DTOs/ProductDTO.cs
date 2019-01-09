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

        public int? ProductGroupId { get; set; } = 1;
        //
        public byte[] FirstImage { get; set; } = null;
        public byte[] SecondImage { get; set; } = null;
        public byte[] ThirdImage { get; set; } = null;

        
       // public ICollection<ProductContent> ProductContents { get; set; }
        //
        public virtual ProductCategory ProductCategory { get; set; }

        //public ICollection<Comment> Comments { get; set; }
        //public ICollection<Vote> Votes { get; set; }
    }
}
