

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using Tao.Sdl;

namespace MyGame
{

    class Program
    {
        
        static void Main(string[] args)
        {
            Engine.Initialize();
            GameManager.Instance.Initialize();

            while (true)
            {
                GameManager.Instance.Render();
                GameManager.Instance.Update();                

                Sdl.SDL_Delay(20);
            }
        }
        
    }
}
