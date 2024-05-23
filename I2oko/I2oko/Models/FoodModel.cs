using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class FoodModel
    {
        public int FoodID { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public string Ingredient { get; set; }
        public string Originality { get; set; }
        public string Biography { get; set; }
        public int HardshipLevel { get; set; }
        public string Recipe { get; set; }
        public int LikeNumber { get; set; }
        public int CookingTime { get; set; }
        public string Taste { get; set; }
        public byte MediaURL { get; set; }
    }
}