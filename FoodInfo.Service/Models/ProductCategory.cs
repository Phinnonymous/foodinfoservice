using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.Models
{
    public class ProductCategory :Base
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public string LanguageCode { get; set; }
        
        //
        //
      //  public ICollection<Product> Products { get; set; }

    }
}
