﻿using System;
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
        private int speedBackwards = 8;

        public PlayerController(Transform transform)
        {
            this.transform = transform;
        }

        public void GetInputs()  //Movimiento de jugador
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
                transform.Translate(new Vector2(0, 1), speedBackwards);
                GameManager.Instance.LevelManager.Player.IdleAnimation();
            }

            if (Engine.KeyPress(Engine.KEY_ESP))
            {
                GameManager.Instance.LevelManager.Player.Shoot();
            }
        }

        public void ChangeSpeed(int speed)
        {
            this.speed += speed;
            speedBackwards += speed;
            LevelManager.backgroundSpeed += 0.1f;
        }

        public void ResetSpeed()
        {
            this.speed = 10;
            this.speedBackwards = 8;
            LevelManager.backgroundSpeed = 1f;
        }

    }
}