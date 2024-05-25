using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Transform
    {
        private Vector2 position;
        private Vector2 scale;
        public Vector2 Scale => scale;
        public Vector2 Position
        {
            set
            {

            }

            get
            {
                return position;
            }
        }


        public Transform(Vector2 position, Vector2 scale)
        {
            this.position = position;
            this.scale = scale;
        }

        public void Translate(Vector2 direccion, float speed)
        {
            position.x += direccion.x * speed;
            position.y += direccion.y * speed;
        }

        public void SetPosition(Vector2 newPosition)
        {
            position.x = newPosition.x;
            position.y = newPosition.y;
        }
    }
}
