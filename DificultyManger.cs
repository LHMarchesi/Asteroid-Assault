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

        private float timeBetweenSlowAsteroids = 7f;
        private float timeBetweenShied = 15f;
        private float timeBetweenSpeedUp = 8f;
        public void Spawner()// Spawn de  enemigos y power Ups, utilizando un timer
        {
            DateTime currentTimeAsteroid = DateTime.Now;
            DateTime currentTimeShield = DateTime.Now;
            DateTime currentTimeSpeedUp = DateTime.Now;

            if ((currentTimeAsteroid - timeLastSpawn).TotalSeconds >= timeBetweenSlowAsteroids)
            {
                GameManager.Instance.LevelManager.GameObjects.Add(AsteroidFactory.CreateAsteroid(Asteroid.SetRandomPosition(), AsteroidType.slow));
                GameManager.Instance.LevelManager.GameObjects.Add(AsteroidFactory.CreateAsteroid(Asteroid.SetRandomPosition(), AsteroidType.fast));
                timeLastSpawn = currentTimeAsteroid;
            }

            if ((currentTimeShield - timeLastShieldSpawn).TotalSeconds >= timeBetweenShied)
            {
                GameManager.Instance.LevelManager.GameObjects.Add(new Shield(Asteroid.SetRandomPosition()));
                timeLastShieldSpawn = currentTimeShield;
            }

            if ((currentTimeSpeedUp - timeLastSpeedUpSpawn).TotalSeconds >= timeBetweenSpeedUp)
            {
                GameManager.Instance.LevelManager.GameObjects.Add(new SpeedUp(Asteroid.SetRandomPosition()));
                timeLastSpeedUpSpawn = currentTimeSpeedUp;
            }
        }
    }
}
