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
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public string NativeLanguageName { get; set; }
        //
        public ICollection<ProductContent> ProductContents { get; set; }

        
    }
}
