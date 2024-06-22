using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class SpawnHandler
    {

        private float timeBetweenSlowAsteroids = 3f;
        private float timeBetweenShied = 20f;
        private float timeBetweenSpeedUp = 13f;
        private float timeBetweenShootPU = 10f;

        GenericPool<Asteroid> asteroidPool;
        GenericPool<Asteroid> fastAsteroidPool;
        GenericPool<PowerUp> shieldPool;
        GenericPool<PowerUp> shootPowerUpPool;
        GenericPool<PowerUp> speedUpPool;


        private DateTime timeLastAsteroidSpawn;
        private DateTime timeLastShieldSpawn;
        private DateTime timeLastSpeedUpSpawn;
        private DateTime timeLastShootPUSpawn;
        public SpawnHandler()
        {
            asteroidPool = new GenericPool<Asteroid>(10, () => AsteroidFactory.CreateAsteroid(ObjectsMovement.SetRandomPosition(), AsteroidType.slow));
            fastAsteroidPool = new GenericPool<Asteroid>(5, () => AsteroidFactory.CreateAsteroid(ObjectsMovement.SetRandomPosition(), AsteroidType.fast));
            shieldPool = new GenericPool<PowerUp>(3, () => PowerUpFactory.CreatePowerUp(ObjectsMovement.SetRandomPosition(), powerUpType.shield));
            shootPowerUpPool = new GenericPool<PowerUp>(2, () => PowerUpFactory.CreatePowerUp(ObjectsMovement.SetRandomPosition(), powerUpType.shoot));
            speedUpPool = new GenericPool<PowerUp>(2, () => PowerUpFactory.CreatePowerUp(ObjectsMovement.SetRandomPosition(), powerUpType.speedUp));
        }
        public void Spawner()// Spawn de  enemigos y power Ups, utilizando un timer
        {
            AsteroidSpawn(true);
            ShieldSpawn(true);
            ShootPowerUPSpawn(true);
            SpeedUpSpawn(true);
        }

        private void AsteroidSpawn(bool canAsteroidSpawn)
        {
            DateTime currentTime = DateTime.Now;

            if (canAsteroidSpawn && (currentTime - timeLastAsteroidSpawn).TotalSeconds >= timeBetweenSlowAsteroids)  // Uso del timer
            {
                Asteroid asteroid = asteroidPool.GetObject();
                if (asteroid != null)
                {
                    asteroid.Transform.SetPosition(ObjectsMovement.SetRandomPosition());
                    // asteroidPool.PrintObjects();
                    GameManager.Instance.LevelManager.GameObjects.Add(asteroid);  // deberiamos utilizar factori ?
                    timeLastAsteroidSpawn = currentTime;
                }
            }
        }

        private void ShieldSpawn(bool canShieldSpawn)
        {
            DateTime currentTime = DateTime.Now;

            if (canShieldSpawn && (currentTime - timeLastShieldSpawn).TotalSeconds >= timeBetweenShied)
            {
                PowerUp shield = shieldPool.GetObject();
                if (shield != null)
                {
                    shield.Transform.SetPosition(ObjectsMovement.SetRandomPosition());
                    // shieldPool.PrintObjects();
                    GameManager.Instance.LevelManager.GameObjects.Add(shield);
                    timeLastShieldSpawn = currentTime;
                }
            }
        }

        private void ShootPowerUPSpawn(bool canShootPowerUpSpawn)
        {
            DateTime currentTime = DateTime.Now;

            if (canShootPowerUpSpawn && (currentTime - timeLastShootPUSpawn).TotalSeconds >= timeBetweenShootPU)
            {
                PowerUp shootPowerUp = shootPowerUpPool.GetObject();
                if (shootPowerUp != null)
                {
                    shootPowerUp.Transform.SetPosition(ObjectsMovement.SetRandomPosition());
                    // shootPowerUpPool.PrintObjects();
                    GameManager.Instance.LevelManager.GameObjects.Add(shootPowerUp);
                    timeLastShootPUSpawn = currentTime;
                }
            }
        }

        private void SpeedUpSpawn(bool canSpeedUpSpawn)
        {
            DateTime currentTime = DateTime.Now;

            if (canSpeedUpSpawn && (currentTime - timeLastSpeedUpSpawn).TotalSeconds >= timeBetweenSpeedUp)
            {
                PowerUp speedUp = speedUpPool.GetObject();
                if (speedUp != null)
                {
                    speedUp.Transform.SetPosition(ObjectsMovement.SetRandomPosition());
                    // speedUpPool.PrintObjects();
                    GameManager.Instance.LevelManager.GameObjects.Add(speedUp);
                    timeLastSpeedUpSpawn = currentTime;
                }
            }
        }

    }
}
