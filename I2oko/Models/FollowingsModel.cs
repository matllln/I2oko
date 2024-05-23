using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace I2oko.Models
{
    public class FollowingsModel
    {
        [Key]
        public int FollowingID { get; set; }
        public string UserName { get; set; }
        public string FollowingsUserName { get; set; }
        public string ProfilePicturePath { get; internal set; }
        public bool IsBlocked { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime FollowingsDateTime { get; set; }
        public bool ContactToAdd { get; set; }

    }
}