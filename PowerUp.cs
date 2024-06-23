using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PowerUp : GameObject, IPoolable
    {
        public event Action<IPoolable> OnDestroy;

        public PowerUp(Vector2 position) : base(position)
        {
            transform = new Transform(position, new Vector2(0, 0));
            OnDestroy += RemovePowerUp;
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
        }
       
        private void RemovePowerUp(IPoolable powerUp)
        {
            GameManager.Instance.LevelManager.GameObjects.Remove(this);
        }

    }
}
