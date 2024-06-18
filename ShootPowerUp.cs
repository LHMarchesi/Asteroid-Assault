using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ShootPowerUp : GameObject, IAcumulabble, IPickuppeable
    {
        private Animation idleAnimation;
        ObjectsMovement objectsMovement;

        private int powerUpSpeed = 5;
        private int maxShoots = 4;
        public static int totalShoots;
        public static bool IsPicked = false;
        public static string totalShootstxt = "0";

        public ShootPowerUp(Vector2 position) : base(position)
        {
            transform = new Transform(position, new Vector2(0, 0));
            CreateAnimations();
            objectsMovement = new ObjectsMovement(transform, powerUpSpeed);
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
            Acumulable();
            Console.WriteLine("A");
        }

        public void Acumulable()
        {
            if (IsPicked)
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
            if (totalShoots == 0)
            {
                IsPicked = false;
            }
            else
            {
                IsPicked = true;
            }
        }

        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 3; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Shield/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.9f, true);
            currentAnimation = idleAnimation;
        }

        
    }
}
