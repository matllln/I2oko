using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{
    public enum IngredientUnit
    {
        [Display(Name = "")] ___,
        [Display(Name = "عدد")] عدد,
        [Display(Name = "حبه")] حبه,
        [Display(Name = "بسته")] بسته,
        [Display(Name = "گرم")] گرم,
        [Display(Name = "کیلو")] کیلو,
        [Display(Name = "لیوان")] لیوان,
        [Display(Name = "پیمانه")] پیمانه,
        [Display(Name = "قاشق چای خوری")] قاشق‌چای‌خوری,
        [Display(Name = "قاشق مربا خوری")] قاشق‌مرباخوری,
        [Display(Name = "قاشق غذا خوری")] قاشق‌غذاخوری,
        [Display(Name = "به مقدار لازم")] به‌مقدارلازم
    }
    public class FoodSpecifiacationModel
    {
        public int FoodSpecID { get; set; }
        [Key]
        public string IngredientName { get; set; }
        public IngredientUnit IngredientUnit { get; set; }
        public byte Picture { get; set; }
        public int Base { get; set; }
        public int Price { get; set; }
        public int Caloris { get; set; }
        public int Carbohydrate { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Water { get; set; }
        public int Fiber { get; set; }
        public int SugarLoaf { get; set; }
        public int Cholesterol { get; set; }
        public int VitaminA { get; set; }
        public int VitaminC { get; set; }
        public int VitaminD { get; set; }
        public int VitaminE { get; set; }
        public int VitaminK { get; set; }
        public int ThiamineB1 { get; set; }
        public int RiboflavinB2 { get; set; }
        public int NiacinB3 { get; set; }
        public int ColleenB4 { get; set; }
        public int PantothenicAcidB5 { get; set; }
        public int VitaminB6 { get; set; }
        public int FolateB9 { get; set; }
        public int VitaminB12 { get; set; }
        public int Calcium { get; set; }
        public int Iron { get; set; }
        public int Magnesium { get; set; }
        public int Phosphorus { get; set; }
        public int Potassium { get; set; }
        public int Sodium { get; set; }
        public int Zinc { get; set; }
        public int Copper { get; set; }
        public int Manganese { get; set; }
        public int Selenium { get; set; }
        public int BoiledWithSalt_Caloris { get; set; }
        public int BoiledWithSalt_Carbohydrate { get; set; }
        public int BoiledWithSalt_Protein { get; set; }
        public int BoiledWithSalt_Fat { get; set; }
        public int BoiledWithSalt_Water { get; set; }
        public int BoiledWithSalt_Fiber { get; set; }
        public int BoiledWithSalt_SugarLoaf { get; set; }
        public int BoiledWithSalt_Cholesterol { get; set; }
        public int BoiledWithSalt_VitaminA { get; set; }
        public int BoiledWithSalt_VitaminC { get; set; }
        public int BoiledWithSalt_VitaminD { get; set; }
        public int BoiledWithSalt_VitaminE { get; set; }
        public int BoiledWithSalt_VitaminK { get; set; }
        public int BoiledWithSalt_ThiamineB1 { get; set; }
        public int BoiledWithSalt_RiboflavinB2 { get; set; }
        public int BoiledWithSalt_NiacinB3 { get; set; }
        public int BoiledWithSalt_ColleenB4 { get; set; }
        public int BoiledWithSalt_PantothenicAcidB5 { get; set; }
        public int BoiledWithSalt_VitaminB6 { get; set; }
        public int BoiledWithSalt_FolateB9 { get; set; }
        public int BoiledWithSalt_VitaminB12 { get; set; }
        public int BoiledWithSalt_Calcium { get; set; }
        public int BoiledWithSalt_Iron { get; set; }
        public int BoiledWithSalt_Magnesium { get; set; }
        public int BoiledWithSalt_Phosphorus { get; set; }
        public int BoiledWithSalt_Potassium { get; set; }
        public int BoiledWithSalt_Sodium { get; set; }
        public int BoiledWithSalt_Zinc { get; set; }
        public int BoiledWithSalt_Copper { get; set; }
        public int BoiledWithSalt_Manganese { get; set; }
        public int BoiledWithSalt_Selenium { get; set; }
        public int BoiledWithoutSalt_Caloris { get; set; }
        public int BoiledWithoutSalt_Carbohydrate { get; set; }
        public int BoiledWithoutSalt_Protein { get; set; }
        public int BoiledWithoutSalt_Fat { get; set; }
        public int BoiledWithoutSalt_Water { get; set; }
        public int BoiledWithoutSalt_Fiber { get; set; }
        public int BoiledWithoutSalt_SugarLoaf { get; set; }
        public int BoiledWithoutSalt_Cholesterol { get; set; }
        public int BoiledWithoutSalt_VitaminA { get; set; }
        public int BoiledWithoutSalt_VitaminC { get; set; }
        public int BoiledWithoutSalt_VitaminD { get; set; }
        public int BoiledWithoutSalt_VitaminE { get; set; }
        public int BoiledWithoutSalt_VitaminK { get; set; }
        public int BoiledWithoutSalt_ { get; set; }
        public int BoiledWithoutSalt_RiboflavinB2 { get; set; }
        public int BoiledWithoutSalt_NiacinB3 { get; set; }
        public int BoiledWithoutSalt_ColleenB4 { get; set; }
        public int BoiledWithoutSalt_PantothenicAcidB5 { get; set; }
        public int BoiledWithoutSalt_VitaminB6 { get; set; }
        public int BoiledWithoutSalt_FolateB9 { get; set; }
        public int BoiledWithoutSalt_VitaminB12 { get; set; }
        public int BoiledWithoutSalt_Calcium { get; set; }
        public int BoiledWithoutSalt_Iron { get; set; }
        public int BoiledWithoutSalt_Magnesium { get; set; }
        public int BoiledWithoutSalt_Phosphorus { get; set; }
        public int BoiledWithoutSalt_Potassium { get; set; }
        public int BoiledWithoutSalt_Sodium { get; set; }
        public int BoiledWithoutSalt_Zinc { get; set; }
        public int BoiledWithoutSalt_Copper { get; set; }
        public int BoiledWithoutSalt_Manganese { get; set; }
        public int BoiledWithoutSalt_Selenium { get; set; }
    }
}