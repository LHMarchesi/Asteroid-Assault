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
        private ScreenManager screenManager;
        private LevelManager levelManager;
               
        public LevelManager LevelManager => levelManager;
        private ShootPowerUp shootPowerUp = new ShootPowerUp(new Vector2(0, 0)); //☻

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
                        ScreenManager.Instance.skin1.x = 20;
                        Player.ship1 = true;
                        Player.ship2 = false;
                        Player.ship3 = false;
                    }
                    if (Engine.KeyPress(Engine.KEY_2)) 
                    {
                        ScreenManager.Instance.skin1.x = 385;
                        Player.ship1 = false;
                        Player.ship2 = true;
                        Player.ship3 = false;
                    }
                    if (Engine.KeyPress(Engine.KEY_3)) 
                    {
                        ScreenManager.Instance.skin1.x = 760;
                        Player.ship1 = false;
                        Player.ship2 = false;
                        Player.ship3 = true;
                    }
                    if (Engine.KeyPress(Engine.KEY_ESP)) 
                    {
                        Initialize();
                        Console.WriteLine("Juego emepzo");
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
                        shootPowerUp.Reset();  // Quizas utilizar un evento? 
                        Shield.IsPicked = false; //
                        SpeedUp.isPicked = false; //
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
                    Engine.Draw(ScreenManager.Instance.menuScreen, 0, 0);  // Utilizar ScreenManager para la carga de escenas ?
                    Engine.Show();
                    break;

                case GameStatus.skinselector:
                    Engine.Clear();
                    Engine.Draw(ScreenManager.Instance.skinScreen, 0, 0); //
                    Engine.Draw(ScreenManager.Instance.boxSelector, ScreenManager.Instance.skin1.x, ScreenManager.Instance.skin1.y);//
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
                    Engine.Draw(ScreenManager.Instance.winScreen, 0, 0);//
                    Engine.Show();
                    break;

                case GameStatus.lose:

                    Engine.Clear();
                    Engine.Draw(ScreenManager.Instance.loseScreen, 0, 0);//
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
