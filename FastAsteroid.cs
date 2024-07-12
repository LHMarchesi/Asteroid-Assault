using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class FastAsteroid : Asteroid
    {
        private Animation idleAnimation;
        private ObjectsMovement objectsMovement;

        public FastAsteroid(Vector2 position, int speed) : base(position, speed) 
        {
            CreateAnimations();
            transform = new Transform(position, new Vector2(75, 75));
            objectsMovement = new ObjectsMovement(transform, speed);
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
            idleAnimation = Animator.CreateAnimation("FastAsteroidIdle", "assets/FastAsteroid/", 4, 15f, true);
            currentAnimation = idleAnimation;
        }
    }
}
