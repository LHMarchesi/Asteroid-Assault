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
        public static PowerUp CreatePowerUp(Vector2 position, powerUpType type)
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
