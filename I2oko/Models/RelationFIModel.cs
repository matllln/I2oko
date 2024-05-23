using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace I2oko.Models
{

    public class RelationFIModel
    {
        [Key]
        public string RelationFIModelID { get; set; }
        public string UserName { get; set; }
        public string NumberOfIngredients { get; set; }
        public virtual ICollection<FoodModel> FoodID { get; set; }
        public virtual ICollection<FoodSpecifiacationModel> FoodSpecID { get; set; }
    }
}