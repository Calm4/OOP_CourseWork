using GameLibrary;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary.Prizes
{
    public class FuelPrize : Prize
    {
        public FuelPrize(Vector2 centerPosition)
        {
            this.textureID = this.textureID = CreateTexture.LoadTexture("fuelPrize.png");
            this.centerPosition = centerPosition;
        }
    }
}
