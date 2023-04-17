using GameLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Dirigible;
using PrizesLibrary;
using OpenTK;
using System.Drawing;

namespace PrizesLibrary.Prizes
{
    public abstract class Prize
    {
        protected Vector2 centerPosition;
        protected int textureID;

        public void Render()
        {
            ObjectRenderer.RenderObjects(textureID, GetPosition());
        }
        public RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition();

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth - 0.005f, colliderHeight - 0.03f);

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
        protected Vector2[] GetPosition()
        {
            return new Vector2[4]
          {
                centerPosition + new Vector2(-0.05f, -0.05f),
                centerPosition + new Vector2(0.05f, -0.05f),
                centerPosition + new Vector2(0.05f, 0.05f),
                centerPosition + new Vector2(-0.05f, 0.05f),
          };
        }

    }
}
