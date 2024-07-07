using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class ScreenManager
    {
        private static ScreenManager instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        // Escenas de juego
        public IntPtr winScreen = Engine.LoadImage("assets/Win.png");
        public IntPtr loseScreen = Engine.LoadImage("assets/Lose.png");
        public IntPtr gameScreen = Engine.LoadImage("assets/BackGround.jpg");
        public IntPtr menuScreen = Engine.LoadImage("assets/MainMenu.jpg");
        public IntPtr pauseScreen = Engine.LoadImage("assets/Pause.jpg");
        public IntPtr skinScreen = Engine.LoadImage("assets/SkinSelector.jpg");

        //Herramientas Seleccion Skin
        public IntPtr boxSelector = Engine.LoadImage("assets/BoxSelector.png");
        public Vector2 boxPosition = new Vector2(20, 115);

    }
}
