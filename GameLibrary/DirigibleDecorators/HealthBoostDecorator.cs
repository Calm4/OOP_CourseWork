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
        
        public HealthBoostDecorator(AbstractDirigible dirigible) : base(dirigible) 
        {
          
        }

        private int GetExtraHealth() { return 50; }

        public override int GetHealth()
        {
            return _dirigible.GetHealth() + 50;
        }
      
        public override void GetDamage(int damage)
        {
            Health -= damage;
            _dirigible.GetDamage(damage);
        }

    }
}
