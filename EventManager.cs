using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeon.Program;

namespace SpartaDungeon
{
    public class EventManager
    {
        private int choice = -1;
        //생성자에 매개변수를 달아서 GM은 게임매니저임을 이벤트 매니저에게 알려준다.
        public EventManager(GameManager gameManager)
        {
            GM = gameManager;
        }
        private GameManager GM;

        public void StartGame() // 게임시작 플레이어 캐릭터 생성----------------------------------------------------------
        {
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
            Console.WriteLine("공격력 : " + GM.player.AttackDamage);
            Console.WriteLine("방어력 : " + GM.player.Deffense);
            Console.WriteLine("체 력 : " + GM.player.HP);
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
            for (int i = 0; i < GM.player.items.Count; i++)
            {
                if (GM.player.items[i].isEquiped == true)
                {
                    Console.WriteLine($"- [E]{GM.player.items[i].Name}");
                }
                else
                {
                    Console.WriteLine($"- {GM.player.items[i].Name}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            choice = int.Parse(Console.ReadLine());
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
        public void Store() // 상점 메서드----------------------------------------------------------------------------
        {

        }

        public void EquipmentManage() // 인벤토리-장착관리 메서드------------------------------------------------------
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
                    Console.WriteLine($"- {i + 1} [E]{GM.player.items[i].Name}");
                }
                else
                {
                    Console.WriteLine($"- {i + 1} {GM.player.items[i].Name}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            while (choice != 0)
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
                    Console.Clear();
                    Console.Clear();
                    Console.WriteLine("인벤토리");
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < GM.player.items.Count; i++)
                    {
                        if (GM.player.items[i].isEquiped == true)
                        {
                            Console.WriteLine($"- {i + 1} [E]{GM.player.items[i].Name}");
                        }
                        else
                        {
                            Console.WriteLine($"- {i + 1} {GM.player.items[i].Name}");
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
    }
}

