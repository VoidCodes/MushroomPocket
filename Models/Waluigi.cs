using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    internal class Waluigi : Character
    {
        public Waluigi(int hp, int exp): base(hp, exp)
        {
            CharacterName = "Waluigi";
            Hp = hp;
            Exp = exp;
            Skill = "Agility";
            TransformTo = "Luigi";
        }
    }
}
