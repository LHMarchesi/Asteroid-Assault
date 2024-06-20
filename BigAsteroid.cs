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

        public BigAsteroid(Vector2 position, int speed) : base(position, speed) // Construcrtor
        {
            CreateAnimations();
            transform = new Transform(position, new Vector2(75, 75));
            objectsMovement = new ObjectsMovement(transform, speed);
            //Destroy += RemoveAsteroid;
        }

        private void CreateAnimations()
        {

            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/BigAsteroid/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 20f, true);
            currentAnimation = idleAnimation;

        }

        private void RemoveAsteroid(Asteroid asteroid)
        {
            GameManager.Instance.LevelManager.GameObjects.Remove(asteroid);
        }
    }
}
