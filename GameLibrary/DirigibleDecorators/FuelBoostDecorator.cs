﻿using GameLibrary.Dirigible;
using OpenTK;


namespace GameLibrary.DirigibleDecorators
{
    public class FuelBoostDecorator : DirigibleDecorator
    {
        int _extraFuel;
        private const int _maxFuel = 3000; // 2000
        public FuelBoostDecorator(AbstractDirigible dirigible,int extraFuel) : base(dirigible) 
        {
              _extraFuel = extraFuel;
            if (_dirigible.Fuel <= _maxFuel)
            {
                if (_dirigible.Fuel <= _maxFuel - _extraFuel)
                {
                    _dirigible.Fuel += _extraFuel;
                }
                else
                {
                    _dirigible.Fuel = _maxFuel;
                }
            }
            else
            {
                _dirigible.Fuel = _maxFuel;
            }
            
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
