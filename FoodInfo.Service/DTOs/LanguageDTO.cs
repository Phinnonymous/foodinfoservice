using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class LanguageDTO
    {
        public int ID { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public string NativeLanguageName { get; set; }
            
    }
}
