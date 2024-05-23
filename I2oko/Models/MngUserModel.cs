using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I2oko.Models
{
    public class MngUserModel
    {
        public int MngUserID { get; set; }
        public string MngName { get; set; }
        public string MngFname { get; set; }
        [Key]
        public string MngUserName { get; set; }
        public string MngPassword { get; set; }
        public string MngEmail { get; set; }
        public string MngPhone { get; set; }
        public string MngAddress { get; set; }
        public bool MngRememberMe { get; set; }
        [Column(TypeName = "varbinary(max)")]
        public byte[] MngPicture { get; set; }
        public string MngPicturePath { get; set; }
    }
}