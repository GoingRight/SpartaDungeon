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
        public bool isSold { get; set; }

        public abstract void Equip(Player player);

        public abstract void Unequip(Player player);
    }

    public class Weapon : Item   //무기
    {
        public Weapon(string name, int price, int attackDamage, string inform)
        {
            Name = name;
            Price = price;
            AttackDamage = attackDamage;
            Inform = inform;
        }
        public int AttackDamage {  get; set; }

        public override void Equip(Player player)
        {
            isEquiped = true;
            player.AttackDamage += AttackDamage;
            player.ExtraAttackDamage += AttackDamage;
        }
        public override void Unequip(Player player)
        {
            isEquiped = false;
            player.AttackDamage -= AttackDamage;
            player.ExtraAttackDamage -= AttackDamage;
        }
    }

    public class Armor : Item    //방어구
    {
        public Armor(string name, int price, int deffense, string inform)
        {
            Name = name;
            Price = price;
            Deffense = deffense;
            Inform = inform;
        }
        public int Deffense { get; set; }
        public override void Equip(Player player)
        {
            isEquiped = true;
            player.Deffense += Deffense;
            player.ExtraDeffense += Deffense;
        }
        public override void Unequip(Player player)
        {
            isEquiped = false;
            player.Deffense -= Deffense;
            player.ExtraDeffense -= Deffense;
        }
    }
}
