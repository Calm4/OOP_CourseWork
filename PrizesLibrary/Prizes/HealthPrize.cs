﻿using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Dirigible;
using OpenTK;

namespace PrizesLibrary.Prizes
{
    internal class HealthPrize : Prize
    {
        public HealthPrize()
        {
            
        }
        public override void UsePrize(AbstractDirigible dirigible)
        {
            dirigible.GetHealth();
        }
    }
}
