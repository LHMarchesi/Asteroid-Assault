using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.assets
{
    public class GameObject
    {
        protected Animation currentAnimation;
        protected Transform transform;
        public Transform Transform => transform;

        public GameObject(Vector2 position) {
            transform = new Transform(position, new Vector2(100, 100));
        }

        public virtual void Render()
        {
            transform.Position = new Vector2(200, 200);
            Engine.Draw(currentAnimation.CurrentFrame, transform.Position.x, transform.Position.y);
        }

        public virtual void Update()
        {
            currentAnimation.Update();
        }
    }

}
