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
        private int _extraArmor;
        public ArmorBoostDecorator(AbstractDirigible dirigible, int extraArmor) : base(dirigible)
        {
            _extraArmor = extraArmor;
        }

        public override int Armor
        {
            get { return _dirigible.Armor + _extraArmor; }
            set { _dirigible.Armor = value; }
        }

        public override void GetDamage(int damage)
        {
            if (_extraArmor > 0)
            {
                int tempDamage = damage - _extraArmor;
                if (tempDamage > 0)
                {
                    _dirigible.GetDamage(tempDamage);
                }
                _extraArmor -= damage;
                if (_extraArmor < 0)
                {
                    _extraArmor = 0;
                }
            }
            else
            {
                _dirigible.GetDamage(damage);
            }
        }

    }
}
