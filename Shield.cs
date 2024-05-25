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
       
        private Animation idleAnimation;
        public Shield(Vector2 pos) : base(pos)
        {
            transform = new Transform(pos, new Vector2(100,100));
            CreateAnimations();
        }
        public void PickUp()
        {
            GameManager.Instance.LevelManager.Player.candie = false;
            Engine.Debug("Shield Obtenido");

        }
        public override void Update()
        {
            base.Update();
        }
        private void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 1; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Ship/Idle/0.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
            currentAnimation = idleAnimation;
        }


    }
}
