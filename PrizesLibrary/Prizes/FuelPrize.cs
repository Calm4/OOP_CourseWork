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
        public FuelPrize(int textureID, Vector2 centerPosition)
        {
            this.textureID = textureID;
            this.centerPosition = centerPosition;
        }
    }
}
