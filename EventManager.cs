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
        //생성자에 매개변수를 달아서 GM은 게임매니저임을 이벤트 매니저에게 알려준다.
        public EventManager(GameManager gameManager)
        {
            GM = gameManager;
        }
        private GameManager GM;
        
        public void StartGame()
        {
            Console.WriteLine("당신의 이름을 입력해주세요.");
            GM.player.InitPlayer(Console.ReadLine());  //플레이어 생성
            //시작 멘트
            Console.Clear();
            Console.WriteLine($"스파르타 마을에 오신 {GM.player.Name}님 환영합니다.");
            Console.WriteLine("이곳에서 던전에 입장하기 전 활동을 할 수 있습니다.");
        }

        public void RunGame()
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
                    RunGame();
                    break;
            }
        }
        public void PlayerInfo()
        {
            Console.Clear();
            Console.WriteLine("Lv. " + GM.player.Lv);
        }

        public void Inventory()
        {

        }
        public void Store()
        {

        }
    }
}

