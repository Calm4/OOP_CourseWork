using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;

namespace AmmunitionLibrary
{
    public abstract class Bullet
    {
        public Vector2 PositionCenter;
        protected int TextureID { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public abstract void Render();
        public abstract RectangleF GetCollider();
        public abstract void Fire();

    }
}
