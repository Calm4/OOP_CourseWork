using OpenTK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

        public RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition(); // добавить более точную коллизию!

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }
        private static float[] Convert(float pointX, float pointY)
        {
            float centralPointX = 0.5f; // значения (0,0) в OpenGL и WinForms не совпадают
            float centralPointY = 0.5f; // в Winforms - левый верхний гол, OpenGL - центр

            float[] resultPoint = new float[2];

            resultPoint[0] = centralPointX + pointX / 2.0f;
            resultPoint[1] = centralPointY - pointY / 2.0f;

            return resultPoint;
        }
    }
}
