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
        public int CharacterId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        /*public Inventory(int _characterId, int _itemId, int _quantity)
        {
            CharacterId = _characterId;
            ItemId = _itemId;
            Quantity = _quantity;
        }*/
    }
}
