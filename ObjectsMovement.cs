

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ObjectsMovement
    {
        private Transform transform;
        private int speed;
        private Vector2 direccion = new Vector2(0,1);

        public ObjectsMovement(Transform transform, int speed)
        {
            this.transform = transform;
            this.speed = speed;
        }

        public void Move()  // Movimiento hacia abajo
        {
            transform.Translate(direccion, speed);
           
        }

        public static Vector2 SetRandomPosition() // Toma dos valores aletoreos, por encima de la pantalla, y devuelve un Vector2 aleatoreo
        {
            RandomNumber random = new RandomNumber();
            int randomX = random.Rand(-100, 1150);
            int randomY = random.Rand(-1000, -200);
            Vector2 randomPos = new Vector2(randomX, randomY);
            return randomPos;
        }
    }
}
