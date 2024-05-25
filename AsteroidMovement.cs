using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class AsteroidMovement
    {
        private Transform transform;
        private int speed;
        private Vector2 direccion = new Vector2(0,1);
        public AsteroidMovement(Transform transform, int speed)
        {
            this.transform = transform;
            this.speed = speed;
        }
        public void MoveEnemy()
        {
            if (transform.Position.y >= 1000)
            {
                transform.SetPosition(Asteroid.SetRandomPosition());
            }
            transform.Translate(direccion, speed);
        }
    }
}
