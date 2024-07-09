using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ShootPowerUp : PowerUp, IPickuppeable
    {

        private Animation idleAnimation;
        ObjectsMovement objectsMovement;

        public int totalShoots { get; private set; }
        public int maxShoots { get; private set; } = 4;
        public string totalShootstxt { get; private set; } = "0";
        public static bool isPicked { get; private set; } = false;
        public bool isProcessed { get; set; }

        public ShootPowerUp(Vector2 position) : base(position)
        {
            transform = new Transform(position, new Vector2(0, 0));
            CreateAnimations();
            objectsMovement = new ObjectsMovement(transform, 5);
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
            isPicked = true;
            PowerUpManager.shootPUCollected.Add(this);
        }

        private void CreateAnimations()
        {
            idleAnimation = Animator.CreateAnimation("IdleBulletPU", "assets/BulletPU/", 1, 40f, true);
            currentAnimation = idleAnimation;
        }
    }
}
