using GameLibrary.Dirigible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.DirigibleDecorators
{
    public class SpeedBoostDecorator : DirigibleDecorator
    {
        public SpeedBoostDecorator(AbstractDirigible dirigible) : base(dirigible) { }
        
        public float GetExtraSpeed() { return 20; }

        public override float GetSpeed()
        {
            return base.GetSpeed() + GetExtraSpeed();
        }
    }
}
