using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class SocialMediaModel
    {
        public int SocialMediaID { get; set; }
        [Key]
        public string UserName { get; set; }
        public byte MediaURL { get; set; }
        public string Caption { get; set; }
        public int LikeNumber { get; set; }
    }
}