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
        private const int _maxHealth = 200;
        public HealthBoostDecorator(AbstractDirigible dirigible, int extraHealth) : base(dirigible)
        {
            _extraHealth = extraHealth;
        }

        public override int Health
        {
            get { return Math.Min(_dirigible.Health + _extraHealth, _maxHealth); }
            set { _dirigible.Health = value; }
        }



    }
}
