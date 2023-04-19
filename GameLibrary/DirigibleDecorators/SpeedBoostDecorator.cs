using GameLibrary.Dirigible;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.DirigibleDecorators
{
    public class SpeedBoostDecorator : DirigibleDecorator
    {
        private float _extraSpeed;
        public SpeedBoostDecorator(AbstractDirigible dirigible, float extraSpeed) : base(dirigible)
        {
            _extraSpeed = extraSpeed;
            if (_dirigible.Speed < 0.0175f) // Ограничение по max скорости
            {
                _dirigible.Speed += extraSpeed;
            }
        }



        public override float Speed
        {
            get { return _dirigible.Speed; }
            set { _dirigible.Speed = value; }
        }
    }
}
