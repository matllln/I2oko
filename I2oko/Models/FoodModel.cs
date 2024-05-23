
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace I2oko.Models
{
    public enum HardshipLevel
    {
        [Display(Name = "")] ___,
        [Display(Name = "مثل آب خوردنه")] مثل‌آب‌خوردنه,
        [Display(Name = "کاری نداره")] کاری‌نداره,
        [Display(Name = "از پسش برمیای")] از‌پسش‌برمیای,
        [Display(Name = "چالش برانگیزه")] چالش‌برانگیزه,
        [Display(Name = "بپزش و به خودت افتخار کن")] بپزش‌و‌به‌خودت‌افتخارکن
    }

    public enum CookingTimeHour
    {
        [Display(Name = "")] ___,
        [Display(Name = " 1' ")] یک,
        [Display(Name = " 2' ")] دو,
        [Display(Name = " 3' ")] سه,
        [Display(Name = " 4' ")] چهارم,
        [Display(Name = " 5' ")] پنج,
        [Display(Name = " 6' ")] شش,
        [Display(Name = " 7' ")] هفتم,
        [Display(Name = " 8' ")] هشت,
        [Display(Name = " 9' ")] نهم,
        [Display(Name = " 10' ")] دهم,
        [Display(Name = " 11' ")] یازده,
        [Display(Name = " 12' ")] دوازده,
        [Display(Name = " 13' ")] سیزده,
        [Display(Name = " 14' ")] چهارده,
        [Display(Name = " 15' ")] پانزده,
        [Display(Name = " 16' ")] شانزده,
        [Display(Name = " 17' ")] هفده,
        [Display(Name = " 18' ")] هجده,
        [Display(Name = " 19' ")] نونزده,
        [Display(Name = " 20' ")] بیست
    }

    public enum CookingTimeMinute
    {
        [Display(Name = "")] ___,
        [Display(Name = " 1'' ")] یک,
        [Display(Name = " 2'' ")] دو,
        [Display(Name = " 3'' ")] سه,
        [Display(Name = " 4'' ")] چهار,
        [Display(Name = " 5'' ")] پنج,
        [Display(Name = " 6'' ")] شش,
        [Display(Name = " 7'' ")] هفت,
        [Display(Name = " 8'' ")] هشت,
        [Display(Name = " 9'' ")] نه,
        [Display(Name = " 10'' ")] ده,
        [Display(Name = " 11'' ")] یازده,
        [Display(Name = " 12'' ")] دوازده,
        [Display(Name = " 13'' ")] سیزده,
        [Display(Name = " 14'' ")] چهارده,
        [Display(Name = " 15'' ")] پانزده,
        [Display(Name = " 16'' ")] شانزده,
        [Display(Name = " 17'' ")] هفده,
        [Display(Name = " 18'' ")] هجده,
        [Display(Name = " 19'' ")] نونزده,
        [Display(Name = " 20'' ")] بیست,
        [Display(Name = " 21'' ")] بیست‌ویک,
        [Display(Name = " 22'' ")] بیست‌ودو,
        [Display(Name = " 23'' ")] بیست‌وسه,
        [Display(Name = " 24'' ")] بیست‌وچهار,
        [Display(Name = " 25'' ")] بیست‌وپنج,
        [Display(Name = " 26'' ")] بیست‌وشش,
        [Display(Name = " 27'' ")] بیست‌وهفت,
        [Display(Name = " 28'' ")] بیست‌وهشت,
        [Display(Name = " 29'' ")] بیست‌ونه,
        [Display(Name = " 30'' ")] سی,
        [Display(Name = " 31'' ")] سی‌ویک,
        [Display(Name = " 32'' ")] سی‌ودو,
        [Display(Name = " 33'' ")] سی‌وسه,
        [Display(Name = " 34'' ")] سی‌وچهار,
        [Display(Name = " 35'' ")] سی‌وپنج,
        [Display(Name = " 36'' ")] سی‌وشش,
        [Display(Name = " 37'' ")] سی‌وهفت,
        [Display(Name = " 38'' ")] سی‌وهشت,
        [Display(Name = " 39'' ")] سی‌ونه,
        [Display(Name = " 40'' ")] چهل,
        [Display(Name = " 41'' ")] چهل‌ویک,
        [Display(Name = " 42'' ")] چهل‌ودو,
        [Display(Name = " 43'' ")] چهل‌وسه,
        [Display(Name = " 44'' ")] چهل‌وچهار,
        [Display(Name = " 45'' ")] چهل‌وپنج,
        [Display(Name = " 46'' ")] چهل‌وشش,
        [Display(Name = " 47'' ")] چهل‌وهفت,
        [Display(Name = " 48'' ")] چهل‌وهشت,
        [Display(Name = " 49'' ")] چهل‌ونه,
        [Display(Name = " 50'' ")] پنجاه,
        [Display(Name = " 51'' ")] پنجاه‌و‌یک,
        [Display(Name = " 52'' ")] پنجاه‌و‌دو,
        [Display(Name = " 53'' ")] پنجاه‌و‌سه,
        [Display(Name = " 54'' ")] پنجاه‌و‌چهار,
        [Display(Name = " 55'' ")] پنجاه‌و‌پنج,
        [Display(Name = " 56'' ")] پنجاه‌و‌شش,
        [Display(Name = " 57'' ")] پنجاه‌و‌هفت,
        [Display(Name = " 58'' ")] پنجاه‌و‌هشت,
        [Display(Name = " 59'' ")] پنجاه‌و‌نه
    }

    public enum people
    {
        [Display(Name = "")] ___,
        [Display(Name = " 1 ")] یک,
        [Display(Name = " 2 ")] دو,
        [Display(Name = " 3 ")] سه,
        [Display(Name = " 4 ")] چهار,
        [Display(Name = " 5 ")] پنج,
        [Display(Name = " 6 ")] شش,
        [Display(Name = " 7 ")] هفت,
        [Display(Name = " 8 ")] هشت,
        [Display(Name = " 9 ")] نه,
        [Display(Name = " 10 ")] ده,
        [Display(Name = " 11 ")] یازده,
        [Display(Name = " 12 ")] دوازده
    }

    //public enum IngredientUnit
    //{
    //    [Display(Name = "")] ___,
    //    [Display(Name = "عدد")] عدد,
    //    [Display(Name = "حبه")] حبه,
    //    [Display(Name = "بسته")] بسته,
    //    [Display(Name = "گرم")] گرم,
    //    [Display(Name = "کیلو")] کیلو,
    //    [Display(Name = "لیوان")] لیوان,
    //    [Display(Name = "پیمانه")] پیمانه,
    //    [Display(Name = "قاشق چای خوری")] قاشق‌چای‌خوری,
    //    [Display(Name = "قاشق مربا خوری")] قاشق‌مرباخوری,
    //    [Display(Name = "قاشق غذا خوری")] قاشق‌غذاخوری,
    //    [Display(Name = "به مقدار لازم")] به‌مقدارلازم
    //}

    public enum Subject
    {
        [Display(Name = "")] ___,
        [Display(Name = "غذای اصلی")] غذای‌اصلی,
        [Display(Name = "غذای کودک")] غذای‌کودک,
        [Display(Name = "پیش غذا و سالاد")] پیش‌غذاوسالاد,
        [Display(Name = "ترشی و مربا")] ترشی‌ومربا,
        [Display(Name = "دسر و شیرینی")] دسروشیرینی,
        [Display(Name = "نوشیدنی")] نوشیدنی,
        [Display(Name = "طعم و رنگ")] طعم‌ورنگ,
        [Display(Name = "سفره زیبا")] سفره‌زیبا
    }
    public class FoodModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodID { get; set; }
        public string UserName { get; set; }
        public Subject Subject { get; set; }
        public string Name { get; set; }
        public string OriginalityPlace { get; set; }
        public string Picture { get; set; }
        public string PictureURL { get; set; }
        public string Video { get; set; }
        public string VideoURL { get; set; }
        public string Biography { get; set; }
        public string Points { get; set; }
        public HardshipLevel HardshipLevel { get; set; }
        public CookingTimeHour CookingTimeHour { get; set; }
        public CookingTimeMinute CookingTimeMinute { get; set; }
        public people people { get; set; }
        public string Recipe { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }
        public int Pin { get; set; }
        public int Save { get; set; }
        public int FoodLikeNumber { get; set; }
        public string Taste { get; set; }
        public string PicturePath { get; set; }
        public bool FoodIsLikeModel { get; set; }
        public bool FoodIsSaveModel { get; set; }
        public int ViewFoodNumber { get; set; }
        public int CommentNumber { get; set; }

    }
}



