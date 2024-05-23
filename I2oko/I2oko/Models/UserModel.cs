using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public enum Sexuality
    {
        [Display(Name = "")] انتخاب‌‌‌نشده,
        [Display(Name = "مرد")] آقا,
        [Display(Name = "زن")] خانم,
        [Display(Name = "نامشخص")] نامشخص
    }
    public enum Categuri
    {
        [Display(Name = "")] انتخاب‌نشده,
        [Display(Name = "کارآفرین")] کارآفرین,
        [Display(Name = "پزشک")] پزشک,
        [Display(Name = "مهندس")] مهندس,
        [Display(Name = "سر آشپز")] سرآشپز,
        [Display(Name = "مغازه دار")] مغازه‌دار,
        [Display(Name = "رستوران")] رستوران,
        [Display(Name = "کارمند")] کارمند,
        [Display(Name = "خانه دار")] خانه‌دار,
        [Display(Name = "هنرمند")] هنرمند,
        [Display(Name = "ورزشکار")] ورزشکار,
        [Display(Name = "بلاگر")] بلاگر,
        [Display(Name = "دانش آموز")] دانش‌اموز,
        [Display(Name = "معلم")] معلم,
        [Display(Name = "نویسنده")] نویسنده,
        [Display(Name = "فعال سیاسی")] فعال‌سیاسی,
        [Display(Name = "فعال اقتصادی")] فعال‌اقتصادی,
        [Display(Name = "فعال فرهنگی")] فعال‌فرهنگی,
        [Display(Name = "فعال زیبایی")]فعال‌زیبایی
    }
    public class UserModel
    {
        public int UserID { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Fname { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public Sexuality Sexuality { get; set; }
        public int BirthDate { get; set; }
        public Categuri Categuri { get; set; }
        public int SkilLevel { get; set; }
        public string Biography { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}