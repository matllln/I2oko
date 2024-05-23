using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class FollowersModel
    {
        [Key]
        public int FollowerID { get; set; }
        public string UserName { get; set; }
        public string FollowersUserName { get; set; }
    }
}