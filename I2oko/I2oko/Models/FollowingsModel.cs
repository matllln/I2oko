using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class FollowingsModel
    {
        [Key]
        public int FollowingID { get; set; }
        public string UserName { get; set; }
        public string FollowingsUserName { get; set; }
    }
}