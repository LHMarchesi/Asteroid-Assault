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
        //int bulletSpeed;
        private Animation idleAnimation;
        public event Action<IPoolable> OnDestroy;


        private List<Bullet> bulletsInUse = new List<Bullet>();
        private List<Bullet> bulletsAvailable = new List<Bullet>();
        public Bullet(Vector2 pos) : base(pos)
        {
            transform = new Transform(pos, new Vector2(5, 5));
            //objectsMovement = new ObjectsMovement(transform, bulletSpeed);
            CreateAnimations();
        }

        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 2; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Bullet/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 10f, true);
            currentAnimation = idleAnimation;
        }

        private void DestroyBullet()
        {
            GameManager.Instance.LevelManager.GameObjects.Remove(this);
            //OnDestroy?.Invoke();
        }

        public override void Update()
        {
            currentAnimation.Update();
            objectsMovement.MoveUp();
        }
    }
}
