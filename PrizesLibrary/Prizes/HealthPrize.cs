using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Dirigible;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.ES10;

namespace PrizesLibrary.Prizes
{
    public class HealthPrize : Prize
    {
        public HealthPrize(Vector2 centerPosition)
        {
            this.textureID = CreateTexture.LoadTexture("healthPrize.png");
            this.centerPosition = centerPosition;
        }
        
       

    }
}
