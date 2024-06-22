using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Asteroid : GameObject, IPoolable
    {
        public event Action<IPoolable> OnDestroy;
    
        public Asteroid(Vector2 position, int speed) : base(position) // Construcrtor
        {
           
            transform = new Transform(position, new Vector2(75, 75));
            OnDestroy += RemoveAsteroid;
        }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
        }

        private void RemoveAsteroid(IPoolable asteroid)
        {
            GameManager.Instance.LevelManager.GameObjects.Remove(this);
        }


    }


}
