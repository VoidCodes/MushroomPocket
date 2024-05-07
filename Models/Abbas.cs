using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    public class Abbas : Character
    {
        public Abbas(string characterName, int hp, int exp, string skill) : base(characterName, hp, exp, skill)
        {
            CharacterName = "Abbas";
            Hp = hp;
            Exp = exp;
            Skill = "Brotecting Za Culture";
            TransformTo = "Shawarma Man";
        }
    }
}
