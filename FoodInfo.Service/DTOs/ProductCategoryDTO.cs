using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class ProductCategoryDTO:BaseDTO
    {
        public int ID { get; set; }

        public string CategoryName { get; set; }
        public string LanguageCode { get; set; }
  
        public ICollection<ProductDTO> Products { get; set; }
    }
}
