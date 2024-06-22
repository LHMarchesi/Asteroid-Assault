using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public enum powerUpType
    {
        speedUp, shoot, shield
    }
    public class PowerUpFactory
    {
        public static PowerUp CreatePowerUP(Vector2 position, powerUpType type)  // PowerUp deberia ser un Objeto Padre, y los powerUps heredar de este
        {
            switch (type)
            {
                case powerUpType.speedUp:
                    return new SpeedUp(position);

                case powerUpType.shoot:
                    return new ShootPowerUp(position);

                case powerUpType.shield:
                    return new Shield(position);
            }
            return null;
        }


    }
}
