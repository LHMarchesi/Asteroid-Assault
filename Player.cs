using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyGame
{
    public class Player : GameObject
    {
        public event Action<Player> onDestroy;

        public PlayerController controller;
        private PlayerLimits playerLimits;
        private CollisionHandler collisionHandler;
        private Vector2 originalPosition;

        private DateTime timeLastShoot;
        private float timeBetweenShoots = 1f;
        private GenericPool<Bullet> bulletPool;

        public Shield shield;
        public PowerUp powerUp;

        static private Animation idleAnimation;

        public bool candie = true;

        public Player(Vector2 position) : base(position)
        {
            playerLimits = new PlayerLimits(this);
            controller = new PlayerController(transform);
            collisionHandler = new CollisionHandler(this);
            originalPosition = position;
            bulletPool = new GenericPool<Bullet>(8, () => new Bullet(new Vector2(Transform.Position.x, Transform.Position.y + Transform.Scale.y)));
            onDestroy += ResetPosition;
            IdleAnimation();
        }

        public override void Update()
        {
            base.Update();
            controller.GetInputs();

            collisionHandler.CheckCollisions();
            playerLimits.CheckLimits();

            if (Time.timeElapse > Time.winTime)
            {
                GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.win);
            }
        }

        public void Destroy()
        {
            onDestroy?.Invoke(this);
        }

        private void ResetPosition(Player player)
        {
            transform.SetPosition(originalPosition);
           
        }

        public float GetPositionX()
        {
            return transform.Position.x;
        }


        // Animations
        public void IdleAnimation()
        {
            if (!SpeedUp.isPicked)
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

        public void Shoot()
        {
            //ShootPowerUp.restarAcumulable();
            DateTime currentTime = DateTime.Now;
            if ((currentTime - timeLastShoot).TotalSeconds >= timeBetweenShoots)
            {
                Bullet bullet = bulletPool.GetObject();
                Bullet bullet2 = bulletPool.GetObject();
                if (bullet != null)
                {
                    bullet.Transform.SetPosition(new Vector2(transform.Position.x + Transform.Scale.x, transform.Position.y + 60 ));
                    bullet2.Transform.SetPosition(new Vector2(transform.Position.x + 20, transform.Position.y + 60));
                    GameManager.Instance.LevelManager.GameObjects.Add(bullet);
                    GameManager.Instance.LevelManager.GameObjects.Add(bullet2);
                    timeLastShoot = currentTime;
                }
            }

        }
    }
}