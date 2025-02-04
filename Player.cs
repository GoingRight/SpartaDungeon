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
        public int ExtraAttackDamage { get; set; }
        public int Deffense { get; set; }
        public int ExtraDeffense { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }

        public List<Item> items = new List<Item>(); // 보유 아이템 리스트

        

        public void InitPlayer(string name)
        {
            Lv = 1;
            Name = name;
            Class = "전사";
            AttackDamage = 10;
            ExtraAttackDamage = 0;
            Deffense = 5;
            ExtraDeffense = 0;
            MaxHP = 100;
            HP = 1;
            Gold = 1500;
        }
    }

}
