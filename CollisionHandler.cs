using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class CollisionHandler
    {
        private Player player;
        private Bullet bullet;

        public CollisionHandler(Player player)
        {
            this.player = player;
        }

        public void CheckCollisions()
        {
            for (int i = 0; i < GameManager.Instance.LevelManager.GameObjects.Count; i++)
            {
                GameObject gameObject = GameManager.Instance.LevelManager.GameObjects[i];
                float distanceX = Math.Abs((gameObject.Transform.Position.x + (gameObject.Transform.Scale.x / 2)) - (player.Transform.Position.x + (player.Transform.Scale.x / 2)));
                float distanceY = Math.Abs((gameObject.Transform.Position.y + (gameObject.Transform.Scale.y / 2)) - (player.Transform.Position.y + (player.Transform.Scale.y / 2)));

                float sumHalfWidth = gameObject.Transform.Scale.x / 2 + player.Transform.Scale.x / 2;
                float sumHalfH = gameObject.Transform.Scale.y / 2 + player.Transform.Scale.y / 2;

                if (distanceX < sumHalfWidth && distanceY < sumHalfH) // Hay colisión
                {
                    HandleCollision(gameObject);
                }
            }
        }

        private void HandleCollision(GameObject gameObject)
        {
            if (gameObject is Asteroid)
            {
                if (!Shield.IsPicked) // Lose Condition
                {
                    Asteroid asteroid = (Asteroid)gameObject;
                    asteroid.Destroy();
                    player.Destroy();


                    GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.lose);
                }
                else
                {
                    GameManager.Instance.LevelManager.GameObjects.Remove(gameObject);
                    player.candie = true;

                    Shield.IsPicked = false;
                    if (player.shield != null)
                    {
                        player.shield.restarAcumulable();
                    }
                }
            }

            if (gameObject is IPickuppeable pickupobj) // Colision con PowerUps
            {
                pickupobj.PickUp();

                if (pickupobj is Shield shieldPicked)  
                {
                    player.shield = shieldPicked;
                    shieldPicked = (Shield)gameObject;
                    shieldPicked.Destroy();
                }

                if (pickupobj is ShootPowerUp)
                {
                    ShootPowerUp shooshootPowerUp = (ShootPowerUp)gameObject;
                    shooshootPowerUp.Destroy();
                }

                if (pickupobj is SpeedUp)
                {
                    SpeedUp speedUp = (SpeedUp)gameObject;
                    speedUp.Destroy();
                }
            }
        }

    }
}
