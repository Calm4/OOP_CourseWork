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
        public HealthPrize(int textureID,Vector2 centerPosition)
        {
            this.textureID = textureID;
            this.centerPosition = centerPosition;
        }
        
       

    }
}
