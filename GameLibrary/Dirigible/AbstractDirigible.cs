using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.DirigibleDecorators;
using System.Drawing;

namespace GameLibrary.Dirigible
{
    public abstract class AbstractDirigible
    {

        public abstract int DirigibleID { get; set; }
        public Vector2 PositionCenter;
        protected Vector2 gunOffset;
        public Vector2 velocity;
        public Vector2 dirigibleWindEffect;


        public bool IsMove { get; set; }
        public bool IsShoot { get; set; }
        public bool IsWindWork { get; set; }

        public abstract int Health { get; set; }
        public abstract int Armor { get; set; }
        public abstract int Fuel { get; set; }
        public abstract int Ammo { get; set; }
        public abstract float Speed { get; set; }

        public abstract void GetDamage(int damage);
        public abstract void ChangeDirectionWithWind(Vector2 newWindSpeed);
        public abstract void ChangeWindDirection(bool turnOver);

        public abstract void Control(List<Key> keys, int textureIdLeft, int textureIdRight, RectangleF checkPlayArea);
        // public abstract void Shoot(List<Key> keys, int[] texture, KeyboardState keyboardState);
        public abstract Vector2 GetGunPosition();
        public abstract void Move(Vector2 movement);
        public abstract void Idle();

        protected abstract float[] Convert(float x, float y);
        public abstract RectangleF GetCollider();
        protected virtual Vector2[] GetPosition()
        {
            return new Vector2[4]
           {
                PositionCenter + new Vector2(-0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, 0.1f),
                PositionCenter + new Vector2(-0.1f, 0.1f),
           };
        }
        public virtual void Render()
        {
            ObjectRenderer.RenderObjects(DirigibleID, GetPosition());
        }
    }
}
