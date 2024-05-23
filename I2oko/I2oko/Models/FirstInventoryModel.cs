using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class FirstInventoryModel
    {
        public int FirstInventoryID { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public byte Picture { get; set; }
    }
}