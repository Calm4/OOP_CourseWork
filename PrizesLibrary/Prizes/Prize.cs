using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Dirigible;
using PrizesLibrary;
using OpenTK;
using System.Drawing;

namespace PrizesLibrary.Prizes
{
    public abstract class Prize
    {
        Vector2 centerPosition;
        int textureID;
        public abstract void UsePrize(AbstractDirigible dirigible);
            
    }
}
