

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ObjectsMovement
    {
        private Transform transform;
        private int speed;
        public ObjectsMovement(Transform transform, int speed)
        {
            this.transform = transform;
            this.speed = speed;
        }

        public void MoveDown()  // Movimiento hacia abajo
        {
            transform.Translate(new Vector2(0,1), speed);
        }

        public void MoveUp()  // Movimiento hacia Arriba
        {
            transform.Translate(new Vector2(0,-1), speed);
        }

        public static Vector2 SetRandomPosition() // Toma dos valores aletoreos, por encima de la pantalla, y devuelve un Vector2 aleatoreo
        {
            
            RandomNumber random = new RandomNumber();
            int randomX = random.Rand(-50, 1200);
            int randomY = random.Rand(-1000, -200);
            Vector2 randomPos = new Vector2(randomX, randomY);
            return randomPos;
        }
    }
}
