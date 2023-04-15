using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Dirigible;
using OpenTK;

namespace GameLibrary.DirigibleDecorators
{
    public abstract class DirigibleDecorator : AbstractDirigible
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

        public override Vector2 GetSpeed()
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
        public override void Idle()
        {
            _dirigible.Idle();
        }
        public override void Move(Vector2 movement)
        {
            _dirigible.Move(movement);
        }
    }
}
