﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class RandomNumber
    {
        private int var1;
        private int var2;
        public int Rand(int var1, int var2) // Toma dos valores y devuelve uno aleatoreo
        {
            this.var1 = var1;
            this.var2 = var2;

            Random rnd = new Random();
            return rnd.Next(var1, var2);
        }
    }
}