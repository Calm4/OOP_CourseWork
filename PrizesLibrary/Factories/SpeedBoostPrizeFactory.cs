using PrizesLibrary.Prizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary.Factories
{
    internal class SpeedBoostPrizeFactory : IPrizeFactory
    {
        public IPrize CreatePrize()
        {
            return new SpeedBoostPrize();
        }
    }
}
