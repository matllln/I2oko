using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class NewsModel
    {
        [Key]
        public int NewsID { get; set; }
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Sourse { get; set; }
        public int Date { get; set; }
        public int Time { get; set; }
        public string Text { get; set; }
        public byte MediaURL { get; set; }
        public int WiewNumber { get; set; }
        public int LikeNumber { get; set; }
    }
}