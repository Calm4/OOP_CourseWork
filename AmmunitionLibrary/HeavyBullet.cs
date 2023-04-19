using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmunitionLibrary
{
    public class HeavyBullet : Bullet
    {
        public override int Damage { get; set; } = 40;

        public override void Fire()
        {
            throw new NotImplementedException();
        }

        public override RectangleF GetCollider()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }
    }
}
