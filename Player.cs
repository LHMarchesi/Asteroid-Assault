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

        public static ShootPowerUp shootPowerUp;
        public static Shield shield;

        private DateTime timeLastShoot;
        private float timeBetweenShoots = 1f;
        private GenericPool<Bullet> bulletPool;

        static private Animation animation;

        public bool candie = true;
        static public bool shipBlue = true;
        static public bool shipRed = false;
        static public bool shipGreen = false;

        public Player(Vector2 position) : base(position)
        {
            playerLimits = new PlayerLimits(this);
            controller = new PlayerController(transform);
            collisionHandler = new CollisionHandler(this);

            originalPosition = position;
            onDestroy += ResetPosition;
            //GameManager.Instance.LevelManager.OnLevelStart += ResetPowerUps;
            //GameManager.Instance.LevelManager.OnLevelEnd += ResetPowerUps;

            IdleAnimation();

            bulletPool = new GenericPool<Bullet>(8, () => new Bullet(new Vector2(Transform.Position.x, Transform.Position.y + Transform.Scale.y)));
        }

        public override void Update()
        {
            base.Update();
            controller.GetInputs();
            collisionHandler.CheckCollisions();
            playerLimits.CheckLimits();
        }

        public override void Render()
        {
            base.Render();

            if (shield != null) // Si tiene el escudo
            {
                if (shipBlue)
                {
                    Shield.shieldImage = Engine.LoadImage("assets/ShieldShip/0.png");
                }
                else if (shipRed)
                {
                    Shield.shieldImage = Engine.LoadImage("assets/ShieldShip/1.png");
                }
                else if (shipGreen)
                {
                    Shield.shieldImage = Engine.LoadImage("assets/ShieldShip/2.png");
                }

                Engine.Draw(Shield.shieldImage, transform.Position.x - 25, transform.Position.y - 25);
            }
        }

        public void Shoot()
        {
            DateTime currentTime = DateTime.Now;
            if ((currentTime - timeLastShoot).TotalSeconds >= timeBetweenShoots)
            {
                if (shootPowerUp != null && PowerUpManager.shootPUCollected.Count >= 1)
                {

                    Bullet bullet = bulletPool.GetObject();
                    Bullet bullet2 = bulletPool.GetObject();
                    if (bullet != null && bullet2 != null)
                    {
                        bullet.Transform.SetPosition(new Vector2(transform.Position.x + Transform.Scale.x, transform.Position.y + 60));
                        bullet2.Transform.SetPosition(new Vector2(transform.Position.x + 20, transform.Position.y + 60));
                        GameManager.Instance.LevelManager.GameObjects.Add(bullet);
                        GameManager.Instance.LevelManager.GameObjects.Add(bullet2);
                        timeLastShoot = currentTime;

                        PowerUpManager.shootPUCollected.Remove(shootPowerUp);
                        shootPowerUp = PowerUpManager.shootPUCollected.Count > 0 ? PowerUpManager.shootPUCollected[0] : null;
                    }
                }
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

        public void IdleAnimation()
        {
            if (shipBlue)
            {
                animation = Animator.CreateAnimation("Ship1Idle", "assets/Ship/Ships/Ship1/Idle/", 4, 20f, true);
                currentAnimation = animation;
            }
            else if (shipRed)
            {
                animation = Animator.CreateAnimation("Ship2Idle", "assets/Ship/Ships/Ship2/Idle/", 4, 20f, true);
                currentAnimation = animation;
            }
            else if (shipGreen)
            {
                animation = Animator.CreateAnimation("Ship3Idle", "assets/Ship/Ships/Ship3/Idle/", 4, 20f, true);
                currentAnimation = animation;
            }
        }

        public void LeftAnimation()
        {
            if (shipBlue)
            {
                animation = Animator.CreateAnimation("Ship1Left", "assets/Ship/Ships/Ship1/Left/", 3, 20f, true);
                currentAnimation = animation;
            }
            else if (shipRed)
            {
                animation = Animator.CreateAnimation("Ship2Left", "assets/Ship/Ships/Ship2/Left/", 3, 20f, true);
                currentAnimation = animation;
            }
            else if (shipGreen)
            {
                animation = Animator.CreateAnimation("Ship3Left", "assets/Ship/Ships/Ship3/Left/", 3, 20f, true);
                currentAnimation = animation;
            }
        }

        public void RightAnimation()
        {
            if (shipBlue)
            {
                animation = Animator.CreateAnimation("Ship1Right", "assets/Ship/Ships/Ship1/Right/", 3, 20f, true);
                currentAnimation = animation;
            }
            else if (shipRed)
            {
                animation = Animator.CreateAnimation("Ship2Right", "assets/Ship/Ships/Ship2/Right/", 3, 20f, true);
                currentAnimation = animation;
            }
            else if (shipGreen)
            {
                animation = Animator.CreateAnimation("Ship3Right", "assets/Ship/Ships/Ship3/Right/", 3, 20f, true);
                currentAnimation = animation;
            }
        }
    }
}