using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public enum AsteroidType
    {
        slow, fast
    }
    public class AsteroidFactory
    {
        public static Asteroid CreateAsteroid(Vector2 position, AsteroidType type)  // Factory de enemigos con diferente velocidad
        {

            switch (type)
            {
                case AsteroidType.slow:
                    Asteroid.IsFast = false;
                    return new Asteroid(position, 10);

                case AsteroidType.fast:
                    Asteroid.IsFast = true;
                    return new Asteroid(position, 15);
            }
            return null;
        }
    }
}
