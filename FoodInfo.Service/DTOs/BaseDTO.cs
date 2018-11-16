using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodInfo.Service.DTOs;

namespace FoodInfo.Service.DTOs
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedUserId { get; set; }
        public int? ModifiedUserId { get; set; }
    }

}
