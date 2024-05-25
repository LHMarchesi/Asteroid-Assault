using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame
{
    public class LevelManager
    {
        Font font = new Font("assets/Fonts/Fuente.ttf", 24);
        private IntPtr gameScreen = Engine.LoadImage("assets/BackGround.png");
        public List<GameObject> GameObjects = new List<GameObject>();

        private Player player = new Player(new Vector2(565, 520));
        public Player Player => player;

        RandomNumber random = new RandomNumber();
        private Time _time;

        public void Initialize()
        {
            
            _time.Initialize();
            EnemySpawner();
        }
        public void Update()
        {
            player.Update();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }



            _time.Update();
           
        }

        public void Render()
        {
            Engine.Clear();

            Engine.Draw(gameScreen, 0, 0);

            player.Render();



            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Render();
            }

            Engine.DrawText($"{Math.Max(0, (int)Time.timeElapse)}", 640, 10, 255, 255, 255, font);


            Engine.Show();
        }

        public void EnemySpawner()
        {
            

            GameObjects.Add(AsteroidFactory.CreateAsteroid(Asteroid.SetRandomPosition(), AsteroidType.slow));
            GameObjects.Add(AsteroidFactory.CreateAsteroid(Asteroid.SetRandomPosition(), AsteroidType.slow));
            GameObjects.Add(AsteroidFactory.CreateAsteroid(Asteroid.SetRandomPosition(), AsteroidType.fast));
            
        } // Spawnea enemigos en posicion aleatoria 
    }
}
