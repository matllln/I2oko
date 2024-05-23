using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace I2oko.Models
{
    public class LikeModel
    {
        [Key]
        public int LikeID { get; set; }
        public string UserNameOwner { get; set; }
        public int PostID { get; set; }
        public int FoodID { get; set; }
        public string UserNameViewer { get; set; }
        public bool PostIsLikeModel { get; set; }
        public bool FoodIsLikeModel { get; set; }
        public string ProfilePicturePath { get; internal set; }
        public string LikePostPicturePath { get; internal set; }
        public string LikeFoodPicturePath { get; internal set; }
        [Column(TypeName = "datetime2")]
        public DateTime LikeDateTime { get; set; }
    }
}