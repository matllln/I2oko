using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public enum provinces
    {
        [Display(Name = "لیست استان‌ها")] لیست‌استان‌ها,
        [Display(Name = " آذربایجان شرقی ")] آذربایجان‌شرقی,
        [Display(Name = " آذربایجان غربی ")] آذربایجان‌غربی,
        [Display(Name = " اردبیل ")] اردبیل,
        [Display(Name = " اصفهان ")] اصفهان,
        [Display(Name = " البرز ")] البرز,
        [Display(Name = " ایلام ")] ایلام,
        [Display(Name = " بوشهر ")] بوشهر,
        [Display(Name = " تهران ")] تهران,
        [Display(Name = " چهارمحال و بختیاری ")] چهارمحال‌و‌بختیاری,
        [Display(Name = " خراسان جنوبی ")] خراسان‌جنوبی,
        [Display(Name = " خراسان رضوی ")] خراسان‌رضوی,
        [Display(Name = " خراسان شمالی ")] خراسان‌شمالی,
        [Display(Name = " خوزستان ")] خوزستان,
        [Display(Name = " زنجان ")] زنجان,
        [Display(Name = " سمنان ")] سمنان,
        [Display(Name = " سیستان و بلوچستان ")] سیستان‌و‌بلوچستان,
        [Display(Name = " فارس ")] فارس,
        [Display(Name = " قزوین ")] قزوین,
        [Display(Name = " قم ")] قم,
        [Display(Name = " کردستان ")] کردستان,
        [Display(Name = " کرمان ")] کرمان,
        [Display(Name = " کرمانشاه ")] کرمانشاه,
        [Display(Name = " کهگیلویه و بویراحمد ")] کهگیلویه‌و‌بویراحمد,
        [Display(Name = " گلستان ")] گلستان,
        [Display(Name = " گیلان ")] گیلان,
        [Display(Name = " لرستان ")] لرستان,
        [Display(Name = " مازندران ")] بیست‌ومازندرانهفتم,
        [Display(Name = " مرکزی ")] مرکزی,
        [Display(Name = " هرمزگان ")] هرمزگان,
        [Display(Name = " همدان ")] همدان,
        [Display(Name = " یزد ")] یزد
    }
    public class DeliciousModel
    {
        [Key]
        public provinces provinces { get; set; }
        public string Picture { get; set; }
        public string Biography { get; set; }
        public int Appetizer { get; set; }
        public int Food { get; set; }
        public int sweets { get; set; }
    }
}