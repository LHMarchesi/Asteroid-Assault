using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Asteroid : GameObject
    {
        public event Action<Asteroid> Destroy;
        private Animation idleAnimation;
        private ObjectsMovement objectsMovement;


        public Asteroid(Vector2 position, int speed) : base(position) // Construcrtor
        {
            CreateAnimations();
            transform = new Transform(position, new Vector2(100, 100));
            objectsMovement = new ObjectsMovement(transform, speed);
            Destroy += RemoveAsteroid;
        }


        public override void Update()
        {
            base.Update();
            currentAnimation.Update();
            objectsMovement.MoveDown();

            if (transform.Position.y >= 1000)
            {
                transform.SetPosition(ObjectsMovement.SetRandomPosition());
            }
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

        public void OnDestroy()
        {
            Destroy.Invoke(this);
        }

        private void RemoveAsteroid(Asteroid asteroid)
        {
          
            GameManager.Instance.LevelManager.GameObjects.Remove(asteroid);
        }


    }


}
