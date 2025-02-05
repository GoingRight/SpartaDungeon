using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeon.Program;

namespace SpartaDungeon
{
    public class GameManager
    {
        public bool isGameOver = false;
        public Player player;
        public GameManager() 
        {
            player = new Player(this);
        }
        public void GameOver()
        {
            isGameOver = true;
            Console.WriteLine("게임 오버");
        }
        
    }
}
