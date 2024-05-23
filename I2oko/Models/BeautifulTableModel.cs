using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class BeautifulTableModel
    {
        public int BeautifulTableID { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public byte MediaURL { get; set; }
        public int LikeNumber { get; set; }
    }
}