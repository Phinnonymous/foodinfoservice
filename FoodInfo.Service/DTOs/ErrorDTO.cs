using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.Models
{
    public class ErrorDTO
    {
        string code { get; set; }
        string source { get; set; }
        string message { get; set; }
        string detail { get; set; }
        string parameters { get; set; }

    }
}
