using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class SecondInventoryModel
    {
        public int SecondInventoryID { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public byte Picture { get; set; }
    }
}