using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Shield : GameObject, IPickuppeable, IAcumulabble
    {
        private ObjectsMovement objectsMovement;
        private Animation idleAnimation;
        public static bool IsPicked = false;
        private int shieldSpeed = 5;

        private int maxAcumulaciones = 2;
        private static int acumulaciones;



        public Shield(Vector2 pos) : base(pos)
        {
            transform = new Transform(pos, new Vector2(0, 0));
            CreateAnimations();
            objectsMovement = new ObjectsMovement(transform, shieldSpeed);
        }

        public void PickUp()
        {
            IsPicked = true;
            Acumulable();
        }

        public void Acumulable()
        {
            if (IsPicked)
            {
                if (acumulaciones < maxAcumulaciones)
                {
                    acumulaciones++;
                }
            }
        }

        public void restarAcumulable()
        {
            acumulaciones--;
            if (acumulaciones == 0)
            {
                IsPicked = false;
            }
            else
            {
                IsPicked = true;
            }

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
