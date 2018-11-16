using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace FoodInfo.Service.Models
{
    public class Base
    {
       
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } 
        public int? CreatedUserId { get; set; }
        public int? ModifiedUserId { get; set; }
    }
}
