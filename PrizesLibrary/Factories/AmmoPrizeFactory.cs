using OpenTK;
using PrizesLibrary.Prizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary.Factories
{
    public class AmmoPrizeFactory : PrizeFactory
    {
        public override Prize CreatePrize(int textureID, Vector2 centerPosition)
        {
            return new AmmoPrize(textureID, centerPosition);
        }
    }
}
