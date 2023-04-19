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
        public override int Fuel
        {
            get { return _dirigible.Fuel; }
            set { _dirigible.Fuel = value; }
        }
        public override int Ammo
        {
            get { return _dirigible.Ammo; }
            set { _dirigible.Ammo = value; }
        }
        public override float Speed
        {
            get { return _dirigible.Speed; }
            set { _dirigible.Speed = value; }
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

        public override void Control(List<Key> keys, int textureIdLeft, int textureIdRight, RectangleF checkPlayArea)
        {
            _dirigible.Control(keys, textureIdLeft, textureIdRight, checkPlayArea);
        }

        /*public override void Shoot(List<Key> keys, int[] texture)
        {
            throw new NotImplementedException();
        }*/
        public override Vector2 GetGunPosition()
        {
            // Позиция пушки относительно координат дирижабля
            Vector2 gunPosition = _dirigible.GetGunPosition() + gunOffset;

            // Если дирижабль смотрит влево, инвертируем координату X позиции пушки
            if (!IsShoot)
            {
                gunPosition.X = _dirigible.GetGunPosition().X - gunOffset.X;
            }

            return gunPosition;
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
