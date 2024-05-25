using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player : GameObject
    {
        public Transform Transform => transform;

        static private Animation idleAnimation;
        static private Animation leftAnimation;
        static private Animation rightAnimation;

        private Vector2 originalPosition;
        private PlayerController controller;
        public Player(Vector2 position) : base(position)
        {
            originalPosition = position;
            controller = new PlayerController(transform);
            IdleAnimation();
        }

        public override void Update()
        {
            
            base.Update();
            controller.GetInputs();

            if (Time.timeElapse > Time.winTime)
            {
                GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.win);
            }
        }

        // Animations
        public void IdleAnimation()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Ship/Idle/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.2f, true);
            currentAnimation = idleAnimation;
        }
        public void LeftAnimation()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 3; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Ship/Left/{i}.png");
                idleTextures.Add(frame);
            }
            leftAnimation = new Animation("Left", idleTextures, 0.2f, true);
            currentAnimation = leftAnimation;
        }
        public void RightAnimation()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 3; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Ship/Right/{i}.png");
                idleTextures.Add(frame);
            }
            rightAnimation = new Animation("Right", idleTextures, 0.2f, true);
            currentAnimation = rightAnimation;
        }

        
    }
}