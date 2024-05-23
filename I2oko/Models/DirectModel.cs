using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class DirectModel
    {
        public int DirectID { get; set; }
        [Key]
        public string UserName { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public byte MediaURL { get; set; }
        public int Date { get; set; }
        public int Time { get; set; }
        public int Like { get; set; }
    }
}