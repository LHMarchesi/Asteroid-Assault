using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ShootPowerUp : PowerUp, IAcumulabble, IPickuppeable
    {

        private Animation idleAnimation;
        ObjectsMovement objectsMovement;
        public event Action<ShootPowerUp> ResetShoot;

        public static int totalShoots;
        public static string totalShootstxt = "0";
        private int powerUpSpeed = 5;
        private int maxShoots = 4;
        static public bool canShoot = false;

        public ShootPowerUp(Vector2 position) : base(position)
        {
            transform = new Transform(position, new Vector2(0, 0));
            CreateAnimations();
            objectsMovement = new ObjectsMovement(transform, powerUpSpeed);
            ResetShoot += ResetValues;
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
            canShoot = true;
            Acumulable();
        }

        public void Reset()
        {
            ResetShoot?.Invoke(this);
            canShoot = false;
        }

        private void ResetValues(ShootPowerUp shootPowerUp)
        {
            totalShoots = 0;
            totalShootstxt = totalShoots.ToString();
        }

        public void Acumulable()
        {
            if (canShoot)
            {
                if (totalShoots < maxShoots)
                {
                    totalShoots++;
                    totalShootstxt = totalShoots.ToString();
                }
                if (totalShoots == maxShoots)
                {
                    totalShootstxt = maxShoots + " (Max)";
                }
            }
        }

        public void restarAcumulable()
        {
            totalShoots--;
            totalShootstxt = totalShoots.ToString();

            if (totalShoots <= 0)
            {
                canShoot = false;
            }
            else
            {
                canShoot = true;
            }
        }

        private void CreateAnimations()
        {
            idleAnimation = Animator.CreateAnimation("IdleBulletPU", "assets/BulletPU/", 1, 40f, true);
            currentAnimation = idleAnimation;
        }
    }
}
