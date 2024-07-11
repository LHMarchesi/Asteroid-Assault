using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class BigAsteroid : Asteroid
    {
        private Animation idleAnimation;
        private ObjectsMovement objectsMovement;

        public BigAsteroid(Vector2 position, int speed) : base(position, speed) 
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
            idleAnimation = Animator.CreateAnimation("Idle", "assets/BigAsteroid", 4, 40f, true);
            currentAnimation = idleAnimation;
        }
    }
}
