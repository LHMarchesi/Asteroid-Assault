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
        public static bool shieldPicked = false;

        private int shieldSpeed = 5;

        public Shield(Vector2 pos) : base(pos)
        {
            transform = new Transform(pos, new Vector2(0,0));
            CreateAnimations();
            objectsMovement = new ObjectsMovement(transform, shieldSpeed);
        }

        public void PickUp()
        {
            shieldPicked = true;
        }

        public override void Update()
        {
            base.Update();
            objectsMovement.Move();
            if (transform.Position.y >= 1000)
            {
                GameManager.Instance.LevelManager.GameObjects.Remove(this);
            }
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
