using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

namespace MyGame
{
    public class GameManager
    {
        public enum GameStatus
        {
            menu, skinselector, game, win, lose, credits, pause
        }

        static public GameStatus gameStart = GameStatus.menu;

        private static GameManager instance;
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

            switch (gameStart) // Estados del juego
            {
                case GameStatus.menu:

                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        Initialize();
                        gameStart = GameStatus.skinselector;
                    }
                    break;

                case GameStatus.skinselector:

                    if (Engine.KeyPress(Engine.KEY_1))
                    {
                        ScreenManager.Instance.boxPosition.x = 20;
                        Player.shipBlue = true;
                        Player.shipRed = false;
                        Player.shipGreen = false;
                    }
                    if (Engine.KeyPress(Engine.KEY_2))
                    {
                        ScreenManager.Instance.boxPosition.x = 385;
                        Player.shipBlue = false;
                        Player.shipRed = true;
                        Player.shipGreen = false;
                    }
                    if (Engine.KeyPress(Engine.KEY_3))
                    {
                        ScreenManager.Instance.boxPosition.x = 760;
                        Player.shipBlue = false;
                        Player.shipRed = false;
                        Player.shipGreen = true;
                    }
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        Initialize();
                        gameStart = GameStatus.game;
                    }

                    break;

                case GameStatus.game:

                    levelManager.Update();

                    if (Engine.KeyPress(Engine.KEY_ESC))
                    {
                        gameStart = GameStatus.pause;
                    }
                    break;

                case GameStatus.win:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        Initialize();
                        gameStart = GameStatus.game;
                    }
                    break;

                case GameStatus.lose:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        Initialize();
                        gameStart = GameStatus.game;
                    }
                    break;

                case GameStatus.pause:
                    if (Engine.KeyPress(Engine.KEY_P))
                    {
                        gameStart = GameStatus.game;
                    }
                    if (Engine.KeyPress(Engine.KEY_R))
                    {
                        Initialize();
                        gameStart = GameStatus.game;

                    }
                    if (Engine.KeyPress(Engine.KEY_X))
                    {
                        Engine.ErrorFatal("Quit");
                    }
                    break;
            }
        }

        public void Render()
        {
            switch (gameStart)
            {
                case GameStatus.menu:

                    Engine.Clear();
                    Engine.Draw(ScreenManager.Instance.menuScreen, 0, 0);
                    Engine.Show();
                    break;

                case GameStatus.skinselector:
                    Engine.Clear();
                    Engine.Draw(ScreenManager.Instance.skinScreen, 0, 0);
                    Engine.Draw(ScreenManager.Instance.boxSelector, ScreenManager.Instance.boxPosition.x, ScreenManager.Instance.boxPosition.y);
                    Engine.Show();
                    break;

                case GameStatus.pause:

                    levelManager.RenderPauseMenu();
                    break;

                case GameStatus.game:

                    levelManager.Render();
                    break;

                case GameStatus.win:

                    Engine.Clear();
                    Engine.Draw(ScreenManager.Instance.winScreen, 0, 0);
                    Engine.Show();
                    break;

                case GameStatus.lose:

                    Engine.Clear();
                    Engine.Draw(ScreenManager.Instance.loseScreen, 0, 0);
                    Engine.Show();
                    break;
            }

        }

        public void ChangeGameStatus(GameStatus newStatus)
        {
            gameStart = newStatus;
        }

        public GameStatus GetCurrentGameStatus()
        {
            return gameStart;
        }

    }
}
