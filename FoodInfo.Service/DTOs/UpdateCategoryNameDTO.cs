using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class UpdateCategoryNameDTO : BaseDTO
    {
        public string OldCategoryName { get; set; }
        public string NewCategoryName { get; set; }
    }
}
