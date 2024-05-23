using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I2oko.Models
{
    public class CommentLikeModel
    {
        [Key]
        public int CommentLikeID { get; set; }
        public int CommentID { get; set; }
        public string UserNameOwner { get; set; }
        public int PostID { get; set; }
        public int FoodID { get; set; }
        public string UserNameLiker { get; set; }
        public bool CommentPostLike { get; set; }
        public bool CommentFoodLike { get; set; }
        public string CommentProfilePicturePath { get; internal set; }
        public string CommentLikePostPicturePath { get; internal set; }
        public string CommentLikeFoodPicturePath { get; internal set; }
        [Column(TypeName = "datetime2")]
        public DateTime CommentLikeDateTime { get; set; }
        public string CommentText { get; internal set; }

    }
}