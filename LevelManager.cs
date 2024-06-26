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
        private SpawnHandler spawnHandler = new SpawnHandler();
        public List<GameObject> GameObjects = new List<GameObject>();

        private Player player = new Player(new Vector2(565, 520));
        public Player Player => player;

        private float backgroundY = -3376;
        static public float backgroundSpeed = 1f;

        Font font = new Font("assets/Fonts/Fuente.ttf", 24);

        public Time _time;

        // Escenas de juego
        public IntPtr winScreen = Engine.LoadImage("assets/Win.png");
        public IntPtr loseScreen = Engine.LoadImage("assets/Lose.png");
        public IntPtr gameScreen = Engine.LoadImage("assets/BackGround.jpg");
        public IntPtr menuScreen = Engine.LoadImage("assets/MainMenu.jpg");
        public IntPtr pauseScreen = Engine.LoadImage("assets/Pause.jpg");
        public IntPtr skinScreen = Engine.LoadImage("assets/SkinSelector.jpg");

        //Herramientas Seleccion Skin
        public IntPtr boxSelector = Engine.LoadImage("assets/BoxSelector.png");
        public Vector2 skin1 = new Vector2(20,110);

        //Escudo
        public IntPtr shieldIsPicked = Engine.LoadImage("assets/ShieldShip/0.png");


        public void Initialize()
        {
            _time.Initialize();

            if (Player.ship1)
            {
                shieldIsPicked = Engine.LoadImage("assets/ShieldShip/0.png");
            }
            else if (Player.ship2)
            {
                shieldIsPicked = Engine.LoadImage("assets/ShieldShip/1.png");
            }
            else if (Player.ship3)
            {
                shieldIsPicked = Engine.LoadImage("assets/ShieldShip/2.png");
            }
        }
        public void Update()
        {
            player.Update();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }

            spawnHandler.Spawner();
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
            Engine.DrawText("Escudos totales : " + Shield.totalShieldtxt, 10, 10, 255, 255, 255, font);
            Engine.DrawText("Balas totales : " + ShootPowerUp.totalShootstxt, 1000, 10, 255, 255, 255, font);
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
