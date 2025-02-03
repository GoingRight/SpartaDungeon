using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
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

        public void Buy()
        {
            GM.player.items.Add(item1);
            GM.player.items.Add(item2);
        }

        //무기-------------------------------------------------------------
        Item item1 = new Weapon("낡은 검", 100, 2);




        //방어구-----------------------------------------------------------
        Item item2 = new Armor("무쇠갑옷", 300, 5);
    }
}
