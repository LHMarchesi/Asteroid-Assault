using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class DifficultManager
    {
        private float speed;
        public void chageSpeed(float speed) // Cambia velocidad del enemigo
        {
            this.speed = speed;
        }

        public void ChangeDifficulty(float var)
        {

            if (Time.timeElapse > 5)
            {
                chageSpeed(var + 5);
                Debug.WriteLine(var);
            }
            if (Time.timeElapse > 20)
            {
                chageSpeed(var + 5);
                Debug.WriteLine(var);
            }
            if (Time.timeElapse > 25)
            {
                chageSpeed(var + 5);
                Debug.WriteLine(var);

            }
            else
            {
                chageSpeed(var);
            }
        }
    }
}
