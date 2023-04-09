using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Dirigible;

namespace PrizesLibrary.Prizes
{
    internal interface IPrize
    {
        void UsePrize(BasicDirigible dirigible);
    }
}
