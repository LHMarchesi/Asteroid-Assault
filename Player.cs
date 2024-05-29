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

        public PlayerController controller;
        private PlayerLimits playerLimits;
        static private Animation idleAnimation;

        private Vector2 originalPosition;
        public bool candie =  true;
       
        public Player(Vector2 position) : base(position)
        {
            playerLimits = new PlayerLimits(this);
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
            playerLimits.CheckLimits();

            if (Time.timeElapse > Time.winTime)
            {
                Shield.shieldPicked = false;
                SpeedUp.isPicked = false;
                GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.win);
            }
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

                if (distanceX < sumHalfWidth && distanceY < sumHalfH) // Hay colision
                {
                    if (gameObject is Asteroid && !Shield.shieldPicked)
                    {
                        Asteroid asteroid = (Asteroid)gameObject; // Convertir el GameObject a Asteroid
                        asteroid.OnDestroy();
                        Ondie.Invoke(this);

                        GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.lose);
                        SpeedUp.isPicked = false;
                    }
                    else if (gameObject is Asteroid && Shield.shieldPicked)
                    {
                        GameManager.Instance.LevelManager.GameObjects.Remove(gameObject);
                        candie = true;
                        Shield.shieldPicked = false;
                    }

                    if (gameObject is IPickuppeable pickupobj)
                    {                        
                        pickupobj.PickUp();
                        GameManager.Instance.LevelManager.GameObjects.Remove(gameObject);
                    }
                }
            }
        }
        public void ResetPosition(Player player)
        {
            transform.SetPosition(originalPosition);
        }

        // Animations
        public void IdleAnimation()
        {
            if(!SpeedUp.isPicked)
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
            else if (SpeedUp.isPicked) 
            {
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 3; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/ShipSpeedUp/Idle/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 0.2f, true);
                currentAnimation = idleAnimation;
            }
        }
        public void LeftAnimation()
        {
            if (!SpeedUp.isPicked)
            {
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 3; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/Ship/Left/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 0.2f, true);
                currentAnimation = idleAnimation;
            }
            else if (SpeedUp.isPicked)
            {
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 3; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/ShipSpeedUp/Left/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 0.2f, true);
                currentAnimation = idleAnimation;
            }
        }
        public void RightAnimation()
        {
            if (!SpeedUp.isPicked)
            {
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 3; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/Ship/Right/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 0.2f, true);
                currentAnimation = idleAnimation;
            }
            else if (SpeedUp.isPicked)
            {
                List<IntPtr> idleTextures = new List<IntPtr>();
                for (int i = 0; i < 3; i++)
                {
                    IntPtr frame = Engine.LoadImage($"assets/ShipSpeedUp/Right/{i}.png");
                    idleTextures.Add(frame);
                }
                idleAnimation = new Animation("Idle", idleTextures, 0.2f, true);
                currentAnimation = idleAnimation;
            }
        }


    }
}