using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class DifficultManager
    {
        private DateTime timeLastSpawn;
        private DateTime timeLastShieldSpawn;
        private DateTime timeLastSpeedUpSpawn;
        private DateTime timeLastShootPUSpawn;

        private float timeBetweenSlowAsteroids = 3f;
        private float timeBetweenShied = 15f;
        private float timeBetweenSpeedUp = 8f;
        private float timeBetweenShootPU = 2f;


        GenericPool<Asteroid> asteroidPool;
        public DifficultManager()
        {

            asteroidPool = new GenericPool<Asteroid>(10, () => AsteroidFactory.CreateAsteroid(ObjectsMovement.SetRandomPosition(), AsteroidType.slow));

        }
        public void Spawner()// Spawn de  enemigos y power Ups, utilizando un timer
        {
            DateTime currentTimeAsteroid = DateTime.Now;
            DateTime currentTimeShield = DateTime.Now;
            DateTime currentTimeSpeedUp = DateTime.Now;
            DateTime currentTimeShootPU = DateTime.Now;

            if ((currentTimeAsteroid - timeLastSpawn).TotalSeconds >= timeBetweenSlowAsteroids)
            {
                Asteroid asteroid = asteroidPool.GetObject();
                if (asteroid != null)
                {
                    asteroid.Transform.SetPosition(ObjectsMovement.SetRandomPosition());
                    asteroidPool.PrintObjects();
                    GameManager.Instance.LevelManager.GameObjects.Add(asteroid);
                    timeLastSpawn = currentTimeAsteroid;
                }
            }

            if ((currentTimeShield - timeLastShieldSpawn).TotalSeconds >= timeBetweenShied)
            {
                GameManager.Instance.LevelManager.GameObjects.Add(new Shield(ObjectsMovement.SetRandomPosition()));
                timeLastShieldSpawn = currentTimeShield;
            }

            if ((currentTimeShootPU - timeLastShootPUSpawn).TotalSeconds >= timeBetweenShootPU)
            {
                GameManager.Instance.LevelManager.GameObjects.Add(new ShootPowerUp(ObjectsMovement.SetRandomPosition()));
                timeLastShootPUSpawn = currentTimeShootPU;
            }

            if ((currentTimeSpeedUp - timeLastSpeedUpSpawn).TotalSeconds >= timeBetweenSpeedUp)
            {
                GameManager.Instance.LevelManager.GameObjects.Add(new SpeedUp(ObjectsMovement.SetRandomPosition()));
                timeLastSpeedUpSpawn = currentTimeSpeedUp;
            }
        }
    }
}
