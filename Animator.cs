using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public static class Animator
    {
        public static Animation CreateAnimation(string name, string assetsPath, int frameCount, float speed, bool isLoopEnabled)
        {
            List<IntPtr> frames = new List<IntPtr>();
            for (int i = 0; i < frameCount; i++)
            {
                IntPtr frame = Engine.LoadImage($"{assetsPath}/{i}.png");
                frames.Add(frame);
            }
            return new Animation(name, frames, speed, isLoopEnabled);
        }
    }
}
