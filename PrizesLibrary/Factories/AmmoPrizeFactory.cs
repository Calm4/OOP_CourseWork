using PrizesLibrary.Prizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizesLibrary.Factories
{
    internal class AmmoPrizeFactory : IPrizeFactory
    {
        public IPrize CreatePrize()
        {
            return new AmmoPrize();
        }
    }
}
