using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class ProductDTO : BaseDTO
    {
        public int ID { get; set; }
        
        public int BarcodeId { get; set; }
        public string ProductName { get; set; }
        public int? ProductGroupId { get; set; }
        //
        public byte[] FirstImage { get; set; }
        public byte[] SecondImage { get; set; }
        public byte[] ThirdImage { get; set; }

       // public ICollection<ProductContent> ProductContents { get; set; }
        //
        public virtual ProductCategoryDTO ProductCategoryDTO { get; set; }

        //public ICollection<Comment> Comments { get; set; }
        //public ICollection<Vote> Votes { get; set; }
    }
}
