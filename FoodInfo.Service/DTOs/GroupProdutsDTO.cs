using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class GroupProdutsDTO : BaseDTO
    {

        public List<string> BarcodeId { get; set; }
        public int ? ProductGroupId { get; set; }


    }
}
