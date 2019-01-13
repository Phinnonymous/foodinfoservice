using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class SearchByNameDTO
    {
        public int ID { get; set; }
        
        public string BarcodeId { get; set; }
        public string ProductName { get; set; }
        public int? ProductGroupId { get; set; } 
        //
        public byte[] FirstImage { get; set; }
  

        // public ICollection<ProductContent> ProductContents { get; set; }
        //
       // public virtual ProductCategory ProductCategory { get; set; }
    }
}
