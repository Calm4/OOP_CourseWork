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
        public SpeedBoostPrize(int textureID, Vector2 centerPosition)
        {
            this.textureID = textureID;
            this.centerPosition = centerPosition;
        }

    }
}
