using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I2oko.Models
{
    public class RelationNotifModel
    {
        [Key]
        public int RelationNotifID { get; set; }
        public string UserName { get; set; }
        public string FollowersUserName { get; set; }
        public string ProfilePicturePath { get; internal set; }
        public string UserNameViewer { get; set; }
        public int PostID { get; set; }
        public int FoodID { get; set; }
        public bool PostIsLikeModel { get; set; }
        public bool FoodIsLikeModel { get; set; }
        public string LikePostPicturePath { get; internal set; }
        public string LikeFoodPicturePath { get; internal set; }
    }
}