using GameLibrary.Dirigible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.DirigibleDecorators
{
    public class FuelBoostDecorator : DirigibleDecorator
    {
        public FuelBoostDecorator(AbstractDirigible dirigible) : base(dirigible) { }

        private int getExtraFuel()
        {
            return 200;
        }
        public override int GetFuel()
        {
            return base.GetFuel() + getExtraFuel();
        }
    

    }
}
