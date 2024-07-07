using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet : GameObject, IPoolable
    {
        ObjectsMovement objectsMovement;
        private int bulletSpeed = 10;
        private Animation idleAnimation;
        public event Action<IPoolable> OnDestroy;

        public Bullet(Vector2 pos) : base(pos)
        {
            transform = new Transform(pos, new Vector2(5, 5));
            objectsMovement = new ObjectsMovement(transform, bulletSpeed);
            OnDestroy += RemoveBullet;
            CreateAnimations();
        }

        public override void Update()
        {
            currentAnimation.Update();
            objectsMovement.MoveUp();
            CheckCollitions();
        }

        private void CheckCollitions()
        {
            for (int i = 0; i < GameManager.Instance.LevelManager.GameObjects.Count; i++)
            {
                GameObject gameObject = GameManager.Instance.LevelManager.GameObjects[i];
                float distanceX = Math.Abs((gameObject.Transform.Position.x + (gameObject.Transform.Scale.x / 2)) - (Transform.Position.x + (Transform.Scale.x / 2)));
                float distanceY = Math.Abs((gameObject.Transform.Position.y + (gameObject.Transform.Scale.y / 2)) - (Transform.Position.y + (Transform.Scale.y / 2)));

                float sumHalfWidth = gameObject.Transform.Scale.x / 2 + Transform.Scale.x / 2;
                float sumHalfH = gameObject.Transform.Scale.y / 2 + Transform.Scale.y / 2;

                if (distanceX < sumHalfWidth && distanceY < sumHalfH) // Hay colisión
                {
                    HandleCollision(gameObject);
                }
            }
        }

        private void CreateAnimations()
        {
            if (Player.ship1)
            {
                    idleAnimation = Animator.CreateAnimation("Ship1BulletIdle", "assets/Bullet/Bullets/1/", 2, 10f, true);
                    currentAnimation = idleAnimation;
            }
            else if (Player.ship2)
            {
                idleAnimation = Animator.CreateAnimation("Ship2BulletIdle", "assets/Bullet/Bullets/2/", 2, 10f, true);
                currentAnimation = idleAnimation;
            }
            else if (Player.ship3)
            {
                idleAnimation = Animator.CreateAnimation("Ship3BulletIdle", "assets/Bullet/Bullets/3/", 2, 10f, true);
                currentAnimation = idleAnimation;
            }
        }

        private void HandleCollision(GameObject gameObject)
        {
            if (gameObject is Asteroid)
            {
                GameManager.Instance.LevelManager.GameObjects.Remove(gameObject);
                GameManager.Instance.LevelManager.GameObjects.Remove(this);
            }
        }

        private void RemoveBullet(IPoolable bullet)
        {
            GameManager.Instance.LevelManager.GameObjects.Remove(this);
        }
    }
}
