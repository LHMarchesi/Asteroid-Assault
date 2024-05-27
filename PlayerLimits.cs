using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PlayerLimits
    {
        private Player player;
        private float maxX = 1280;
        private float maxY = 720;

        public PlayerLimits(Player player)
        {
            this.player = player;
        }

        public void CheckLimits() // Limites de pantalla
        {
            Vector2 position = player.Transform.Position;

            if (position.x < 0)
            {
                position.x = 0;
            }
            else if (position.x > maxX - player.Transform.Scale.x * 1.2f)
            {
                position.x = maxX - player.Transform.Scale.x * 1.2f;
            }

            if (position.y < 0)
            {
                position.y = 0;
            }
            else if (position.y > maxY - player.Transform.Scale.y * 1.5f)
            {
                position.y = maxY - player.Transform.Scale.y * 1.5f;
            }
            player.Transform.SetPosition(position);
        }
    }
}

