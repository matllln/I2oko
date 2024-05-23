using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class PostModel
    {
        [Key]
        public int PostID { get; set; }
        public string UserName { get; set; }
        public byte MediaURL { get; set; }
        public string Text { get; set; }
        public int Date { get; set; }
        public int Time { get; set; }
        public int LikeNumber { get; set; }
        public string Place { get; set; }
        public int Pin { get; set; }
        public int Save { get; set; }
    }
}