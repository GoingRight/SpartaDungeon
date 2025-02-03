using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Player
    {
        public int Lv { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int AttackDamage { get; set; }
        public int Deffense { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }

        public void InitPlayer(string name)
        {
            Lv = 1;
            Name = name;
            Class = "전사";
            AttackDamage = 10;
            Deffense = 5;
            HP = 100;
            Gold = 1500;
        }
    }

}
