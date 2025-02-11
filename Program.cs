﻿namespace SpartaDungeon
{
    internal class Program
    {     
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            Store store = new Store(gameManager);
            EventManager eventManager = new EventManager(gameManager, store); // 게임매니저를 생성하고 그 게임매니저를 이벤트 매니저의 매개변수에 넣어준다.

            eventManager.StartGame();
            eventManager.RunGame();
        }
    }
}
