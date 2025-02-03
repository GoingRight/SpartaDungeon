using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{
    public class Store
    {
        public Store(GameManager gameManager) // 생성자
        {
            GM = gameManager;
        }
        GameManager GM;

        public List<Item> items = new List<Item>(); // 상점 아이템 리스트

        public void InitStore()
        {
            items.Add(weapon1);
            items.Add(weapon2);
            items.Add(weapon3);
            items.Add(armor1);
            items.Add(armor2);
            items.Add(armor3);
        }
        public void Buy(Item item)
        {
            if (GM.player.Gold >= item.Price && item.isSold == false)
            {
                GM.player.Gold -= item.Price;
                GM.player.items.Add(item);
                item.isSold = true;
                Console.WriteLine("구매를 완료했습니다.(Enter를 눌러 확인)");
                Console.ReadLine();
            }
            else if(GM.player.Gold >= item.Price && item.isSold == true)
            {
                Console.WriteLine("이미 구매한 아이템입니다.(Enter를 눌러 확인)");
                Console.ReadLine();
            }
            else if(GM.player.Gold < item.Price)
            {
                Console.WriteLine("Gold가 부족합니다.(Enter를 눌러 확인)");
                Console.ReadLine();
            }
        }
        

        //무기-------------------------------------------------------------
        Item weapon1 = new Weapon("낡은 검", 100, 2,"어디서든 구할 수 있는 흔한 검이다.");
        Item weapon2 = new Weapon("스파르타의 창", 500, 13, "스파르타의 전사들이 사용했다는 전설의 창");
        Item weapon3 = new Weapon("예리한 단검", 300, 7, "잘 연마되어 반짝이는 단검. 그러나 무척 짧다.");


        //방어구-----------------------------------------------------------
        Item armor1 = new Armor("무쇠갑옷", 300, 5, "무거운 갑옷. 무게에 비해 효율은 좋아 보이지 않는다.");
        Item armor2 = new Armor("기사단복", 800, 12, "마법이 깃든 천옷. 웬만한 웬만한 중갑 못지 않다.");
        Item armor3 = new Armor("여왕의 축복", 1500, 30, "여왕의 축복이 담긴 금제 갑옷. 보석을 이용한 어깨장식이 특징.");
    }
}
