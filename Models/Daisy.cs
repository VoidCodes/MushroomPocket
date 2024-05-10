using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MushroomPocket.Models
{
    public class Daisy : Character
    {
        public Daisy(int _hp, int _exp): base(_hp, _exp)
        {
            CharacterName = "Daisy";
            Hp = _hp;
            Exp = _exp;
            Skill = "Leadership";
            TransformTo = "Peach";
        }
    }
}
