using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeon.Program;

namespace SpartaDungeon
{
    public class EventManager
    {
        private int choice = -1;
        //생성자에 매개변수를 달아서 GM은 게임매니저임을 이벤트 매니저에게 알려준다.
        public EventManager(GameManager gameManager, Store store)
        {
            GM = gameManager;
            STR = store;
        }
        private GameManager GM;
        private Store STR;

        public void StartGame() // 게임시작 플레이어 캐릭터 생성----------------------------------------------------------
        {
            STR.InitStore();
            Console.WriteLine("당신의 이름을 입력해주세요.");
            GM.player.InitPlayer(Console.ReadLine());  //플레이어 생성
            //시작 멘트
            Console.Clear();
            Console.WriteLine($"스파르타 마을에 오신 {GM.player.Name}님 환영합니다.");
            Console.WriteLine("이곳에서 던전에 입장하기 전 활동을 할 수 있습니다.");
        }

        public void RunGame() // 게임 실행(게임 오버 전까지 계속 반복)--------------------------------------------------
        {
            while (GM.isGameOver == false)
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine("4. 휴식하기");
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Console.Clear();
                        PlayerInfo();
                        break;
                    case 2:
                        Console.Clear();
                        Inventory();
                        break;
                    case 3:
                        Console.Clear();
                        Store();
                        break;
                    case 4:
                        Console.Clear();
                        Rest();
                        break;
                        
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못된 입력입니다. 다시 한번 확인해 주시기 바랍니다.");
                        break;
                }
            }
        }
        public void PlayerInfo() // 상태 보기 메서드------------------------------------------------------
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("Lv. " + GM.player.Lv);
            Console.WriteLine($"{GM.player.Name} ({GM.player.Class})");
            if (GM.player.ExtraAttackDamage > 0)
            {
                Console.WriteLine($"공격력 : {GM.player.AttackDamage} (+{GM.player.ExtraAttackDamage})");
            }
            else
            {
                Console.WriteLine($"공격력 : {GM.player.AttackDamage}");
            }
            if (GM.player.ExtraDeffense > 0)
            {
                Console.WriteLine($"방어력 : {GM.player.Deffense} (+{GM.player.ExtraDeffense})");
            }
            else
            {
                Console.WriteLine($"방어력 : {GM.player.Deffense}");
            }
            Console.WriteLine($"체 력 : {GM.player.HP} / {GM.player.MaxHP}");
            Console.WriteLine("Gold : " + GM.player.Gold);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                choice = int.Parse(Console.ReadLine());
            }
            Console.Clear();
            choice = -1;
        }

        public void Inventory() // 인벤토리 메서드---------------------------------------------------
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < GM.player.items.Count; i++) // 아이템 목록 나열
            {
                if (GM.player.items[i].isEquiped == true) //장착된 장비
                {
                    Console.Write($"- [E]{GM.player.items[i].Name}");
                    if (GM.player.items[i] is Weapon weapon) // 장착된 무기
                    {
                        Console.Write($" | 공격력 +{weapon.AttackDamage}");
                        Console.WriteLine($" | {weapon.Inform}");
                    }
                    else if (GM.player.items[i] is Armor armor) // 장착된 방어구
                    {
                        Console.Write($" | 방어력 +{armor.Deffense}");
                        Console.WriteLine($" | {armor.Inform}");
                    }
                }
                else //장착되지 않은 장비
                {
                    Console.Write($"- {GM.player.items[i].Name}");
                    if (GM.player.items[i] is Weapon weapon) //장착되지 않은 무기
                    {
                        Console.Write($" | 공격력 +{weapon.AttackDamage}");
                        Console.WriteLine($" | {weapon.Inform}");
                    }
                    else if (GM.player.items[i] is Armor armor) // 장착되지 않은 방어구
                    {
                        Console.Write($" | 방어력 +{armor.Deffense}");
                        Console.WriteLine($" | {armor.Inform}");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            choice = int.Parse(Console.ReadLine()); //선택 입력
            while (choice != 0 && choice != 1)
            {
                Console.WriteLine("잘못된 입력입니다.");
                choice = int.Parse(Console.ReadLine());
            }
            if (choice == 1)
            {
                choice = -1;
                EquipmentManage();
            }
            else
            {
                Console.Clear();
            }
            choice = -1;
        }

        public void EquipmentManage() // 인벤토리-장착관리 메서드------------------------------------------------------인벤토리 아이템의 이름 앞에 번호가 매겨짐
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < GM.player.items.Count; i++)
            {
                if (GM.player.items[i].isEquiped == true)
                {
                    Console.Write($"- {i + 1} [E]{GM.player.items[i].Name}");
                    if (GM.player.items[i] is Weapon weapon)
                    {
                        Console.Write($" | 공격력 +{weapon.AttackDamage}");
                        Console.WriteLine($" | {weapon.Inform}");
                    }
                    else if (GM.player.items[i] is Armor armor)
                    {
                        Console.Write($" | 방어력 +{armor.Deffense}");
                        Console.WriteLine($" | {armor.Inform}");
                    }
                }
                else
                {
                    Console.Write($"- {i + 1} {GM.player.items[i].Name}");
                    if (GM.player.items[i] is Weapon weapon)
                    {
                        Console.Write($" | 공격력 +{weapon.AttackDamage}");
                        Console.WriteLine($" | {weapon.Inform}");
                    }
                    else if (GM.player.items[i] is Armor armor)
                    {
                        Console.Write($" | 방어력 +{armor.Deffense}");
                        Console.WriteLine($" | {armor.Inform}");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기"); //---------------------------- 여기까지는 인벤토리 메서드와 크게 다르지 않음
            while (choice != 0) // 처음 메서드에 들어왔을 때 choice 값은 -1
            {
                choice = int.Parse(Console.ReadLine());
                if (choice > 0 && choice <= GM.player.items.Count)
                {
                    if (GM.player.items[choice - 1].isEquiped == false)
                    {
                        Equip(GM.player.items[choice - 1]);
                    }
                    else
                    {
                        Unequip(GM.player.items[choice - 1]);
                    }
                    Console.Clear();//--------------------------------------------------장비가 착용된 것을 바로 확인할 수 있게 지우고 새로 인터페이스 구성
                    Console.WriteLine("인벤토리");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < GM.player.items.Count; i++)
                    {
                        if (GM.player.items[i].isEquiped == true)
                        {
                            Console.Write($"- {i + 1} [E]{GM.player.items[i].Name}");
                            if (GM.player.items[i] is Weapon weapon)
                            {
                                Console.Write($" | 공격력 +{weapon.AttackDamage}");
                                Console.WriteLine($" | {weapon.Inform}");
                            }
                            else if (GM.player.items[i] is Armor armor)
                            {
                                Console.Write($" | 방어력 +{armor.Deffense}");
                                Console.WriteLine($" | {armor.Inform}");
                            }
                        }
                        else
                        {
                            Console.Write($"- {i + 1} {GM.player.items[i].Name}");
                            if (GM.player.items[i] is Weapon weapon)
                            {
                                Console.Write($" | 공격력 +{weapon.AttackDamage}");
                                Console.WriteLine($" | {weapon.Inform}");
                            }
                            else if (GM.player.items[i] is Armor armor)
                            {
                                Console.Write($" | 방어력 +{armor.Deffense}");
                                Console.WriteLine($" | {armor.Inform}");
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                }
                else if (choice < 0 || choice > GM.player.items.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else if (choice == 0)
                {
                    Console.Clear();
                }
            }
        }
        public void Equip(Item item) // 장착
        {
            item.Equip(GM.player);
        }
        public void Unequip(Item item) // 장착 해제
        {
            item.Unequip(GM.player);
        }

        public void Store() // 상점 메서드----------------------------------------------------------------------------
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GM.player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < STR.items.Count; i++)
            {
                Console.Write($"- {STR.items[i].Name}");
                if (STR.items[i] is Weapon weapon)
                {
                    Console.Write($" | 공격력 +{weapon.AttackDamage}");
                }
                else if (STR.items[i] is Armor armor)
                {
                    Console.Write($" | 방어력 +{armor.Deffense}");
                }
                Console.Write($" | {STR.items[i].Inform}");
                if (STR.items[i].isSold == false)
                {
                    Console.WriteLine($" | {STR.items[i].Price}G");
                }
                else
                {
                    Console.WriteLine(" | 구매 완료");
                }
            }

            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            choice = int.Parse(Console.ReadLine());
            while (choice != 0 && choice != 1)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                choice = int.Parse(Console.ReadLine());
            }
            if (choice == 1)
            {
                choice = -1;
                ItemShopping();
            }
            else if (choice == 0)
            {
                Console.Clear();
            }
            choice = -1;
        }

        public void ItemShopping()//-----------------------------------------------아이템 구매 메서드
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GM.player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < STR.items.Count; i++)
            {
                Console.Write($"- {i + 1} {STR.items[i].Name}");
                if (STR.items[i] is Weapon weapon)
                {
                    Console.Write($" | 공격력 +{weapon.AttackDamage}");
                }
                else if (STR.items[i] is Armor armor)
                {
                    Console.Write($" | 방어력 +{armor.Deffense}");
                }
                Console.Write($" | {STR.items[i].Inform}");
                if (STR.items[i].isSold == false)
                {
                    Console.WriteLine($" | {STR.items[i].Price}G");
                }
                else
                {
                    Console.WriteLine(" | 구매 완료");
                }
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            while (choice != 0)
            {
                choice = int.Parse(Console.ReadLine());
                if (choice > 0 && choice <= STR.items.Count)
                {
                    STR.Buy(STR.items[choice - 1]);  //-------------------------------구매 후 상점에 구매 현황을 업데이트 (Buy 메서드에서 콘솔클리어는 이루어짐)
                    Console.WriteLine("상점");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine();
                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine($"{GM.player.Gold} G");
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < STR.items.Count; i++)
                    {
                        Console.Write($"- {i + 1} {STR.items[i].Name}");
                        if (STR.items[i] is Weapon weapon)
                        {
                            Console.Write($" | 공격력 +{weapon.AttackDamage}");
                        }
                        else if (STR.items[i] is Armor armor)
                        {
                            Console.Write($" | 방어력 +{armor.Deffense}");
                        }
                        Console.Write($" | {STR.items[i].Inform}");
                        if (STR.items[i].isSold == false)
                        {
                            Console.WriteLine($" | {STR.items[i].Price}G");
                        }
                        else
                        {
                            Console.WriteLine(" | 구매 완료");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                }
                else if (choice < 0 || choice > STR.items.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else // 0일 때
                {
                    Console.Clear();
                }
            }
        }

        public void Rest() //-------------------------------------휴식 기능
        {
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 100 회복할 수 있습니다. (보유골드 : {GM.player.Gold})");
            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0.나가기");
            choice = int.Parse(Console.ReadLine());
            while(choice != 0&&choice != 1)
            {
                Console.WriteLine("잘못된 입력입니다.");
                choice = int.Parse(Console.ReadLine());
            }
            if (choice == 1)
            {
                if (GM.player.Gold >= 500)
                {
                    GM.player.Gold -= 500;
                    GM.player.HP += 100;
                    if (GM.player.HP > GM.player.MaxHP)
                    {
                        GM.player.HP = GM.player.MaxHP;
                    }
                    Console.Clear();
                    Console.WriteLine("휴식을 완료했습니다.(Enter를 눌러 확인)");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Gold가 부족합니다.(Enter를 눌러 확인)");
                    Console.ReadLine();
                }
            }
            else if(choice == 0)
            {
                Console.Clear();
            }
            choice = -1;
        }
    }
}

