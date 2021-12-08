using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Account
    {
        public int UserId { get; set; }  
        public int AccountId { get; set; }
        public List<string> FavoriteGenres { get; set; } = new List<string>();
        public string ProfileName { get; set; }
    }
}
