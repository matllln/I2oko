using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class PetModel
    {
        public int PetID { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }
        public byte Picture { get; set; }
        public string Food { get; set; }
        public string Keeping { get; set; }
        public int LikeNumber { get; set; }
    }
}