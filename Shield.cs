using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Shield : PowerUp, IPickuppeable
    {
        private ObjectsMovement objectsMovement;
        private Animation idleAnimation;


        public int totalShield { get; private set; } = 0;
        public int maxShield { get; private set; } = 2;
        public static bool IsPicked { get; private set; }

        private int shieldSpeed = 5;

        public bool IsProcessed { get; set; }

        public static IntPtr shieldImage;

        public Shield(Vector2 pos) : base(pos)
        {
            transform = new Transform(pos, new Vector2(0, 0));
            CreateAnimations();
            objectsMovement = new ObjectsMovement(transform, shieldSpeed);
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
            IsPicked = true;
            PowerUpManager.shieldCollected.Add(this);
        }

        private void CreateAnimations()
        {
            idleAnimation = Animator.CreateAnimation("IdleShield", "assets/Shield/", 3, 20f, true);
            currentAnimation = idleAnimation;
        }
    }
}
