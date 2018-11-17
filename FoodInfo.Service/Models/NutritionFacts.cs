using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.Models
{
    public class NutritionFacts
    {
        public int ID { get; set; }
        public decimal Energy { get; set; }
        public decimal Fat { get; set; }
        public decimal SaturatedFattyAcids { get; set; }

        public decimal TransFattyAcids { get; set; }
        public decimal Carbohydrate { get; set; }
        public decimal Fiber { get; set; }
        public decimal Protein { get; set; }
        public decimal Salt { get; set; }

        public ICollection<ProductContent> ProductContent { get; set; }
    }
}
