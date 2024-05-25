using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GameManager
    {
        public enum GameStatus
        {
            menu, game, win, lose, credits
        }

        private static GameManager instance;
        static public GameStatus gameStart = GameStatus.menu;

        private IntPtr menuScreen = Engine.LoadImage("assets/MainMenu.png");
        private IntPtr winScreen = Engine.LoadImage("assets/Win.png");
        private IntPtr loseScreen = Engine.LoadImage("assets/Lose.png");
        Font font = new Font("assets/Fonts/Fuente.ttf", 24);

        private LevelManager levelManager;
        public LevelManager LevelManager => levelManager;

        public static GameManager Instance

        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public void Initialize()
        {
            levelManager = new LevelManager();
            levelManager.Initialize();
        }

        public void Update()
        {

            switch (gameStart)
            {
                case GameStatus.menu:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {

                        gameStart = GameStatus.game;
                    }
                    break;

                case GameStatus.game:

                    levelManager.Update();

                    break;

                case GameStatus.win:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        gameStart = GameStatus.game;

                    }
                    break;
                case GameStatus.lose:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        gameStart = GameStatus.menu;
                    }
                    break;
                    //.....
            }
        }

        public void Render()
        {
            switch (gameStart)
            {
                case GameStatus.menu:
                    Engine.Clear();
                    Engine.Draw(menuScreen, 0, 0);
                    Engine.Show();

                    break;

                case GameStatus.game:

                    levelManager.Render();

                    Engine.DrawText($"{Math.Max(0, (int)Time.timeElapse)}", 640, 10, 255, 255, 255, font);

                    break;

                case GameStatus.win:
                    Engine.Clear();

                    Engine.Draw(winScreen, 0, 0);
                    Engine.Show();
                    break;
                case GameStatus.lose:
                    Engine.Clear();

                    Engine.Draw(loseScreen, 0, 0);
                    Engine.Show();
                    break;
                    //.....
            }

        }

        public void ChangeGameStatus(GameStatus newStatus)
        {
            gameStart = newStatus;
        }





    }
}
