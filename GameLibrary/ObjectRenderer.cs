using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace GameLibrary
{
    public class ObjectRenderer
    {
        public static void RenderObjects(int textureID, Vector2[] objectPosition)
        {
            Vector2[] vertices = new Vector2[4]
            {
                new Vector2(0,0),// ┌ лево верх 
                new Vector2(1,0),// ┐ право верх
                new Vector2(1,1),//  ┘ право низ
                new Vector2(0,1),// └ лево низ
            };

            GL.BindTexture(TextureTarget.Texture2D, textureID);

            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < vertices.Length; i++)
            {
                GL.TexCoord2(vertices[i]);
                GL.Vertex2(objectPosition[i]);
            }

            GL.End();
        }
        public static void Begin(int screenWidth, int screenHeight)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // лево право низ верх z-вблизи z-вдали
            GL.Ortho(-1f, 1f, 1f, -1f, -1f, 1f);


            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

        }
    }
}
