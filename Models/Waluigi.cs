using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    internal class Waluigi : Character
    {
        public Waluigi(string characterName, int hp, int exp, string skill) : base(characterName, hp, exp, skill)
        {
            CharacterName = "Waluigi";
            Hp = 23;
            Exp = 11;
            Skill = "Agility";
        }
    }
}
