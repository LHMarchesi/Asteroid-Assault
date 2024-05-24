using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class LevelManager
    {

        DifficultyManager difficultyManager = new DifficultyManager();
        public List<Asteroid> enemyList = new List<Asteroid>();
        public Asteroid enemy = new Asteroid(new Vector2(0, 0), Player.player);
        private Player player;

        RandomNumber random = new RandomNumber();
        private float enemyDefaultSpeed = 5;

        public LevelManager(Player _player)
        {
            player = _player;
            Vector2 initialEnemyPosition = new Vector2(random.Rand(0, 1024), random.Rand(-500, 0));
            Asteroid initialEnemy = new Asteroid(initialEnemyPosition, player);
            enemyList.Add(initialEnemy);
        }
        public void Update()
        {

            EnemySpawner();
        }

        public void EnemySpawner()
        {

            int randomX = random.Rand(0, 1024);
            int randomY = random.Rand(-500, 0);

            foreach (Asteroid enemy in enemyList)
            {
                enemy.Update();
                enemy.Render();
                enemy.Transform.Translate(new Vector2(0, 1), enemyDefaultSpeed);

                if (enemy.Transform.Position.y >= 1000)
                {
                    enemy.Transform.Position = new Vector2(player.Transform.Position.x, randomY); // vuelven a  aparecer arriba
                }
            }
        } // Spawnea enemigos en posicion aleatoria 
    }
}
