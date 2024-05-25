using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class SpeedUp : GameObject, IPickuppeable
    {
        private Animation idleAnimation;
        private int newSpeed = 15;
        public SpeedUp(Vector2 pos) : base(pos)
        {
            
            CreateAnimations();
            transform = new Transform(pos, new Vector2(100,100));
        }
        public void PickUp()
        {
            GameManager.Instance.LevelManager.Player.controller.ChangeSpeed(newSpeed);
            Console.WriteLine("SpeedUp");
        }

        public override void Update()
        {
            base.Update();
        }

        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/SpeedUp/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.9f, true);
            currentAnimation = idleAnimation;
        }
    }
}
