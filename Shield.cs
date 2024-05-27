using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Shield : GameObject, IPickuppeable
    {
        private ObjectsMovement objectsMovement;
        private Animation idleAnimation;
        private int shieldSpeed = 5;
        public Shield(Vector2 pos) : base(pos)
        {
            transform = new Transform(pos, new Vector2(0,0));
            CreateAnimations();
            objectsMovement = new ObjectsMovement(transform, shieldSpeed);
        }
        public void PickUp()
        {
            GameManager.Instance.LevelManager.Player.candie = false;
            GameManager.Instance.LevelManager.Player.shieldPicked = true;
            Engine.Debug("Shield Obtenido");
        }
        public override void Update()
        {
            base.Update();
            objectsMovement.Move();
        }

        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 3; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Shield/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.9f, true);
            currentAnimation = idleAnimation;
        }


    }
}
