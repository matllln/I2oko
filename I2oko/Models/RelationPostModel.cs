using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I2oko.Models
{
    public class RelationPostModel
    {
        [Key]
        public string UserName { get; set; }
        public int PostID { get; set; }
        public int FoodID { get; set; }
        public string MediaURL { get; set; }
        public string PicturePath { get; set; }
        public string Text { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string OriginalityPlace { get; set; }
        public string PictureURL { get; set; }
        public string VideoURL { get; set; }
        public string Biography { get; set; }
        public string Points { get; set; }
        public string HardshipLevel { get; set; }
        public string CookingTimeHour { get; set; }
        public string CookingTimeMinute { get; set; }
        public string people { get; set; }
        public string Recipe { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }
        public bool PostIsLikeModel { get; set; }
        public bool FoodIsLikeModel { get; set; }
        public bool PostIsSaveModel { get; set; }
        public bool FoodIsSaveModel { get; set; }
        public int FoodLikeNumber { get; set; }
        public int PostLikeNumber { get; set; }
        public int ViewFoodNumber { get; set; }
        public int ViewPostNumber { get; set; }
    }
}