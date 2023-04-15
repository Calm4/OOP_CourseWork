using GameLibrary.Dirigible;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.DirigibleDecorators
{
    public class SpeedBoostDecorator : DirigibleDecorator
    {
        public SpeedBoostDecorator(AbstractDirigible dirigible) : base(dirigible) { }
        
        public Vector2 GetExtraSpeed() { return new Vector2(); }

        public override void Move(Vector2 movement)
        {
            base.Move(movement);    
        }
    }
}
