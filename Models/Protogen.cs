using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    internal class Protogen : Character
    {
        public Protogen(int hp, int exp): base(hp, exp)
        {
            CharacterName = "Protogen";
            Hp = hp;
            Exp = exp;
            Skill = "Radiant Burst";
            TransformTo = "Primagen";
        }
    }
}