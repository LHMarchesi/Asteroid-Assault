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

        private Player player = new Player(new Vector2(565, 520));
        public Player Player => player;

        private float backgroundY = -3376;
        static public float backgroundSpeed = 1f;

        Font font = new Font("assets/Fonts/Fuente.ttf", 24);
        public Time _time;


        public void Initialize()
        {
            _time.Initialize();
            //OnLevelStart.Invoke(this);
        }

        public void Update()
        {
            player.Update();
            _time.Update();
            spawnHandler.Spawner();

            backgroundY += backgroundSpeed;

            if (Time.timeElapse > Time.winTime)
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
            Engine.DrawText("Escudos totales : " + Shield.totalShieldtxt, 10, 10, 255, 255, 255, font);
            Engine.DrawText("Balas totales : " + ShootPowerUp.totalShootstxt, 1000, 10, 255, 255, 255, font);
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
