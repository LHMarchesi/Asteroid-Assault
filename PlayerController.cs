using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PlayerController
    {
        private Transform transform;
        private int speed = 10;
        public PlayerController(Transform transform)
        {
            this.transform = transform;
        }

        public void GetInputs()  //Character Movement
        {

            if (Engine.KeyPress(Engine.KEY_A))
            {
                transform.Translate(new Vector2(-1, 0), speed);
                GameManager.Instance.LevelManager.Player.LeftAnimation();
            }


            if (Engine.KeyPress(Engine.KEY_D))
            {
                transform.Translate(new Vector2(1, 0), speed);
                GameManager.Instance.LevelManager.Player.RightAnimation();
            }

            if (Engine.KeyPress(Engine.KEY_W))
            {
                transform.Translate(new Vector2(0, -1), speed);
                GameManager.Instance.LevelManager.Player.IdleAnimation();
            }

            if (Engine.KeyPress(Engine.KEY_S))
            {
                transform.Translate(new Vector2(0, 1), speed / 2);
                GameManager.Instance.LevelManager.Player.IdleAnimation();
            }
        }

        public void ChangeSpeed(int speed)
        {
            this.speed += speed;
        }
    }
}