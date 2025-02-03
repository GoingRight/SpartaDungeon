using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Item   //아이템 공통
    {
        public string Name {  get; set; }
        public int Price {  get; set; }
        public string Inform {  get; set; }
        public bool isEquiped { get; set; }
    }

    public class Weapon : Item   //무기
    {
        public Weapon(string name, int price, int atteckDamage)
        {
            Name = name;
            Price = price;
            AttackDamage = atteckDamage;
        }
        public int AttackDamage {  get; set; }
    }

    public class Armor : Item    //방어구
    {
        public Armor(string name, int price, int deffense)
        {
            Name = name;
            Price = price;
            Deffense = deffense;
        }
        public int Deffense { get; set; }
    }
}
