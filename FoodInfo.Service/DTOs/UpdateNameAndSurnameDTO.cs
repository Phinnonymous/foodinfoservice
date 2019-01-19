using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class UpdateNameAndSurnameDTO
    {
        public string NewName { get; set; }
        public string NewSurname { get; set; }
        public string Username { get; set; }
    }
}