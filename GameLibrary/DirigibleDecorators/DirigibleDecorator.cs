using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Dirigible;
using OpenTK;
using OpenTK.Input;

namespace GameLibrary.DirigibleDecorators
{
    public abstract class DirigibleDecorator : AbstractDirigible
    {
        protected AbstractDirigible _dirigible;

        public DirigibleDecorator(AbstractDirigible dirigible)
        {
            _dirigible = dirigible;
        }

        public override int Health
        {
            get { return _dirigible.Health; }
            set { _dirigible.Health = value; }
        }
        public override int Armor
        {
            get { return _dirigible.Armor; }
            set { _dirigible.Armor = value; }
        }
       /* public override int GetHealth()
        {
            return _dirigible.GetHealth();
        }*/

        public override void GetDamage(int damage)
        {
            _dirigible.GetDamage(damage);

        }
       /* public override int GetArmor()
        {
            return _dirigible.GetArmor();
        }*/
        public override void SetArmor(int armor)
        {
            _dirigible.SetArmor(armor);
        }

        public override int GetAmmo()
        {
            return _dirigible.GetAmmo();
        }

        public override float GetSpeed()
        {
            return _dirigible.GetSpeed();
        }
        public override int GetFuel()
        {
            return _dirigible.GetFuel();
        }
       
        public override void Control(List<Key> keys, int textureIdLeft, int textureIdRight)
        {
            _dirigible.Control(keys, textureIdLeft, textureIdRight);
        }

        public override void Shoot(List<Key> keys, int[] texture)
        {
            throw new NotImplementedException();
        }

        public override void Idle()
        {
            _dirigible.Idle();
        }
        public override void Move(Vector2 movement)
        {
            _dirigible.Move(movement);
        }
        public override void Render()
        {
            _dirigible.Render();
        }
        public override RectangleF GetCollider()
        {
            return _dirigible.GetCollider();
        }
        protected override Vector2[] GetPosition()
        {
            return new Vector2[4]
           {
                PositionCenter + new Vector2(-0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, 0.1f),
                PositionCenter + new Vector2(-0.1f, 0.1f),
           };
        }
        protected override float[] Convert(float pointX, float pointY)
        {
            float centralPointX = 0.5f;
            float centralPointY = 0.5f;

            float[] resultPoint = new float[2];

            resultPoint[0] = centralPointX + pointX / 2.0f;
            resultPoint[1] = centralPointY - pointY / 2.0f;

            return resultPoint;
        }

    }
}
