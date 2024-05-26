﻿using MyGame.assets;
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
        private IntPtr shieldIsPicked = Engine.LoadImage("assets/ShieldShip/0.png");
        public List<GameObject> GameObjects = new List<GameObject>();

        private Player player = new Player(new Vector2(565, 520));
        public Player Player => player;

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

            if (GameManager.Instance.LevelManager.Player.shieldPicked == true)
            {
                Engine.Draw(shieldIsPicked, player.Transform.Position.x - 25, player.Transform.Position.y - 25);
            }


            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Render();
            }

            Engine.DrawText($"{Math.Max(0, (int)Time.timeElapse)}", 640, 10, 255, 255, 255, font);

            Engine.Show();
        }

        public void EnemySpawner()// Spawnea enemigos en posicion aleatoria 
        {
            

            GameObjects.Add(AsteroidFactory.CreateAsteroid(Asteroid.SetRandomPosition(), AsteroidType.slow));
            GameObjects.Add(AsteroidFactory.CreateAsteroid(Asteroid.SetRandomPosition(), AsteroidType.slow));
            GameObjects.Add(AsteroidFactory.CreateAsteroid(Asteroid.SetRandomPosition(), AsteroidType.fast));
            
            GameObjects.Add(new Shield(new Vector2(426,100)));
            GameObjects.Add(new SpeedUp(new Vector2(852,100)));

            
        }
    }
}
