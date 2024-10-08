﻿using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet : GameObject, IPoolable
    {
        ObjectsMovement objectsMovement;
        private int bulletSpeed = 10;
        private Animation idleAnimation;
        public event Action<IPoolable> OnDestroy;

        public Bullet(Vector2 pos) : base(pos)
        {
            transform = new Transform(pos, new Vector2(5, 5));
            objectsMovement = new ObjectsMovement(transform, bulletSpeed);
            OnDestroy += RemoveBullet;
            CreateAnimations();
        }

        public override void Update()
        {
            currentAnimation.Update();
            objectsMovement.MoveUp();
            CheckCollitions();
        }

        private void CheckCollitions()  // Chequea colisiones  
        {
            for (int i = 0; i < GameManager.Instance.LevelManager.GameObjects.Count; i++)
            {
                GameObject gameObject = GameManager.Instance.LevelManager.GameObjects[i];
                float distanceX = Math.Abs((gameObject.Transform.Position.x + (gameObject.Transform.Scale.x / 2)) - (Transform.Position.x + (Transform.Scale.x / 2)));
                float distanceY = Math.Abs((gameObject.Transform.Position.y + (gameObject.Transform.Scale.y / 2)) - (Transform.Position.y + (Transform.Scale.y / 2)));

                float sumHalfWidth = gameObject.Transform.Scale.x / 2 + Transform.Scale.x / 2;
                float sumHalfH = gameObject.Transform.Scale.y / 2 + Transform.Scale.y / 2;

                if (distanceX < sumHalfWidth && distanceY < sumHalfH) // Hay colisión
                {
                    if (gameObject is Asteroid)
                    {
                        GameManager.Instance.LevelManager.GameObjects.Remove(gameObject);
                        OnDestroy.Invoke(this);
                    }
                }
            }
        }

        private void CreateAnimations() // Cambio de sprite, dependiendo la nave
        {
            if (Player.shipBlue)
            {
                    idleAnimation = Animator.CreateAnimation("Ship1BulletIdle", "assets/Bullet/Bullets/1/", 2, 10f, true);
                    currentAnimation = idleAnimation;
            }
            else if (Player.shipRed)
            {
                idleAnimation = Animator.CreateAnimation("Ship2BulletIdle", "assets/Bullet/Bullets/2/", 2, 10f, true);
                currentAnimation = idleAnimation;
            }
            else if (Player.shipGreen)
            {
                idleAnimation = Animator.CreateAnimation("Ship3BulletIdle", "assets/Bullet/Bullets/3/", 2, 10f, true);
                currentAnimation = idleAnimation;
            }
        }

        private void RemoveBullet(IPoolable bullet)
        {
            GameManager.Instance.LevelManager.GameObjects.Remove(this);
        }
    }
}
