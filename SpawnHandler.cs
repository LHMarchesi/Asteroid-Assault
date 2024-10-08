﻿using MyGame.assets;
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
        private float timeBetweenFastAsteroids = 5f;
        private float timeBetweenBigAsteroids = 15f;
        private float timeBetweenShied = 5f;
        private float timeBetweenSpeedUp = 13f;
        private float timeBetweenShootPU = 5f;

        GenericPool<Asteroid> slowAsteroidPool;
        GenericPool<Asteroid> fastAsteroidPool;
        GenericPool<Asteroid> BigAsteroidPool;
        GenericPool<PowerUp> shieldPool;
        GenericPool<PowerUp> shootPowerUpPool;
        GenericPool<PowerUp> speedUpPool;


        private DateTime timeLastSlowAsteroidSpawn;
        private DateTime timeLastFastAsteroidSpawn;
        private DateTime timeLastBigAsteroidSpawn;
        private DateTime timeLastShieldSpawn;
        private DateTime timeLastSpeedUpSpawn;
        private DateTime timeLastShootPUSpawn;

        public SpawnHandler()  // Pools 
        {
            slowAsteroidPool = new GenericPool<Asteroid>(8, () => AsteroidFactory.CreateAsteroid(ObjectsMovement.SetRandomPosition(), AsteroidType.slow));
            fastAsteroidPool = new GenericPool<Asteroid>(15, () => AsteroidFactory.CreateAsteroid(ObjectsMovement.SetRandomPosition(), AsteroidType.fast));
            BigAsteroidPool = new GenericPool<Asteroid>(6, () => AsteroidFactory.CreateAsteroid(ObjectsMovement.SetRandomPosition(), AsteroidType.big));

            shieldPool = new GenericPool<PowerUp>(4, () => PowerUpFactory.CreatePowerUp(ObjectsMovement.SetRandomPosition(), powerUpType.shield));
            shootPowerUpPool = new GenericPool<PowerUp>(2, () => PowerUpFactory.CreatePowerUp(ObjectsMovement.SetRandomPosition(), powerUpType.shoot));
            speedUpPool = new GenericPool<PowerUp>(2, () => PowerUpFactory.CreatePowerUp(ObjectsMovement.SetRandomPosition(), powerUpType.speedUp));
        }

        public void Spawner()// Spawn de  enemigos y power Ups, utilizando un timer
        {
            if (Time.timeElapse >= 2)
            {
                SlowAsteroidSpawn(true);
            }
            if (Time.timeElapse >= 3)
            {
                FastAsteroidSpawn(true);
                SpeedUpSpawn(true);
            }
            if (Time.timeElapse >= 5)
            {
                BigAsteroidSpawn(true);

            }
            if (Time.timeElapse >= 7)
            {
                ShieldSpawn(true);
            }
            if (Time.timeElapse >= 10)
            {
                ShootPowerUPSpawn(true);
            }
        }

        private void SlowAsteroidSpawn(bool canSlowAsteroidSpawn)
        {
            DateTime currentTime = DateTime.Now;

            if (canSlowAsteroidSpawn && (currentTime - timeLastSlowAsteroidSpawn).TotalSeconds >= timeBetweenSlowAsteroids)  // Uso del timer
            {
                Asteroid asteroid = slowAsteroidPool.GetObject();
                if (asteroid != null)
                {
                    asteroid.Transform.SetPosition(ObjectsMovement.SetRandomPosition());
                    GameManager.Instance.LevelManager.GameObjects.Add(asteroid);
                    timeLastSlowAsteroidSpawn = currentTime;
                }
            }
        }
        private void FastAsteroidSpawn(bool canFastAsteroidSpawn)
        {
            DateTime currentTime = DateTime.Now;

            if (canFastAsteroidSpawn && (currentTime - timeLastFastAsteroidSpawn).TotalSeconds >= timeBetweenFastAsteroids)
            {
                Asteroid asteroid = fastAsteroidPool.GetObject();
                if (asteroid != null)
                {
                    asteroid.Transform.SetPosition(ObjectsMovement.SetRandomPosition());
                    GameManager.Instance.LevelManager.GameObjects.Add(asteroid);
                    timeLastFastAsteroidSpawn = currentTime;
                }
            }
        }
        private void BigAsteroidSpawn(bool canBigAsteroidSpawn)
        {
            DateTime currentTime = DateTime.Now;

            if (canBigAsteroidSpawn && (currentTime - timeLastBigAsteroidSpawn).TotalSeconds >= timeBetweenBigAsteroids)
            {
                Asteroid asteroid = BigAsteroidPool.GetObject();
                if (asteroid != null)
                {
                    asteroid.Transform.SetPosition(ObjectsMovement.SetRandomPosition());
                    GameManager.Instance.LevelManager.GameObjects.Add(asteroid);
                    timeLastBigAsteroidSpawn = currentTime;
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
                    GameManager.Instance.LevelManager.GameObjects.Add(speedUp);
                    timeLastSpeedUpSpawn = currentTime;
                }
            }
        }

    }
}
