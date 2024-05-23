using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public class RequestModel
    {
        public int RequestID { get; set; }
        [Key]
        public string UserName { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public int Date { get; set; }
        public int Time { get; set; }
    }
}