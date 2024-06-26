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

        private void CreateAnimations()
        {
            if (Player.ship1)
            {
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 2; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/Bullet/Bullets/1/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 10f, true);
                currentAnimation = idleAnimation;

            }
            else if (Player.ship2)
            {
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 2; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/Bullet/Bullets/2/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 10f, true);
                currentAnimation = idleAnimation;
            }
            else if (Player.ship3)
            {
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 2; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/Bullet/Bullets/3/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 10f, true);
                currentAnimation = idleAnimation;
            }
        }

        private void RemoveBullet(IPoolable bullet)
        {
            GameManager.Instance.LevelManager.GameObjects.Remove(this);
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

        private void HandleCollision(GameObject gameObject)
        {
            if (gameObject is Asteroid)
            {
                GameManager.Instance.LevelManager.GameObjects.Remove(gameObject);
                GameManager.Instance.LevelManager.GameObjects.Remove(this);
            }
        }

    }
}
