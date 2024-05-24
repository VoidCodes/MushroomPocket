using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    internal class Primagen : Character
    {
        public Primagen(int hp, int exp): base(hp, exp)
        {
            CharacterName = "Primagen";
            Hp = hp;
            Exp = exp;
            Skill = "Radiant Burst";
            TransformTo = "Protogen";
        }
    }
}