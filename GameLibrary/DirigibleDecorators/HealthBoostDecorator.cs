using GameLibrary.Dirigible;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameLibrary.DirigibleDecorators
{
    public class HealthBoostDecorator : DirigibleDecorator
    {
        private int _extraHealth;
        public HealthBoostDecorator(AbstractDirigible dirigible, int extraHealth) : base(dirigible) 
        {
            _extraHealth = extraHealth;
        }

        private int GetExtraHealth() { return 50; }

        public override int Health
        {
            get { return _dirigible.Health + _extraHealth; }
            set { _dirigible.Health = value; }
        }

        

    }
}
