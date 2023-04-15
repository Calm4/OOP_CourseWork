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
using GameLibrary.DirigibleDecorators;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Wpf;
using System.Security.Policy;
using System.Drawing;
using System.Windows.Threading;
using System.Diagnostics;
using GameLibrary.Dirigible;



namespace DirigibleBattle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        int backGroundTexture;
        int mountainRange;
        BasicDirigible firstPlayer;
        BasicDirigible secondPlayer;

        int firstDirigibleTexture;
        int secondDirigibleTexture;
        bool isWdown, isSdown, isIdown, isKdown, isJdown, isDdown;

        public MainWindow()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings { MajorVersion = 3, MinorVersion = 6 };
            glControl.Start(settings);
            glControl.InvalidateVisual();
            GL.Enable(EnableCap.Texture2D);
            firstDirigibleTexture = CreateTexture.LoadTexture("dirigible.png");
            secondDirigibleTexture = CreateTexture.LoadTexture("dirigible.png");
            firstPlayer = new BasicDirigible(new Vector2(-0.6f, -0.4f), firstDirigibleTexture);
            secondPlayer = new BasicDirigible(new Vector2(0.5f, 0f), secondDirigibleTexture);


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(16.0); // ~60 FPS
            timer.Tick += Timer_Tick;
            timer.Start();


        }

        private void GameRender()
        {
            if (isWdown)
                firstPlayer.Move(new Vector2(0f, 0.01f));
            if (isSdown)
                firstPlayer.Move(new Vector2(0f, -0.01f));
            if (isIdown)
                secondPlayer.Move(new Vector2(0f, 0.01f));
            if (isKdown)
                secondPlayer.Move(new Vector2(0f, -0.01f));

            firstPlayer.Idle();
            secondPlayer.Idle();

        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            GameRender();
            InputControl();
            glControl.InvalidateVisual();
        }

        private void glControl_Loaded(object sender, RoutedEventArgs e)
        {

            backGroundTexture = CreateTexture.LoadTexture("sky.png");
            mountainRange = CreateTexture.LoadTexture("mountine.png");



            Debug.WriteLine(firstPlayer + "!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

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

            ObjectRenderer.RenderObjects(mountainRange, new Vector2[4] {
                new Vector2(-1.0f, 0.8f),
                new Vector2(1.0f, 0.8f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1f),
            });
            //ObjectRenderer.Begin((int)this.Width, (int)this.Height);

            // ЭТО БОЛЬШЕ НЕ НАДО!
            /*ObjectRenderer.RenderObjects(firstDirigibleTexture, new Vector2[4] {
                new Vector2(-0.7f, -0.5f),
                new Vector2(-0.5f, -0.5f),
                new Vector2(-0.5f, -0.3f),
                new Vector2(-0.7f, -0.3f),
            });*/

            //ObjectRenderer.Begin((int)this.Width, (int)this.Height);

            /*ObjectRenderer.RenderObjects(secondDirigibleTexture, new Vector2[4] {
                new Vector2(0.75f, 0.25f),
                new Vector2(0.5f, 0.25f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0.75f, 0.5f),
            });*/



            firstPlayer.Render();
            secondPlayer.Render();
        }

        public void InputControl()
        {
            KeyboardState keyboardState = OpenTK.Input.Keyboard.GetState();
            Vector2 moveVectorFirstPlayer = Vector2.Zero;
            Vector2 moveVectorSecondPlayer = Vector2.Zero;
            float firstPlayerSpeed = 0.01f ,secondPlayerSpeed = 0.01f;

            if (keyboardState.IsKeyDown(OpenTK.Input.Key.W))
            {
                moveVectorFirstPlayer += new Vector2(0f,-0.001f);
                //firstPlayer.Move(new Vector2(0f, -0.01f));
            }

            if (keyboardState.IsKeyDown(OpenTK.Input.Key.S))
            {
                moveVectorFirstPlayer += new Vector2(0f, 0.001f);
                //firstPlayer.Move(new Vector2(0f, 0.01f));
            }
            
            if (keyboardState.IsKeyDown(OpenTK.Input.Key.A))
            {
                moveVectorFirstPlayer += new Vector2(-0.001f,0f);
                //firstPlayer.Move(new Vector2(-0.01f, 0f));
            }

            if (keyboardState.IsKeyDown(OpenTK.Input.Key.D))
            {
                moveVectorFirstPlayer += new Vector2(0.001f,0f);
                //firstPlayer.Move(new Vector2(0.01f, 0f));
            }
            if (keyboardState.IsKeyDown(OpenTK.Input.Key.Up))
            {
                moveVectorSecondPlayer += new Vector2(0f, -0.001f);
                //firstPlayer.Move(new Vector2(0f, -0.01f));
            }

            if (keyboardState.IsKeyDown(OpenTK.Input.Key.Down))
            {
                moveVectorSecondPlayer += new Vector2(0f, 0.001f);
                //firstPlayer.Move(new Vector2(0f, 0.01f));
            }

            if (keyboardState.IsKeyDown(OpenTK.Input.Key.Left))
            {
                moveVectorSecondPlayer += new Vector2(-0.001f, 0f);
                //firstPlayer.Move(new Vector2(-0.01f, 0f));
            }

            if (keyboardState.IsKeyDown(OpenTK.Input.Key.Right))
            {
                moveVectorSecondPlayer += new Vector2(0.001f, 0f);
                //firstPlayer.Move(new Vector2(0.01f, 0f));
            }
            // Нормализует передвижение 
            if (moveVectorFirstPlayer != Vector2.Zero)
            {
                moveVectorFirstPlayer = Vector2.Normalize(moveVectorFirstPlayer) * firstPlayerSpeed;
            }
            if (moveVectorSecondPlayer != Vector2.Zero)
            {
                moveVectorSecondPlayer = Vector2.Normalize(moveVectorSecondPlayer) * secondPlayerSpeed;
            }
            firstPlayer.Move(moveVectorFirstPlayer);
            secondPlayer.Move(moveVectorSecondPlayer);
            
        }


    }
}
