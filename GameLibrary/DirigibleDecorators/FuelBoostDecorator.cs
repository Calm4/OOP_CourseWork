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
        int _extraFuel;
        public FuelBoostDecorator(AbstractDirigible dirigible,int extraFuel) : base(dirigible) 
        {
              _extraFuel = extraFuel;
        }

        public override int Fuel
        {
            get { return _dirigible.Fuel + _extraFuel; }
            set { _dirigible.Fuel = value; }
        }



    }
}
