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
        private GameManager GM;
        public int Lv { get; set; }
        public int EXP { get; set; }
        public int MaxEXP { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int AttackDamage { get; set; }
        public int ExtraAttackDamage { get; set; }
        public int Deffense { get; set; }
        public int ExtraDeffense { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }
        public Item? EquipedWeapon { get; set; }
        public Item? EquipedArmor { get; set; }

        public List<Item> items = new List<Item>(); // 보유 아이템 리스트

        public Player(GameManager GM)
        {
            this.GM = GM;
        }

        public void InitPlayer(string name)
        {
            Lv = 1;
            EXP = 0;
            MaxEXP = 1;
            Name = name;
            Class = "전사";
            AttackDamage = 10;
            ExtraAttackDamage = 0;
            Deffense = 5;
            ExtraDeffense = 0;
            MaxHP = 100;
            HP = 1;
            Gold = 3000;
        }

        public void GiveEXP(int amount)
        {
            EXP += amount;
            if (EXP >= MaxEXP)
            {
                Lv++;
                EXP = 0;
                MaxEXP++;
                AttackDamage += 1;
                Deffense += 1;
                Console.WriteLine("레벨이 올랐습니다.");
            }
        }

        public bool TakeDamage_IsDeath(int damage) // 플레이어가 데미지를 받았을 때 실행 시키는 로직
        {
            return ChangeHP_IsDeath(HP - damage);
        }

        public bool ChangeHP_IsDeath(int targetHP) // 플레이어가 데미지를 받지는 않았지만 플레이어의 체력이 변했을 때
        {
            HP = targetHP;
            if (HP <= 0)
            {
                GM.GameOver();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
