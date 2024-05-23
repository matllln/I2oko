using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class IntroductionsModel
    {
        public int IntroductionID { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string Fname { get; set; }
        [Key]
        public string BiusinesName { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public byte LicensePic { get; set; }
        public byte MediaURL { get; set; }
        public string Biography { get; set; }
        public string Products { get; set; }
        public int LikeNumber { get; set; }
    }
}