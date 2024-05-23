using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class CommentModel
    {
        public int CommentID { get; set; }
        [Key]
        public string From { get; set; }
        public string To { get; set; }
        public int LikeNumber { get; set; }
    }
}