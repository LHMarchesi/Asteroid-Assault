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
        private DifficultManager difficultManager = new DifficultManager();
        public List<GameObject> GameObjects = new List<GameObject>();

        private Player player = new Player(new Vector2(565, 520));
        public Player Player => player;

        private float backgroundY = -3376;
        static public float backgroundSpeed = 1f;
        

        Font font = new Font("assets/Fonts/Fuente.ttf", 24);

        private Time _time;

        // Escenas de juego
        public IntPtr winScreen = Engine.LoadImage("assets/Win.png");
        public IntPtr loseScreen = Engine.LoadImage("assets/Lose.png");
        public IntPtr gameScreen = Engine.LoadImage("assets/BackGround.jpg");
        public IntPtr menuScreen = Engine.LoadImage("assets/MainMenu.jpg");
        public IntPtr pauseScreen = Engine.LoadImage("assets/Pause.jpg");

        //Escudo
        public IntPtr shieldIsPicked = Engine.LoadImage("assets/ShieldShip/0.png");

        
        public void Initialize()
        {

            _time.Initialize();

        }
        public void Update()
        {
            player.Update();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }

            difficultManager.Spawner();
            _time.Update();

            backgroundY += backgroundSpeed;
        }

        public void Render()
        {
            Engine.Clear();

            Engine.Draw(gameScreen, 0, backgroundY, 1280, 4096);
            player.Render();

            if (Shield.IsPicked) // Si tiene el escudo
            {
                Engine.Draw(shieldIsPicked, player.Transform.Position.x - 25, player.Transform.Position.y - 25);
            }

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Render();
            }



            Engine.DrawText($"{Math.Max(0, (int)Time.timeElapse)}", 640, 10, 255, 255, 255, font);
            Engine.DrawText("Escudos totales : " + Shield.totalShieldtxt , 10, 10, 255, 255, 255, font);
            Engine.DrawText("Balas totales : " + ShootPowerUp.totalShootstxt , 1000, 10, 255, 255, 255, font);
            Engine.Show();
        }

        public void RenderPauseMenu()
        {
            Engine.Clear();

            Engine.Draw(pauseScreen, 0, 0);

            Engine.Show();
        }

    }
}
