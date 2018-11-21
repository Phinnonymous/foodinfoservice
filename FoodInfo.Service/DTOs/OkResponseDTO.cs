using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class OkResponseDTO
    {
        public object Result { get; set; }
        public int statusCode { get; set; }
        
    }
}
