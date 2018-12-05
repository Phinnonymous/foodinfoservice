using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.Models
{
    public class ProductContent :Base
    {

        public int ID { get; set; }
        public string Ingredients { get;  set; }

        public virtual NutritionFacts NutritionFact { get; set; }
        public virtual Product Product { get; set; }
        public string Warnings { get; set; }
        public string CookingTips { get; set; }
        public string Recommendations { get; set; }
        public string VideoURL { get; set; }
        public virtual Language Language { get; set; }
        public string Details { get; set; }

    }


}
