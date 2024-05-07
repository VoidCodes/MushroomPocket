using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    internal class Wario : Character
    {
        public Wario(string characterName, int hp, int exp, string skill) : base(characterName, hp, exp, skill)
        {
            CharacterName = "Wario";
            Hp = 87;
            Exp = 34;
            Skill = "Strength";
        }
    }
}
