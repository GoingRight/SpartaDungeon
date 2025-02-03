using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public abstract class Item   //아이템 공통
    {
        public string Name {  get; set; }
        public int Price {  get; set; }
        public string Inform {  get; set; }
        public bool isEquiped { get; set; }

        public abstract void Equip(Player player);

        public abstract void Unequip(Player player);
    }

    public class Weapon : Item   //무기
    {
        public Weapon(string name, int price, int attackDamage)
        {
            Name = name;
            Price = price;
            AttackDamage = attackDamage;
        }
        public int AttackDamage {  get; set; }

        public override void Equip(Player player)
        {
            isEquiped = true;
            player.AttackDamage += AttackDamage;
        }
        public override void Unequip(Player player)
        {
            isEquiped = false;
            player.AttackDamage -= AttackDamage;
        }
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
        public override void Equip(Player player)
        {
            isEquiped = true;
            player.Deffense += Deffense;
        }
        public override void Unequip(Player player)
        {
            isEquiped = false;
            player.Deffense -= Deffense;
        }
    }
}
