using GameLibrary.Dirigible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.DirigibleDecorators
{
    public class ArmorBoostDecorator : DirigibleDecorator
    {
        public ArmorBoostDecorator(AbstractDirigible dirigible) : base(dirigible) { }
        
        public int GetExtraArmor() { return 30; }

        public override int GetArmor()
        {
            return base.GetArmor() + GetExtraArmor();
        }

    }
}
