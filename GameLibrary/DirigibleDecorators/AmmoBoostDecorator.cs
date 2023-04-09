using GameLibrary.Dirigible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.DirigibleDecorators
{
    public class AmmoBoostDecorator : DirigibleDecorator
    {
        public AmmoBoostDecorator(AbstractDirigible dirigible) : base(dirigible) { }

        public int GetExtraAmmo() { return 30; }

        public override int GetAmmo()
        {
            return base.GetAmmo() + GetExtraAmmo();
        }

    }
}
