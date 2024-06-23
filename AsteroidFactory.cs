using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public enum AsteroidType
    {
        slow, fast, big
    }
    public class AsteroidFactory
    {
        public static Asteroid CreateAsteroid(Vector2 position, AsteroidType type)  // Factory de Asteroides 
        {

            switch (type)
            {
                case AsteroidType.slow:
                    
                    return new SlowAsteroid(position, 10);

                case AsteroidType.fast:
                    
                    return new FastAsteroid(position, 15);
                
                case AsteroidType.big:
                    return new BigAsteroid(position, 7);
            }
            return null;
        }
    }
}
