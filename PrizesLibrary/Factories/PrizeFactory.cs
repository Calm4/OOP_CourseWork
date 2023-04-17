using System;
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
    public abstract class PrizeFactory
    {
       //??? public abstract Prize CreatePrize(Vector2 centerPosition, int textureID);
        public abstract Prize CreatePrize(int textureID, Vector2 centerPosition);
    }
}
