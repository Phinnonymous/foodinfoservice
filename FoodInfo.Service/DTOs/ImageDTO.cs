using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class ImageDTO
    {
        public byte[] FirstImage { get; set; }
        public byte[] SecondImage { get; set; }
        public byte[] ThirdImage { get; set; }

        public string BarcodeId { get; set; }
    }
}
