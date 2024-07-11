using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame
{
    public class LevelManager
    {
        public event Action<LevelManager> OnLevelStart;
        public event Action<LevelManager> OnLevelEnd;

        public List<GameObject> GameObjects = new List<GameObject>();
        private SpawnHandler spawnHandler = new SpawnHandler();
        private PowerUpManager powerUpManager = new PowerUpManager();

        private Player player = new Player(new Vector2(565, 520));
        public Player Player => player;

        private float backgroundY = -3376;
        static public float backgroundSpeed = 1f;

        public string totalShieldTxt = "0";

        Font font = new Font("assets/Fonts/Fuente.ttf", 24);
        public Time _time;

         public void Initialize()
        {
            _time.Initialize();
            powerUpManager.ResetPowerUps();
        }

        public void Update()
        {
            player.Update();
            _time.Update();
            spawnHandler.Spawner();
            powerUpManager.Handler();
 
            backgroundY += backgroundSpeed;

            if (Time.timeElapse > Time.winTime) // Win Condition
            {
                GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.win);
                //OnLevelEnd.Invoke(this);
            }

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }
        }

        public void Render()
        {
            Engine.Clear();

            Engine.Draw(ScreenManager.Instance.gameScreen, 0, backgroundY, 1280, 4096);

            player.Render();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Render();
            }

            Engine.DrawText($"{Math.Max(0, (int)Time.timeElapse)}", 640, 10, 255, 255, 255, font);
            Engine.DrawText("Escudos totales : " + PowerUpManager.totalShieldtxt, 10, 10, 255, 255, 255, font);
            Engine.DrawText("Balas totales : " + PowerUpManager.shootTxt, 1000, 10, 255, 255, 255, font);
            Engine.Show();
        }

        public void RenderPauseMenu()
        {
            Engine.Clear();

            Engine.Draw(ScreenManager.Instance.pauseScreen, 0, 0);

            Engine.Show();
        }

    }
}
