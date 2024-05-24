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
        private LevelManager levelManager = new LevelManager(Player.player);

        private IntPtr menuScreen = Engine.LoadImage("assets/MainMenu.png");
        private IntPtr winScreen = Engine.LoadImage("assets/Win.png");
        private IntPtr loseScreen = Engine.LoadImage("assets/Lose.png");
        private IntPtr gameScreen = Engine.LoadImage("assets/BackGround.png");

        Font font = new Font("assets/Fonts/Fuente.ttf", 24);
        private static Time _time;
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

        public void Update()
        {

            switch (gameStart)
            {
                case GameStatus.menu:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        _time.Initialize();
                        gameStart = GameStatus.game;
                    }
                    break;

                case GameStatus.game:
                    _time.Update();
                    Player.player.Update();
                    levelManager.Update();

                    break;

                case GameStatus.win:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        gameStart = GameStatus.game;
                        _time.Initialize();
                    }
                    break;
                case GameStatus.lose:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        gameStart = GameStatus.menu;
                        _time.Initialize();
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
                    Engine.Clear();
                    Engine.Draw(gameScreen, 0, 0);
                    Player.player.Render();
                    Engine.DrawText($"{Math.Max(0, (int)Time.timeElapse)}", 640, 10, 255, 255, 255, font);

                    Engine.Show();
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
