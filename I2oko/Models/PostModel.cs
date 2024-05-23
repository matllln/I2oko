using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I2oko.Models
{
    public class PostModel
    {
        [Key]
        public int PostID { get; set; }
        public string UserName { get; set; }
        public string Media { get; set; }
        public string MediaURL { get; set; }
        public string Text { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }
        public int PostLikeNumber { get; set; }
        public string Place { get; set; }
        public int Pin { get; set; }
        public int Save { get; set; }
        public string Subject { get; set; }
        public string FittedImage { get; set; }
        public string PicturePath { get; set; }
        public bool PostIsLikeModel { get; set; }
        public bool PostIsSaveModel { get; set; }
        public int ViewPostNumber { get; set; }
        public int CommentNumber { get; set; }

    }
}