using System.ComponentModel.DataAnnotations;


namespace I2oko.Models
{
    public class FavoriteModel
    {
        [Key]
        public int FavoriteID { get; set; }
        public string UserNameOwner { get; set; }
        public int PostID { get; set; }
        public int FoodID { get; set; }
        public string UserNameViewer { get; set; }
        public bool PostIsSaveModel { get; set; }
        public bool FoodIsSaveModel { get; set; }
    }
}