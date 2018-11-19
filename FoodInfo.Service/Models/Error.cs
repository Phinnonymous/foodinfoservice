using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.Models
{
    public class Error : Base
    {
        public int ID { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        
    }
}
