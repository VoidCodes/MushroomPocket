using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MushroomPocket.Models
{
    public enum ItemEffectType
    {
        HPBoost,
        EXPBoost
    }
    
    public class Items
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public ItemEffectType EffectType { get; set; }
        public int EffectValue { get; set; }

        // Navigation properties
        public List<Inventory> Inventory { get; set; }

        /*public Items(string itemName, ItemEffect effect)
        {
            ItemName = itemName;
            Effect = effect;
        }*/
    }
}
