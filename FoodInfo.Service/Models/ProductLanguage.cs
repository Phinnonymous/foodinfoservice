using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace FoodInfo.Service.Models
{
    public class ProductLanguage : Base
    {
        public int ID { get; set; }
        

        //
        public virtual Language Language { get; set; }
        //
        public virtual Product Product { get; set; }


    }

}
