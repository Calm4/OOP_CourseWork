using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Dirigible;

namespace GameLibrary.DirigibleDecorators
{
    public class DirigibleDecorator : AbstractDirigible
    {
        protected AbstractDirigible _dirigible;

        public DirigibleDecorator(AbstractDirigible dirigible)
        {
            _dirigible = dirigible;
        }

        public override int GetHealth()
        {
            return _dirigible.GetHealth();
        }

        public override int GetArmor()
        {
            return _dirigible.GetArmor();
        }

        public override int GetAmmo()
        {
            return _dirigible.GetAmmo();
        }

        public override float GetSpeed()
        {
            return _dirigible.GetSpeed();
        }

        public override void Controls()
        {
            _dirigible.Controls();
        }

        public override bool IsAlive()
        {
            return _dirigible.IsAlive();
        }
    }
}
