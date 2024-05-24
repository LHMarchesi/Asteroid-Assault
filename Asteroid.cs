using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    internal class Asteroid : GameObject
    {

        public Transform Transform => transform;

        private Player player;
        private Animation idleAnimation;


        public Asteroid(Vector2 position, Player player) : base(position)
        {
            CreateAnimations();
            this.player = player;
        }


        public override void Update()
        {
            base.Update();

            if (CheckPlayerCollision())
            {
                GameManager.Instance.ChangeGameStatus(GameManager.GameStatus.lose);
            }
        }
        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Meteoro/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
            currentAnimation = idleAnimation;
        }

        private bool CheckPlayerCollision() //Colision con jugador
        {
            // Obtenemos las posiciones y dimensiones del jugador y el enemigo
            float distanceX = Math.Abs((player.Transform.Position.x + (player.Transform.Scale.x / 2)) - (transform.Position.x + (transform.Scale.x / 2)));
            float distanceY = Math.Abs((player.Transform.Position.y + (player.Transform.Scale.y / 2)) - (transform.Position.y + (transform.Scale.y / 2)));

            float sumHalfWidth = player.Transform.Scale.x / 2 + transform.Scale.x / 2;
            float sumHalfH = player.Transform.Scale.y / 2 + transform.Scale.y / 2;

            // Verificamos la colisión
            if (distanceX < sumHalfWidth && distanceY < sumHalfH)
            {
                return true;
            }
            return false;
        }
        // Aumenta dificultad con el tiempo

    }


}
