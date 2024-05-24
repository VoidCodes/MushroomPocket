using System;
using System.Collections.Generic;

namespace MushroomPocket.Models
{
    public class MushroomMaster
    {
        public string Name { get; set; }
        public int NoToTransform { get; set; }
        public string TransformTo { get; set; }

        public MushroomMaster(string name, int noToTransform, string transformTo)
        {
            Name = name;
            NoToTransform = noToTransform;
            TransformTo = transformTo;
        }
    }

    public class Character
    {
        public int Id { get; set; }
        public string CharacterName { get; set; }
        public int Hp { get; set; }
        public int Exp { get; set; }
        public int ExpToTransform { get; set; }
        public string Skill { get; set; }
        public string TransformTo { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }

        public Character(int hp, int exp)
        {
            Hp = hp;
            Exp = exp;
        }
    }
}