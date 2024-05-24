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
        static public Player player = new Player(new Vector2(565, 520));
        public Transform Transform => transform;

        static private Animation idleAnimation;
        static private Animation leftAnimation;
        static private Animation rightAnimation;
       
        private PlayerLimits playerLimits;
        private PlayerController controller;

        Vector2 minBounds = new Vector2(0, 0);
        Vector2 maxBounds = new Vector2(1280,720);



        public Player(Vector2 position) : base(position)
        {
            controller = new PlayerController(transform);
            IdleAnimation();
        }
       
        public override void Update()
        {
            //playerLimits.Limits(player);
            base.Update();
            controller.GetInputs();

            if (Time.timeElapse > Time.winTime)
            {
                GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.win);
            }
            Vector2 newPosition = GetClampedPosition(minBounds, maxBounds);
            transform.Position = newPosition;
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

        public Vector2 GetClampedPosition(Vector2 minBounds, Vector2 maxBounds)
        {
            Vector2 newPosition = transform.Position;

            // Verificar los límites horizontales
            if (newPosition.x < minBounds.x)
            {
                newPosition.x = minBounds.x;

            }
            else if (newPosition.x > maxBounds.x)
            {
                newPosition.x = maxBounds.x;
            }

            // Verificar los límites verticales
            if (newPosition.y < minBounds.y)
            {
                newPosition.y = minBounds.y;
            }
            else if (newPosition.y > maxBounds.y)
            {
                newPosition.y = maxBounds.y;
            }

            return newPosition;
        }
    }


}