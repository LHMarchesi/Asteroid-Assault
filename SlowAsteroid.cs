using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class SlowAsteroid : Asteroid
    {
        private Animation idleAnimation;
        ObjectsMovement objectsMovement;

        public SlowAsteroid(Vector2 position, int speed) : base(position, speed)
        {
            transform = new Transform(position, new Vector2(100, 100));
            objectsMovement = new ObjectsMovement(transform, speed);
            CreateAnimations();
        }


        public override void Update()
        {
            base.Update();
            objectsMovement.MoveDown();
            if (transform.Position.y >= 1000)
            {
                transform.SetPosition(ObjectsMovement.SetRandomPosition());
            }
        }

        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 3; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Asteroid/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 20f, true);
            currentAnimation = idleAnimation;
        }
    }
}
