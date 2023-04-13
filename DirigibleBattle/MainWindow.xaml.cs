using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenTK;
using OpenTK.Graphics;
using GameLibrary;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Wpf;
using System.Security.Policy;
using System.Drawing;

namespace DirigibleBattle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        int backGroundTexture;

        public MainWindow()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings{MajorVersion = 3, MinorVersion = 6 };
            glControl.Start(settings);
            glControl.InvalidateVisual();

            GL.Enable(EnableCap.Texture2D);

        }

        

        private void glControl_Loaded(object sender, RoutedEventArgs e)
        {
            backGroundTexture = CreateTexture.LoadTexture("sky.png");
        }

        private void glControl_Render(TimeSpan obj)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.BlanchedAlmond);

            GL.BindTexture(TextureTarget.Texture2D, backGroundTexture);
            GL.Begin(PrimitiveType.Quads);

            //GL.Color3(System.Drawing.Color.Red);
            GL.TexCoord2(0, 0);
            GL.Vertex2(-1f, 1f);

           // GL.Color3(System.Drawing.Color.Blue);
            GL.TexCoord2(1, 0);
            GL.Vertex2(1f, 1f);

           // GL.Color3(System.Drawing.Color.Green);
            GL.TexCoord2(1, 1);
            GL.Vertex2(1f, -1f);

            //GL.Color3(System.Drawing.Color.Magenta);
            GL.TexCoord2(0, 1);
            GL.Vertex2(-1f, -1f);

            GL.End();

        }
    }

}
