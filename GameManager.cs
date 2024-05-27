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
        private LevelManager levelManager;
        static public GameStatus gameStart = GameStatus.menu;
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
                        gameStart = GameStatus.game;
                    }
                    break;

                case GameStatus.game:

                    levelManager.Update();
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
            }
        }

        public void Render()
        {
            switch (gameStart)
            {
                case GameStatus.menu:

                    Engine.Clear();
                    Engine.Draw(levelManager.menuScreen, 0, 0);
                    Engine.Show();
                    break;

                case GameStatus.game:

                    levelManager.Render();
                    break;

                case GameStatus.win:

                    Engine.Clear();
                    Engine.Draw(levelManager.winScreen, 0, 0);
                    Engine.Show();
                    break;

                case GameStatus.lose:

                    Engine.Clear();
                    Engine.Draw(levelManager.loseScreen, 0, 0);
                    Engine.Show();
                    break;
            }

        }

        public void ChangeGameStatus(GameStatus newStatus)
        {
            gameStart = newStatus;
        }
    }
}
