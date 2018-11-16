using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FoodInfo.Service.Models
{
    public class Language : Base
    {
        public int ID { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        //
        public ICollection<ProductLanguage> ProductLanguages { get; set; }

    }
}
