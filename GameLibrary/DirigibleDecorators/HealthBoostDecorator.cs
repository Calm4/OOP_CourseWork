using GameLibrary.Dirigible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameLibrary.DirigibleDecorators
{
    public class HealthBoostDecorator : DirigibleDecorator
    {
        public HealthBoostDecorator(AbstractDirigible dirigible) : base(dirigible) { }

        public int GetExtraHealth() { return 50; }

        public override int GetHealth()
        {
            return _dirigible.GetHealth() + GetExtraHealth();
        }

        public override bool IsAlive()
        {
            return base.IsAlive() && GetHealth() > 0;
        }
    }
}
