using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        // Foreign key
        public int CharacterId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public Character Character { get; set; }
        public Items Items { get; set; }
        public Inventory(int characterId, int itemId, int quantity)
        {
            CharacterId = characterId;
            ItemId = itemId;
            Quantity = quantity;
        }
    }
}
