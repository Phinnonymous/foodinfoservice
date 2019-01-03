using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodInfo.Service.Models;

namespace FoodInfo.Service.DTOs
{
    public class CommentDTO : BaseDTO
    {
        public string UserComment { get; set; }
        public int ProductContentId { get; set; }
    }
}
