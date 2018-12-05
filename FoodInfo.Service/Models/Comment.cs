using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace FoodInfo.Service.Models
{
    public class Comment : Base
    {
        public int ID { get; set; }
        public string UserComment { get; set; }
        public virtual Product Product { get; set; }

    }
}
