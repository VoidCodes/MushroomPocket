using System.ComponentModel.DataAnnotations;

namespace MushroomPocket.Models
{
    public enum ItemEffect
    {
        HPBoost,
        EXPBoost
    }
    
    public class Items
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public ItemEffect Effect { get; set; }

        public Items(string itemName, ItemEffect effect)
        {
            ItemName = itemName;
            Effect = effect;
        }
    }
}
