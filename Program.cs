namespace SpartaDungeon
{
    internal class Program
    {
        public class Item
        {

        }

        
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            EventManager eventManager = new EventManager(gameManager); // 게임매니저를 생성하고 그 게임매니저를 이벤트 매니저의 매개변수에 넣어준다.
            eventManager.StartGame();
            eventManager.RunGame();
        }
    }
}
