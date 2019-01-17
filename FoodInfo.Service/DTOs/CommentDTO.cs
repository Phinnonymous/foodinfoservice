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

        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
