using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.DTOs
{
    public class VoteDTO : BaseDTO
    {
        public int ?  UserVote { get; set; }
        public string  BarcodeID { get; set; }
    }
}
