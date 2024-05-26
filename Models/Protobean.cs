using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    internal class Protobean : Character
    {
        public Protobean(int hp, int exp): base(hp, exp)
        {
            CharacterName = "Protobean";
            Hp = hp;
            Exp = exp;
            Skill = "Neon Beam";
            TransformTo = "Protogen";
        }
    }
} 