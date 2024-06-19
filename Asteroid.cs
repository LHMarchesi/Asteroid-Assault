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
        public static bool IsFast;
        private ObjectsMovement objectsMovement;


        public Asteroid(Vector2 position, int speed) : base(position) // Construcrtor
        {
            CreateAnimations();
            transform = new Transform(position, new Vector2(75, 75));
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
            if (IsFast == false)
            { 
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 4; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/Asteroid/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 20f, true);
                currentAnimation = idleAnimation;
            }
            else
            {
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 4; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/FastAsteroid/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 10f, true);
                currentAnimation = idleAnimation;
            }
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
