using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Dirigible
{
    public class BasicDirigible : AbstractDirigible
    {
        public BasicDirigible(Vector2 start,int textrure)
        {
            PositionCenter = start;
            dirigibleID = textrure;
            this.Speed = new Vector2(0, 0.001f);
        }
        public Vector2 Speed { get; set; }
        public Vector2 PositionCenter;
        public int dirigibleID { get; set; }
        public bool isMove { get; set; }
        public int Health { get; set; } = 100;
        public int Armor { get; set; } = 100;
        public int Ammo { get; set; } = 30;
        public int Fuel { get; set; } = 5500;

        public override void Controls()
        {
            throw new NotImplementedException();
        }

        public override int GetAmmo()
        {
            return Ammo;
        }

        public override int GetArmor()
        {
            return Armor;
        }

        public override int GetHealth()
        {
            return Health;
        }

        public override Vector2 GetSpeed()
        {
            return Speed;
        }


        public override bool IsAlive()
        {
            return Health > 0 && Armor > 0;
        }
        public override void Idle()
        {
            isMove = true;
            PositionCenter += Speed;
            isMove = false;
        }

        public override void Move(Vector2 movement)
        {
            if (isMove || Fuel <= 0)
                return;
            PositionCenter += movement;
            Fuel--;

            Debug.WriteLine("Fuel is " + Fuel);
        }
        public void Render()
        {
            ObjectRenderer.RenderObjects(dirigibleID, GetPosition());
        }
        private Vector2[] GetPosition()
        {
            return new Vector2[4]
           {
                PositionCenter + new Vector2(-0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, 0.1f),
                PositionCenter + new Vector2(-0.1f, 0.1f),
           };
        }
    }
}
