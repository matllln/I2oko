using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace I2oko.Models
{
    public class FollowersModel
    {
        [Key]
        public int FollowerID { get; set; }
        public string UserName { get; set; }
        public string FollowersUserName { get; set; }
        public string ProfilePicturePath { get; internal set; }
        public bool IsBlocked { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime FollowersDateTime { get; set; }
        public bool ContactToAdd { get; set; }
        public string UserNameViewer { get; set; }

    }
}