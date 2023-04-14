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
using System.Windows.Threading;
using System.Diagnostics;

namespace DirigibleBattle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
       
        int backGroundTexture;
        int mountainRange;
        int firstDirigibleTexture;
        int secondDirigibleTexture;
        

        public MainWindow()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings{MajorVersion = 3, MinorVersion = 6 };
            glControl.Start(settings);
            glControl.InvalidateVisual();
            GL.Enable(EnableCap.Texture2D);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16.0);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine(DateTime.Now);
        }

        private void glControl_Loaded(object sender, RoutedEventArgs e)
        {

            backGroundTexture = CreateTexture.LoadTexture("sky.png");
            mountainRange = CreateTexture.LoadTexture("mountine.png");
            firstDirigibleTexture = CreateTexture.LoadTexture("dirigible.png");
            secondDirigibleTexture = CreateTexture.LoadTexture("dirigible.png");
        }

        private void glControl_Render(TimeSpan obj)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.BlanchedAlmond);

            ObjectRenderer.Begin((int)this.Width, (int)this.Height);

            
            ObjectRenderer.Begin((int)this.Width, (int)this.Height);

            ObjectRenderer.RenderObjects(backGroundTexture, new Vector2[4] {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
            });
            ObjectRenderer.Begin((int)this.Width, (int)this.Height);

            ObjectRenderer.RenderObjects(firstDirigibleTexture, new Vector2[4] {
                new Vector2(-0.75f, -0.5f),
                new Vector2(-0.5f, -0.5f),
                new Vector2(-0.5f, -0.25f),
                new Vector2(-0.75f, -0.25f),
            });

            ObjectRenderer.Begin((int)this.Width, (int)this.Height);

            ObjectRenderer.RenderObjects(secondDirigibleTexture, new Vector2[4] {
                new Vector2(0.75f, 0.25f),
                new Vector2(0.5f, 0.25f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0.75f, 0.5f),
            });

            ObjectRenderer.Begin((int)this.Width, (int)this.Height);

            ObjectRenderer.RenderObjects(mountainRange, new Vector2[4] {
                new Vector2(-1.0f, 0.8f),
                new Vector2(1.0f, 0.8f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1f),
            });
        }
    }

}
