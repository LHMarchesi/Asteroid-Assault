﻿using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class SpeedUp : PowerUp, IPickuppeable 
    {
        public event Action<SpeedUp> ResetSpeedUp;

        private Animation idleAnimation;
        private ObjectsMovement objectsMovement;

        static public bool isPicked = false;

        private int MovementSpeed = 5;
        private int newSpeed = 3;

        public SpeedUp(Vector2 pos) : base(pos)  // Constructor
        {
            CreateAnimations();
            transform = new Transform(pos, new Vector2(0, 0));
            objectsMovement = new ObjectsMovement(transform, MovementSpeed);
        }
        public override void Update()
        {
            base.Update();
            objectsMovement.MoveDown();
            if (transform.Position.y >= 1000)
            {
                GameManager.Instance.LevelManager.GameObjects.Remove(this);
            }
        }

        public void PickUp()
        {
            GameManager.Instance.LevelManager.Player.controller.ChangeSpeed(newSpeed);
            isPicked = true;
        }

        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/SpeedUp/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 20f, true);
            currentAnimation = idleAnimation;
        }

        public void Reset()
        {
            GameManager.Instance.LevelManager.Player.controller.ResetSpeed(); GameManager.Instance.LevelManager.Player.controller.ResetSpeed();
            isPicked = false;
        }
    }
}
