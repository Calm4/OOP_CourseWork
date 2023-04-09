using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Dirigible
{
    public abstract class AbstractDirigible
    {
        public abstract int GetHealth() ;
        public abstract float GetSpeed();
        public abstract int GetArmor();
        public abstract int GetAmmo();
        public abstract void Controls();
        public abstract bool IsAlive(); // Подумать над работой, вроде работает не правильно

        
    }
}
