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
        private const float _maxSpeed = 0.0175f;
        public SpeedBoostDecorator(AbstractDirigible dirigible, float extraSpeed) : base(dirigible)
        {
            _extraSpeed = extraSpeed;
         
        }

        public override float Speed
        {
            get { return Math.Min(_dirigible.Speed + _extraSpeed, _maxSpeed); }
            set { _dirigible.Speed = value; }
        }
    }
}
