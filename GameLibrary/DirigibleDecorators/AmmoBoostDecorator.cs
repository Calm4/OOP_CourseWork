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
        private int _extraAmmo;
        public AmmoBoostDecorator(AbstractDirigible dirigible,int extraAmmo) : base(dirigible) 
        {
            _extraAmmo = extraAmmo;
        }

        private int GetExtraAmmo() { return 30; }

        public override int Ammo
        {
            get { return _dirigible.Ammo + _extraAmmo; }
            set { _dirigible.Ammo = value; }
        }
    }
}
