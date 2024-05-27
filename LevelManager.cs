﻿using MyGame.assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyGame
{
    public class LevelManager
    {
        private DifficultManager difficultManager = new DifficultManager();
        public List<GameObject> GameObjects = new List<GameObject>();

        private Player player = new Player(new Vector2(565, 520));
        public Player Player => player;

        Font font = new Font("assets/Fonts/Fuente.ttf", 24);

        private Time _time;

        // Escenas de juego
        public IntPtr winScreen = Engine.LoadImage("assets/Win.png");
        public IntPtr loseScreen = Engine.LoadImage("assets/Lose.png");
        public IntPtr gameScreen = Engine.LoadImage("assets/BackGround.png");
        public IntPtr menuScreen = Engine.LoadImage("assets/MainMenu.png");

        public IntPtr shieldIsPicked = Engine.LoadImage("assets/ShieldShip/0.png");

        
        public void Initialize()
        {

            _time.Initialize();

        }
        public void Update()
        {
            player.Update();

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }

            difficultManager.Spawner();
            _time.Update();

        }

        public void Render()
        {
            Engine.Clear();

            Engine.Draw(gameScreen, 0, 0);
            player.Render();

            if (Player.shieldPicked) // Si tiene el escudo
            {
                Engine.Draw(shieldIsPicked, player.Transform.Position.x - 25, player.Transform.Position.y - 25);
            }

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Render();
            }

            Engine.DrawText($"{Math.Max(0, (int)Time.timeElapse)}", 640, 10, 255, 255, 255, font);

            Engine.Show();
        }

       
    }
}
