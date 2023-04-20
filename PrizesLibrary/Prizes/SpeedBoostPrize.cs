using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Dirigible;
using OpenTK;

namespace PrizesLibrary.Prizes
{
    public class SpeedBoostPrize : Prize
    {
        public SpeedBoostPrize(Vector2 centerPosition)
        {
            this.textureID = CreateTexture.LoadTexture("speedPrize.png"); ;
            this.centerPosition = centerPosition;
        }

    }
}
