﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrizesLibrary.Prizes;
using System.Drawing;
using PrizesLibrary;
using OpenTK;
using GameLibrary.Dirigible;
using GameLibrary.DirigibleDecorators;
using GameLibrary;

namespace PrizesLibrary.Factories
{
    /* public abstract class PrizeFactory
     {
        //??? public abstract Prize CreatePrize(Vector2 centerPosition, int textureID);
         public abstract Prize CreatePrize(int textureID, Vector2 centerPosition);


     }*/
    public class PrizeFactory
    {
        public Prize AddNewPrize()
        {

            Random random = new Random();
            float randomPosX, randomPosY;
            Prize prize = null;
            int prizeNumber = random.Next(0, 5);
            randomPosX = (float)(random.NextDouble() * 1.5 - 0.75); // -0.75 до 0.75
            randomPosY = (float)(random.NextDouble() * 1.5 - 0.75); // -0.75 до 0.75


            switch (prizeNumber)
            {
                case 0:
                    prize = new AmmoPrize(new Vector2(randomPosX, randomPosY));
                    break;
                case 1:
                    prize = new ArmorPrize(new Vector2(randomPosX, randomPosY));
                    break;
                case 2:
                    prize = new HealthPrize(new Vector2(randomPosX, randomPosY));
                    break;
                case 3:
                    prize = new SpeedBoostPrize(new Vector2(randomPosX, randomPosY));
                    break;
                case 4:
                    prize = new FuelPrize(new Vector2(randomPosX, randomPosY));
                    break;
                default:
                    break;
            }
            return prize;
        }
    }
}
