using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace I2oko.Models
{
    public class CommentModel
    {
        [Key]
        public int ID { get; set; }
        public int CommentLikeID { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string CommentText { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }
        public int LikeNumber { get; set; }
        public int CommentToPostID { get; set; }
        public int CommentToFoodID { get; set; }
        public string UserCommentPicture { get; set; }
        public string CommentToPostPicturePath { get; set; }
        public string CommentToFoodPicturePath { get; set; }
        public int CommentPostLikeNumber { get; set; }
        public int CommentFoodLikeNumber { get; set; }
        public bool CommentPostLike { get; set; }
        public bool CommentFoodLike { get; set; }
    }
}