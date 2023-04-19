using GameLibrary.Dirigible;
using OpenTK;
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
            _dirigible.Fuel += extraFuel;
        }

        public override int Fuel
        {
            get { return _dirigible.Fuel; }
            set { _dirigible.Fuel = value; }
        }

        public override void Move(Vector2 movement)
        {
            if (IsMove || Fuel <= 0)
                return;

            if (_extraFuel > 0)
            {
                _extraFuel--;
            }
            else
            {
                _dirigible.Move(movement);
                
            }
        }




    }
}
