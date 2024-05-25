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
        public event Action<Player> Ondie;
       

        static private Animation idleAnimation;
        static private Animation leftAnimation;
        static private Animation rightAnimation;

        private Vector2 originalPosition;
        private PlayerController controller;
        public Player(Vector2 position) : base(position)
        {
            originalPosition = position;
            controller = new PlayerController(transform);
            Ondie += ResetPosition;
            IdleAnimation();
        }

        public override void Update()
        {
            
            base.Update();
            controller.GetInputs();
            CheckCollisions();

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

        public void ResetPosition(Player player)
        {
            transform.SetPosition(originalPosition);
        }

        private void CheckCollisions()
        {

            for (int i = 0; i < GameManager.Instance.LevelManager.GameObjects.Count; i++)
            {
                GameObject gameObject = GameManager.Instance.LevelManager.GameObjects[i];
                float distanceX = Math.Abs((gameObject.Transform.Position.x + (gameObject.Transform.Scale.x / 2)) - (transform.Position.x + (transform.Scale.x / 2)));
                float distanceY = Math.Abs((gameObject.Transform.Position.y + (gameObject.Transform.Scale.y / 2)) - (transform.Position.y + (transform.Scale.y / 2)));

                float sumHalfWidth = gameObject.Transform.Scale.x / 2 + transform.Scale.x / 2;
                float sumHalfH = gameObject.Transform.Scale.y / 2 + transform.Scale.y / 2;

                if (distanceX < sumHalfWidth && distanceY < sumHalfH) // hay colision
                {
                    if (gameObject is Asteroid)
                    {
                        Ondie.Invoke(this);
                        GameManager.Instance.LevelManager.GameObjects.Remove(gameObject);
                        GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.lose);
                    }

                    //if (gameObject is IPickuppeable pickupobj)
                    //{
                    //    pickupobj.PickUp();
                    //    GameManager.Instance.LevelController.GameObjects.Remove(gameObject);
                    //}

                }

            }
        }


    }
}