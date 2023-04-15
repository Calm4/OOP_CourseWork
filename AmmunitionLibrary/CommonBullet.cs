using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary;
using System.Drawing;

namespace AmmunitionLibrary
{
    public class CommonBullet : Bullet
    {
        public CommonBullet(Vector2 startPosition,int textureID)
        {
            PositionCenter = startPosition;
            TextureID = textureID;

        }
        public override void Render()
        {
            ObjectRenderer.RenderObjects(TextureID, GetPosition());
        }
        private Vector2[] GetPosition()
        {
            return new Vector2[4]
            {
                PositionCenter + new Vector2(-0.03f, -0.015f),
                PositionCenter + new Vector2(0.03f, -0.015f),
                PositionCenter + new Vector2(0.03f, 0.015f),
                PositionCenter + new Vector2(-0.03f, 0.015f),
            };
        }
        public override void Fire()
        {
            PositionCenter += new Vector2(0.025f, 0);
        }
        public override RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition(); 

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth, colliderHeight);

            return collider;
        }
        private static float[] Convert(float pointX, float pointY)
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
