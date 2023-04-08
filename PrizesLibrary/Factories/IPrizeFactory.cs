using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrizesLibrary.Prizes;

namespace PrizesLibrary.Factories
{
    internal interface IPrizeFactory
    {
        IPrize CreatePrize();
    }
}
