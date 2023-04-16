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
using AmmunitionLibrary;



namespace DirigibleBattle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        BasicDirigible firstPlayer;
        BasicDirigible secondPlayer;

        List<Bullet> firstPlayerAmmo;
        List<Bullet> secondPlayerAmmo;

        bool wasFirstPlayerFirePressed = false;
        bool wasSecondPlayerFirePressed = false;

        DispatcherTimer gameTimer;
        RectangleF mountineCollider;
        RectangleF screenBorderCollider;
        KeyboardState keyboardState;

        int backGroundTexture;
        int mountainRange;

        int commonBulletTexture;

        int firstDirigibleTextureRight;
        int firstDirigibleTextureLeft;
        int secondDirigibleTextureRight;
        int secondDirigibleTextureLeft;

        List<OpenTK.Input.Key> firstPlayerInput = new List<OpenTK.Input.Key>()
            {
                OpenTK.Input.Key.W,
                OpenTK.Input.Key.S,
                OpenTK.Input.Key.A,
                OpenTK.Input.Key.D,
            };
        List<OpenTK.Input.Key> secondPlayerInput = new List<OpenTK.Input.Key>()
            {
                  OpenTK.Input.Key.Up,
                OpenTK.Input.Key.Down,
                OpenTK.Input.Key.Left,
                OpenTK.Input.Key.Right,
            };


        public MainWindow()
        {
            InitializeComponent();
            GameSettings();
            AddTexture();
            AddObjects();
            StartTimer();
        }
        private void GameSettings()
        {
            var settings = new GLWpfControlSettings { MajorVersion = 3, MinorVersion = 6 };
            glControl.Start(settings);
            glControl.InvalidateVisual();
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }
        private void AddTexture()
        {
            firstDirigibleTextureRight = CreateTexture.LoadTexture("dirigible_red_right_side.png");
            firstDirigibleTextureLeft = CreateTexture.LoadTexture("dirigible_red_left_side.png");
            secondDirigibleTextureRight = CreateTexture.LoadTexture("dirigible_blue_right_side.png");
            secondDirigibleTextureLeft = CreateTexture.LoadTexture("dirigible_blue_left_side.png");
            commonBulletTexture = CreateTexture.LoadTexture("CommonPulya.png");
            backGroundTexture = CreateTexture.LoadTexture("sky.png");
            mountainRange = CreateTexture.LoadTexture("mountine.png");
        }
        private void AddObjects()
        {
            firstPlayer = new BasicDirigible(new Vector2(-0.6f, -0.4f), firstDirigibleTextureRight);
            secondPlayer = new BasicDirigible(new Vector2(0.5f, 0f), secondDirigibleTextureLeft);
            firstPlayerAmmo = new List<Bullet>();
            secondPlayerAmmo = new List<Bullet>();
            screenBorderCollider = new RectangleF(0.0f, 0.125f, 1.0f, 0.875f);
        }
        private void StartTimer()
        {
            gameTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10.0) }; // ~100 FPS
            gameTimer.Tick += Timer_Tick;
            gameTimer.Start();
        }

        private void GameRender()
        {
            firstPlayer.Idle();
            secondPlayer.Idle();

            if (firstPlayer.GetCollider().IntersectsWith(secondPlayer.GetCollider()))
            {
                gameTimer.Stop();

                MessageBox.Show("НИЧЬЯ", "ИГРА ОКОНЧЕНА", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }

            if (mountineCollider.IntersectsWith(firstPlayer.GetCollider()) || !firstPlayer.IsAlive())
            {
                gameTimer.Stop();
                MessageBox.Show("ПОБЕДИЛ ИГРОК НА СИНЕМ ДИРИЖАБЛЕ", "ИГРА ОКОНЧЕНА", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            if (mountineCollider.IntersectsWith(secondPlayer.GetCollider()) || !secondPlayer.IsAlive())
            {
                gameTimer.Stop();
                MessageBox.Show("ПОБЕДИЛ ИГРОК НА КРАСНОМ ДИРИЖАБЛЕ", "ИГРА ОКОНЧЕНА", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            for (int i = 0; i < firstPlayerAmmo.Count; i++)
            {

                firstPlayerAmmo[i].Fire();


                if (secondPlayer.GetCollider().IntersectsWith(firstPlayerAmmo[i].GetCollider()))
                {
                    firstPlayerAmmo.RemoveAt(i);
                    secondPlayer.Health -= 20;
                }
                else if (!firstPlayerAmmo[i].GetCollider().IntersectsWith(screenBorderCollider))
                {
                    firstPlayerAmmo.RemoveAt(i);
                }
            }

            for (int i = 0; i < secondPlayerAmmo.Count; i++)
            {

                secondPlayerAmmo[i].Fire();
                if (firstPlayer.GetCollider().IntersectsWith(secondPlayerAmmo[i].GetCollider()))
                {
                    secondPlayerAmmo.RemoveAt(i);
                    firstPlayer.Health -= 20;
                }
                else if (!secondPlayerAmmo[i].GetCollider().IntersectsWith(screenBorderCollider))
                {
                    secondPlayerAmmo.RemoveAt(i);
                }
            }

        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            GameRender();
            ShootControl();

            firstPlayer.Controls(firstPlayerInput, firstDirigibleTextureLeft, firstDirigibleTextureRight);
            secondPlayer.Controls(secondPlayerInput, secondDirigibleTextureLeft, secondDirigibleTextureRight);

            glControl.InvalidateVisual();
        }

        private void glControl_Render(TimeSpan obj)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            ObjectRenderer.RenderObjects(backGroundTexture, new Vector2[4] {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
            });

            ObjectRenderer.RenderObjects(mountainRange, new Vector2[4] {
                new Vector2(-1.0f, 0.8f),
                new Vector2(1.0f, 0.8f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1f),
            });

            mountineCollider = new RectangleF(0.0f, -0.1f, 1.0f, 0.2f);

            firstPlayer.Render();
            secondPlayer.Render();

            foreach (Bullet bullet in firstPlayerAmmo)
            {
                bullet.Render();
            }
            foreach (Bullet bullet in secondPlayerAmmo)
            {
                bullet.Render();
            }


        }


        public void ShootControl()
        {
            // КАК-ТО ПОПЫТАТЬСЯ ВЫНЕСТИ СТРЕЛЬБУ В МЕТОД ДИРИЖАБЛЯ, НО РЕШИВ ПРОБЛЕМУ С ССЫЛКАМИ
            keyboardState = OpenTK.Input.Keyboard.GetState();

            bool firstPlayerFire = keyboardState.IsKeyDown(OpenTK.Input.Key.Space);
            bool secondPlayerFire = keyboardState.IsKeyDown(OpenTK.Input.Key.Enter);


            //============================Точечная стрельба(без спама)============================//
            if (!wasFirstPlayerFirePressed && firstPlayerFire)
            {

                firstPlayerAmmo.Add(new CommonBullet(firstPlayer.PositionCenter - new Vector2(0f, -0.05f), commonBulletTexture, true));
            }
            if (!wasSecondPlayerFirePressed && secondPlayerFire)
            {

                secondPlayerAmmo.Add(new CommonBullet(secondPlayer.PositionCenter - new Vector2(-0f, -0.05f), commonBulletTexture, false));
            }


            wasFirstPlayerFirePressed = firstPlayerFire;
            wasSecondPlayerFirePressed = secondPlayerFire;
        }

        
    }
}
