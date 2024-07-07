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
            idleAnimation = Animator.CreateAnimation("SlowAsteroidIdle", "assets/Asteroid/", 3, 20f, true);
            currentAnimation = idleAnimation;
        }
    }
}
