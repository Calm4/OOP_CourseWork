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
        public int DirigibleID { get; set; }
        public Vector2 PositionCenter;

        public bool IsMove { get; set; }
        public int Health { get; set; } = 100;
        public int Armor { get; set; } = 100;
        public int Ammo { get; set; } = 30;
        public float ActiveSpeed { get; set; } = 0.01f;
        public int Fuel { get; set; } = 5000;
        public abstract int GetHealth();
        public abstract float GetSpeed();
        public abstract int GetArmor();
        public abstract int GetAmmo();
        public abstract int GetFuel();
        public abstract void Controls(List<Key> keys, int textureIdLeft, int textureIdRight);

        public abstract bool IsAlive(); // Подумать над работой, вроде работает не правильно

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
        public abstract void Render();
    }
}
