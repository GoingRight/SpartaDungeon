using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
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
                Console.WriteLine("5. 던전입장");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요");
                Console.Write(">>");
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
                    case 5:
                        EnterDungeon();
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
            if (GM.player.EquipedWeapon != null)
            {
                Console.WriteLine($"장착 중인 무기 : {GM.player.EquipedWeapon.Name}");
            }
            if (GM.player.EquipedArmor != null)
            {
                Console.WriteLine($"장착 중인 방어구 : {GM.player.EquipedArmor.Name}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
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
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");

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
            Console.WriteLine("0. 나가기"); //여기까지는 인벤토리 메서드와 크게 다르지 않음
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
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
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
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
            if (item is Weapon weapon)
            {
                if (GM.player.EquipedWeapon == null)
                {
                    GM.player.EquipedWeapon = weapon;
                    weapon.Equip(GM.player);
                }
                else if (GM.player.EquipedWeapon != null)
                {
                    GM.player.EquipedWeapon.Unequip(GM.player);
                    weapon.Equip(GM.player);
                    GM.player.EquipedWeapon = weapon;
                }
            }
            else if (item is Armor armor)
            {
                if (GM.player.EquipedArmor == null)
                {
                    GM.player.EquipedArmor = armor;
                    armor.Equip(GM.player);
                }
                else if (GM.player.EquipedArmor != null)
                {
                    GM.player.EquipedArmor.Unequip(GM.player);
                    armor.Equip(GM.player);
                    GM.player.EquipedWeapon = armor;
                }
            }
        }
        public void Unequip(Item item) // 장착 해제
        {
            item.Unequip(GM.player);
            if (item is Weapon weapon)
            {
                GM.player.EquipedWeapon = null;
            }
            else if (item is Armor armor)
            {
                GM.player.EquipedArmor = null;
            }
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
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
            choice = int.Parse(Console.ReadLine());
            while (choice != 0 && choice != 1 && choice != 2)
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                choice = int.Parse(Console.ReadLine());
            }
            if (choice == 1)
            {
                choice = -1;
                ItemShopping();
            }
            else if (choice == 2)
            {
                choice = -1;
                ItemSell();
            }
            else if (choice == 0)
            {
                Console.Clear();
            }
            choice = -1;
        }

        public void ItemShopping()//아이템 구매 메서드--------------------------------------------------------------------------------------
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
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
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
            while (choice != 0)
            {
                choice = int.Parse(Console.ReadLine());
                if (choice > 0 && choice <= STR.items.Count)
                {
                    STR.Buy(STR.items[choice - 1]);  //-------------------------------구매 후 상점에 구매 현황을 업데이트 (Buy 메서드에서 콘솔클리어는 이루어짐)
                    Console.WriteLine("상점 - 아이템 구매");
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
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.Write(">>");
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

        public void ItemSell()//아이템 판매 메서드------------------------------------------------------------------------------
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GM.player.Gold} G");
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
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                if (choice > 0 && choice <= GM.player.items.Count)
                {
                    if (GM.player.items[choice - 1].isEquiped == true)
                    {
                        Unequip(GM.player.items[choice - 1]);
                    }
                    STR.Sell(GM.player.items[choice - 1]);
                    Console.Clear();
                    Console.WriteLine("상점 - 아이템 판매"); //상점 창 최신화를 위한 코드-------------------------------------------------------------------------
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                    Console.WriteLine();
                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine($"{GM.player.Gold} G");
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
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    choice = int.Parse(Console.ReadLine());
                }
                else if (choice < 0 || choice > GM.player.items.Count)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    choice = int.Parse(Console.ReadLine());
                }
            }
            Console.Clear();
            choice = -1;
        }

        public void Rest() //휴식 메서드------------------------------------------------------------------------------------
        {
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 100 회복할 수 있습니다. (보유골드 : {GM.player.Gold})");
            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0.나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요");
            Console.Write(">>");
            choice = int.Parse(Console.ReadLine());
            while (choice != 0 && choice != 1)
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
            else if (choice == 0)
            {
                Console.Clear();
            }
            Console.Clear();
            choice = -1;
        }

        public void EnterDungeon()  //던전 입장 메서드--------------------------------------------------------------------------------------------
        {
            Console.Clear();
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 쉬운 던전 | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전 | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전 | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            choice = int.Parse(Console.ReadLine());
            while (choice != 0 && choice != 1 && choice != 2 && choice != 3)
            {
                Console.WriteLine("잘못된 입력입니다.");
                choice = int.Parse(Console.ReadLine());
            }
            Dungeon();
            choice = -1;
            choice = int.Parse(Console.ReadLine());
            while (choice != 0)
            {
                Console.WriteLine("잘못된 입력입니다.");
                choice = int.Parse(Console.ReadLine());
            }
            Console.Clear();
            choice = -1;
        }

        public void Dungeon() //던전 실행 메서드--------------------------------------------------------------------------------------------------
        {
            Console.Clear();
            Random random = new Random();
            int difficulty = 0;
            int successpossibility = random.Next(0, 10);
            int damagePossibility = random.Next(20, 36);
            int recommendedDeffense = 0;
            int basicReward = 0;
            int extraReward = random.Next(GM.player.AttackDamage, GM.player.AttackDamage * 2 + 1);
            if (choice == 1)
            {
                difficulty = 1;
                recommendedDeffense = 5;
                basicReward = 1000;
                if (GM.player.Deffense >= recommendedDeffense)
                {
                    SuccessDungeon(damagePossibility, recommendedDeffense, basicReward, extraReward, difficulty);
                }
                else
                {
                    if (successpossibility < 4) //40% 확률로 실패
                    {
                        FailDungeon();
                    }
                    else
                    {
                        SuccessDungeon(damagePossibility, recommendedDeffense, basicReward, extraReward, difficulty);
                    }
                }
            }
            else if (choice == 2)
            {
                difficulty = 2;
                recommendedDeffense = 11;
                basicReward = 1700;
                if (GM.player.Deffense >= recommendedDeffense)
                {
                    SuccessDungeon(damagePossibility, recommendedDeffense, basicReward, extraReward, difficulty);
                }
                else
                {
                    if (successpossibility < 4) //40% 확률로 실패
                    {
                        FailDungeon();
                    }
                    else
                    {
                        SuccessDungeon(damagePossibility, recommendedDeffense, basicReward, extraReward, difficulty);
                    }
                }
            }
            else if (choice == 3)
            {
                difficulty = 3;
                recommendedDeffense = 17;
                basicReward = 2500;
                if (GM.player.Deffense >= recommendedDeffense)
                {
                    SuccessDungeon(damagePossibility, recommendedDeffense, basicReward, extraReward, difficulty);
                }
                else
                {
                    if (successpossibility < 4) //40% 확률로 실패
                    {
                        FailDungeon();
                    }
                    else
                    {
                        SuccessDungeon(damagePossibility, recommendedDeffense, basicReward, extraReward, difficulty);
                    }
                }
            }
        }

        public void SuccessDungeon(int damagePossibility, int recommendedDeffense, int basicReward, int extraReward, int difficulty)//던전 공략 성공-------------------------------------
        {
            int beforeHP = GM.player.HP;
            int beforeGold = GM.player.Gold;
            extraReward = extraReward * basicReward / 100;
            int damage = damagePossibility - (recommendedDeffense - GM.player.Deffense);
            if (GM.player.TakeDamage_IsDeath(damage)) return;

            GM.player.Gold += basicReward;
            GM.player.Gold += extraReward;
            Console.WriteLine("던전 클리어");
            Console.WriteLine("축하합니다!");
            if (difficulty == 1)
            {
                Console.WriteLine("쉬운 던전을 클리어 하였습니다.");
            }
            else if (difficulty == 2)
            {
                Console.WriteLine("일반 던전을 클리어 하였습니다.");
            }
            else if (difficulty == 3)
            {
                Console.WriteLine("어려운 던전을 클리어 하였습니다.");
            }
            Console.WriteLine();
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 : {beforeHP} -> {GM.player.HP}");
            Console.WriteLine($"Gold : {beforeGold} G -> {GM.player.Gold} G");
            GM.player.GiveEXP(1);
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
        }

        public void FailDungeon()//던전 공략 실패-----------------------------------------------------------------
        {
            if (GM.player.ChangeHP_IsDeath(GM.player.HP/2)) return;
            Console.WriteLine("던전 공략 실패");
            Console.WriteLine("던전 공략에 실패하였습니다. 체력이 절반으로 감소합니다.ㅠㅠ");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

        }
    }
}

