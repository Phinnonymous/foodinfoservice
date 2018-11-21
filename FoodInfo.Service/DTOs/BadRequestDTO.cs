using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class BadRequestDTO
    {
        public string Message { get; set; } = string.Empty;
        public  object Result { get; set; } = null; 
        public int statusCode { get; set; }
    }
}
