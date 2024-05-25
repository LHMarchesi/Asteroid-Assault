using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Asteroid : GameObject
    {

        public Transform Transform => transform;
        private Animation idleAnimation;
        private AsteroidMovement asteroidMovement;
     
        public Asteroid(Vector2 position, int speed) : base(position)
        {
            CreateAnimations();
            transform = new Transform(position, new Vector2(100, 100));
            asteroidMovement = new AsteroidMovement(transform, speed);

        }


        public override void Update()
        {
            base.Update();
            CheckPlayerCollision();
            currentAnimation.Update();
            asteroidMovement.MoveEnemy();
        }
        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Meteoro/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
            currentAnimation = idleAnimation;
        }
        private void CheckPlayerCollision() //Colision con jugador
        {
            // Obtenemos las posiciones y dimensiones del jugador y el enemigo
            float distanceX = Math.Abs((GameManager.Instance.LevelManager.Player.Transform.Position.x + (GameManager.Instance.LevelManager.Player.Transform.Scale.x / 2)) - (transform.Position.x + (transform.Scale.x / 2)));
            float distanceY = Math.Abs((GameManager.Instance.LevelManager.Player.Transform.Position.y + (GameManager.Instance.LevelManager.Player.Transform.Scale.y / 2)) - (transform.Position.y + (transform.Scale.y / 2)));

            float sumHalfWidth = GameManager.Instance.LevelManager.Player.Transform.Scale.x / 2 + transform.Scale.x / 2;
            float sumHalfH = GameManager.Instance.LevelManager.Player.Transform.Scale.y / 2 + transform.Scale.y / 2;

            // Verificamos la colisión
            if (distanceX < sumHalfWidth && distanceY < sumHalfH)
            {
                GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.lose);

            }

        }

        public static Vector2 SetRandomPosition()
        {
            RandomNumber random = new RandomNumber();
            int randomX = random.Rand(0, 1024);
            int randomY = random.Rand(-500, 0);
            Vector2 randomPos = new Vector2(randomX, randomY);
            return randomPos;
        }

    }


}
