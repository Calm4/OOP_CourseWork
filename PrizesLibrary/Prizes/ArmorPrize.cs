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
    public class ArmorPrize : Prize
    {
        public ArmorPrize(Vector2 centerPosition)
        {
            this.textureID = this.textureID = CreateTexture.LoadTexture("armorPrize.png");
            this.centerPosition = centerPosition;
        }
    }
}
