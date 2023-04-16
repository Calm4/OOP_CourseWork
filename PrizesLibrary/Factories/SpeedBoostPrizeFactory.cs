using PrizesLibrary.Prizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary.Factories
{
    internal class SpeedBoostPrizeFactory : PrizeFactory
    {
        public override Prize CreatePrize()
        {
            return new SpeedBoostPrize();
        }
    }
}
