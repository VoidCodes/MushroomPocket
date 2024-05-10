using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    internal class Wario : Character
    {
        public Wario(int hp, int exp): base(hp, exp)
        {
            CharacterName = "Wario";
            Hp = hp;
            Exp = exp;
            Skill = "Strength";
            TransformTo = "Luigi";
        }
    }
}
