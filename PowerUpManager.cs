using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class PowerUpManager
    {
        public static List<Shield> shieldCollected = new List<Shield>();
        public static List<ShootPowerUp> shootPUCollected = new List<ShootPowerUp>();

        public static string totalShieldtxt = "0";
        public static string shootTxt = "0";
        public void Handler() // Añada los powerUps
        {
            totalShieldtxt = shieldCollected.Count.ToString();
            foreach (Shield shield in shieldCollected.ToList())
            {
                if (!shield.IsProcessed)
                {
                    AddPowerUp(shield.totalShield, shield.maxShield, totalShieldtxt);
                    shield.IsProcessed = true;
                }
            }

            shootTxt = shootPUCollected.Count.ToString();
            foreach (ShootPowerUp shootPU in shootPUCollected)
            {
                if (!shootPU.isProcessed)
                {
                    AddPowerUp(shootPU.totalShoots, shootPU.maxShoots, shootTxt);
                    shootPU.isProcessed = true;
                }
            }

        }

        private void AddPowerUp(int total, int max, string txt)
        {
            if (total < max)
            {
                total++;
                txt = total.ToString();
            }
            if (total == max)
            {
                txt = max + " (Max)";
            }
        }

        public void ResetPowerUps() // Reseteo de valores
        {
            Player.shield = null;
            shieldCollected.Clear();
            shootPUCollected.Clear();
            totalShieldtxt = "0";
            shootTxt = "0";
        }
    }
}
