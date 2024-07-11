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
                if (player.candie) // Lose Condition
                {
                    Asteroid asteroid = (Asteroid)gameObject;
                    asteroid.Destroy();
                    player.Destroy();
                    GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.lose);
                }
                else
                {
                    GameManager.Instance.LevelManager.GameObjects.Remove(gameObject);

                    // Remover Shield
                    if (PowerUpManager.shieldCollected != null)  
                    {
                        Shield shieldToRemove = null;

                        // Encontrar el Shield asociado al jugador
                        foreach (Shield shield in PowerUpManager.shieldCollected)
                        {
                            if (shield == Player.shield)
                            {
                                shieldToRemove = shield;
                                break;
                            }
                        }

                        if (shieldToRemove != null)
                        {
                            PowerUpManager.shieldCollected.Remove(shieldToRemove);
                            Player.shield = PowerUpManager.shieldCollected.Count > 0 ? PowerUpManager.shieldCollected[0] : null; // Después de eliminar un Shield, actualiza Player.shield al siguiente Shield en la lista
                           
                            if (Player.shield == null)
                            {
                                player.candie = true;
                            }
                        }
                    }
                }
            }

            if (gameObject is IPickuppeable pickupobj) // Colision con PowerUps
            {
                pickupobj.PickUp();

                if (pickupobj is Shield shieldPicked)
                {
                    player.candie = false;
                    Player.shield = shieldPicked;
                    shieldPicked = (Shield)gameObject;
                    shieldPicked.Destroy();
                }

                if (pickupobj is ShootPowerUp shootPowerUpPicked)
                {
                    ShootPowerUp shootPowerUp = (ShootPowerUp)gameObject;
                    Player.shootPowerUp = shootPowerUpPicked;
                    shootPowerUp.Destroy();
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
