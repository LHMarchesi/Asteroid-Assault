﻿using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Shield : PowerUp, IPickuppeable, IAcumulabble
    {
        private ObjectsMovement objectsMovement;
        private Animation idleAnimation;

        public static bool IsPicked = false;
        private int shieldSpeed = 5;
        private int maxShield = 2;

        public static int totalShield;
        public static string totalShieldtxt = "0";

        public Shield(Vector2 pos) : base(pos)
        {
            transform = new Transform(pos, new Vector2(0, 0));
            CreateAnimations();
            objectsMovement = new ObjectsMovement(transform, shieldSpeed);
           
        }

        public void PickUp()
        {
            IsPicked = true;
            Acumulable();

        }

        public void Acumulable()
        {
            if (IsPicked)
            {
                if (totalShield < maxShield)
                {
                    totalShield++;
                    totalShieldtxt = totalShield.ToString();
                }
                if (totalShield == maxShield)
                {
                    totalShieldtxt = maxShield + " (Max)";
                }
            }
        }

        public void restarAcumulable()
        {
            totalShield--;
            totalShieldtxt = totalShield.ToString();
            if (totalShield == 0)
            {
                IsPicked = false;
            }
            else
            {
                IsPicked = true;
            }

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

        private void CreateAnimations()
        {
            idleAnimation = Animator.CreateAnimation("IdleShield", "assets/Shield/", 3, 20f, true);
            currentAnimation = idleAnimation;
        }

    }
}
