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
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Wpf;
using System.Security.Policy;
using System.Drawing;
using System.Windows.Threading;
using System.Diagnostics;
using AmmunitionLibrary;
using GameLibrary;
using GameLibrary.DirigibleDecorators;
using GameLibrary.Dirigible;
using PrizesLibrary.Factories;
using PrizesLibrary.Prizes;
using PrizesLibrary;


namespace DirigibleBattle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        AbstractDirigible firstPlayer;
        AbstractDirigible secondPlayer;

        List<Bullet> firstPlayerAmmo;
        List<Bullet> secondPlayerAmmo;

        bool wasFirstPlayerFirePressed = false;
        bool wasSecondPlayerFirePressed = false;

        DispatcherTimer gameTimer;
        DispatcherTimer prizeTimer;
        PrizeFactory prizeFactory;
        List<Prize> prizeList = new List<Prize>();
        Random random = new Random();

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
        int ammoPrizeTexture;
        int armorPrizeTexture;
        int fuelPrizeTexture;
        int healthPrizeTexture;
        int speedPrizeTexture;

        readonly List<OpenTK.Input.Key> firstPlayerInput = new List<OpenTK.Input.Key>()
            {
                OpenTK.Input.Key.W,
                OpenTK.Input.Key.S,
                OpenTK.Input.Key.A,
                OpenTK.Input.Key.D,
            };
        readonly List<OpenTK.Input.Key> secondPlayerInput = new List<OpenTK.Input.Key>()
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

            AbstractDirigible bd = new BasicDirigible(new Vector2(0f, 0f), firstDirigibleTextureLeft);
            Debug.WriteLine(bd.Health); //100
            Debug.WriteLine(bd.Armor); // 50
            bd.GetDamage(40);
            Debug.WriteLine("=========================");
            Debug.WriteLine(bd.Health); //100
            Debug.WriteLine(bd.Armor); //10
            bd.GetDamage(40);
            Debug.WriteLine("=========================");
            Debug.WriteLine(bd.Health); //70
            Debug.WriteLine(bd.Armor); //0
            bd = new ArmorBoostDecorator(bd, 20);
            Debug.WriteLine("=========================");
            Debug.WriteLine(bd.Health); //70
            Debug.WriteLine(bd.Armor); //20
            bd = new HealthBoostDecorator(bd, 50);
            Debug.WriteLine("=========================");
            Debug.WriteLine(bd.Health); //120
            Debug.WriteLine(bd.Armor); //20
            bd.GetDamage(40);
            Debug.WriteLine("=========================");
            Debug.WriteLine(bd.Health); //100
            Debug.WriteLine(bd.Armor); //0
            bd.GetDamage(40);
            Debug.WriteLine("=========================");
            Debug.WriteLine(bd.Health); //60
            Debug.WriteLine(bd.Armor); //0



            // Debug.WriteLine(bd.GetHealth());



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
            ammoPrizeTexture = CreateTexture.LoadTexture("ammoPrize.png");
            armorPrizeTexture = CreateTexture.LoadTexture("armorPrize.png");
            fuelPrizeTexture = CreateTexture.LoadTexture("fuelPrize.png");
            healthPrizeTexture = CreateTexture.LoadTexture("healthPrize.png");
            speedPrizeTexture = CreateTexture.LoadTexture("speedPrize.png");
        }
        private void AddObjects()
        {
            firstPlayer = new BasicDirigible(new Vector2(-0.6f, -0.4f), firstDirigibleTextureRight);
            secondPlayer = new BasicDirigible(new Vector2(0.5f, 0f), secondDirigibleTextureLeft);
            firstPlayerAmmo = new List<Bullet>();
            secondPlayerAmmo = new List<Bullet>();
            screenBorderCollider = new RectangleF(0f, 0.1f, 1f, 0.875f);

        }
        private void StartTimer()
        {
            gameTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10.0) }; // ~100 FPS
            gameTimer.Tick += GameTimer_Tick;

            prizeTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(16.0) };
            prizeTimer.Tick += PrizeTimer_Tick;

            gameTimer.Start();
            prizeTimer.Start();
        }


        private void PrizeTimer_Tick(object sender, EventArgs e)
        {
            PrizeGenerate();

        }

        private void PrizeGenerate()
        {

            float randomPosX, randomPosY;

            if (prizeList.Count < 9)
            {
                int prizeNumber = random.Next(0, 5);
                randomPosX = (float)(random.NextDouble() * 1.5 - 0.75); // -0.75 до 0.75
                randomPosY = (float)(random.NextDouble() * 1.5 - 0.75); // -0.75 до 0.75

                switch (prizeNumber)
                {
                    case 0:
                        prizeFactory = new AmmoPrizeFactory();
                        prizeList.Add(prizeFactory.CreatePrize(ammoPrizeTexture, new Vector2(randomPosX, randomPosY)));

                        break;
                    case 1:
                        prizeFactory = new ArmorPrizeFactory();
                        prizeList.Add(prizeFactory.CreatePrize(armorPrizeTexture, new Vector2(randomPosX, randomPosY)));
                        break;
                    case 2:
                        prizeFactory = new HealthPrizeFactory();
                        prizeList.Add(prizeFactory.CreatePrize(healthPrizeTexture, new Vector2(randomPosX, randomPosY)));
                        break;
                    case 3:
                        prizeFactory = new SpeedBoostPrizeFactory();
                        prizeList.Add(prizeFactory.CreatePrize(speedPrizeTexture, new Vector2(randomPosX, randomPosY)));
                        break;
                    case 4:
                        prizeFactory = new FuelPrizeFactory();
                        prizeList.Add(prizeFactory.CreatePrize(fuelPrizeTexture, new Vector2(randomPosX, randomPosY)));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                return;
            }
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            GameRender();
            ShootControl();

            firstPlayer.Control(firstPlayerInput, firstDirigibleTextureLeft, firstDirigibleTextureRight,screenBorderCollider);

            secondPlayer.Control(secondPlayerInput, secondDirigibleTextureLeft, secondDirigibleTextureRight,screenBorderCollider);


            glControl.InvalidateVisual();

            // Debug.WriteLine(firstPlayer.GetFuel());
        }

        private void GameRender()
        {
            firstPlayer.Idle();
            secondPlayer.Idle();
            GameStateCheck();

            for (int i = 0; i < firstPlayerAmmo.Count; i++)
            {

                firstPlayerAmmo[i].Fire();


                if (secondPlayer.GetCollider().IntersectsWith(firstPlayerAmmo[i].GetCollider()))
                {
                    firstPlayerAmmo.RemoveAt(i);
                    secondPlayer.GetDamage(40); //firstPlayerAmmo[i].Damage

                    Debug.WriteLine("Health: " + secondPlayer.Health);
                    Debug.WriteLine("Armor: " + secondPlayer.Armor);
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
                    //  firstPlayer.GetHealth();
                    firstPlayer.GetDamage(40); //secondPlayerAmmo[i].Damage

                    Debug.WriteLine("Health: " + firstPlayer.Health);
                    Debug.WriteLine("Armor: " + firstPlayer.Armor);
                }
                else if (!secondPlayerAmmo[i].GetCollider().IntersectsWith(screenBorderCollider))
                {
                    secondPlayerAmmo.RemoveAt(i);
                }
            }
            for (int i = 0; i < prizeList.Count; i++)
            {
                Prize prize = prizeList[i];

                if (firstPlayer.GetCollider().IntersectsWith(prize.GetCollider()))
                {
                    if (prize.GetType().Equals(typeof(AmmoPrize)))
                    {
                        firstPlayer = new AmmoBoostDecorator(firstPlayer);

                        Debug.WriteLine("ammo:" + firstPlayer.GetAmmo());

                    }
                    if (prize.GetType().Equals(typeof(ArmorPrize)))
                    {
                        firstPlayer = new ArmorBoostDecorator(firstPlayer, 20);
                        Debug.WriteLine("arrmor:" + firstPlayer.Armor);

                    }
                    if (prize.GetType().Equals(typeof(FuelPrize)))
                    {
                        firstPlayer = new FuelBoostDecorator(firstPlayer);

                        Debug.WriteLine("fuel:" + firstPlayer.GetFuel());

                    }
                    if (prize.GetType().Equals(typeof(HealthPrize)))
                    {
                        firstPlayer = new HealthBoostDecorator(firstPlayer, 50);
                        Debug.WriteLine("hp:" + firstPlayer.Health);

                    }
                    if (prize.GetType().Equals(typeof(SpeedBoostPrize)))
                    {
                        firstPlayer = new SpeedBoostDecorator(firstPlayer);
                        Debug.WriteLine("hp:" + firstPlayer.GetSpeed());

                    }
                    prizeList.Remove(prize);
                    i--;
                }
            }
            for (int i = 0; i < prizeList.Count; i++)
            {
                Prize prize = prizeList[i];

                if (secondPlayer.GetCollider().IntersectsWith(prize.GetCollider()))
                {
                    if (prize.GetType().Equals(typeof(AmmoPrize)))
                    {
                        secondPlayer = new AmmoBoostDecorator(secondPlayer);

                        Debug.WriteLine("ammo:" + secondPlayer.GetAmmo());

                    }
                    if (prize.GetType().Equals(typeof(ArmorPrize)))
                    {
                        secondPlayer = new ArmorBoostDecorator(secondPlayer, 20);
                        Debug.WriteLine("arrmor:" + secondPlayer.Armor);

                    }
                    if (prize.GetType().Equals(typeof(FuelPrize)))
                    {
                        secondPlayer = new FuelBoostDecorator(secondPlayer);

                        Debug.WriteLine("fuel:" + secondPlayer.GetFuel());

                    }
                    if (prize.GetType().Equals(typeof(HealthPrize)))
                    {
                        secondPlayer = new HealthBoostDecorator(secondPlayer, 50);
                        Debug.WriteLine("hp:" + secondPlayer.Health);

                    }
                    if (prize.GetType().Equals(typeof(SpeedBoostPrize)))
                    {
                        secondPlayer = new SpeedBoostDecorator(secondPlayer);
                        Debug.WriteLine("hp:" + secondPlayer.GetSpeed());

                    }
                    prizeList.Remove(prize);
                    i--;
                }
            }


        }
        /*public void PlayerShootSystem(AbstractDirigible player, List<Bullet> bulletsList)
        {
          
            for (int i = 0; i < bulletsList.Count; i++)
            {
                bulletsList[i].Fire();


                if (player.GetCollider().IntersectsWith(bulletsList[i].GetCollider()))
                {
                    bulletsList.RemoveAt(i);
                    player.GetDamage(bulletsList[i].Damage); //firstPlayerAmmo[i].Damage
                    Debug.WriteLine(player.GetHealth());
                }
                else if (!bulletsList[i].GetCollider().IntersectsWith(screenBorderCollider))
                {
                    bulletsList.RemoveAt(i);
                }
            }
        }*/
        public void GameStateCheck()
        {
            if (firstPlayer.GetCollider().IntersectsWith(secondPlayer.GetCollider()))
            {
                gameTimer.Stop();

                MessageBox.Show("НИЧЬЯ", "ИГРА ОКОНЧЕНА", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }

            if (mountineCollider.IntersectsWith(firstPlayer.GetCollider()) || firstPlayer.Health <= 0)
            {
                gameTimer.Stop();
                MessageBox.Show("ПОБЕДИЛ ИГРОК НА СИНЕМ ДИРИЖАБЛЕ", "ИГРА ОКОНЧЕНА",
                                        MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            if (mountineCollider.IntersectsWith(secondPlayer.GetCollider()) || secondPlayer.Health <= 0)
            {
                gameTimer.Stop();
                MessageBox.Show("ПОБЕДИЛ ИГРОК НА КРАСНОМ ДИРИЖАБЛЕ", "ИГРА ОКОНЧЕНА",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }
        public void CheckPlayerBuff(AbstractDirigible player)
        {
            for (int i = 0; i < prizeList.Count; i++)
            {
                Prize prize = prizeList[i];

                if (player.GetCollider().IntersectsWith(prize.GetCollider()))
                {
                    if (prize.GetType().Equals(typeof(AmmoPrize)))
                    {
                        player = new AmmoBoostDecorator(player);

                        Debug.WriteLine("ammo:" + player.GetAmmo());

                    }
                    if (prize.GetType().Equals(typeof(ArmorPrize)))
                    {
                        player = new ArmorBoostDecorator(player, 20);
                        Debug.WriteLine("arrmor:" + player.Armor);

                    }
                    if (prize.GetType().Equals(typeof(FuelPrize)))
                    {
                        player = new FuelBoostDecorator(player);

                        Debug.WriteLine("fuel:" + player.GetFuel());

                    }
                    if (prize.GetType().Equals(typeof(HealthPrize)))
                    {
                        player = new HealthBoostDecorator(player, 50);
                        Debug.WriteLine("hp:" + player.Health);

                    }
                    if (prize.GetType().Equals(typeof(SpeedBoostPrize)))
                    {
                        player = new SpeedBoostDecorator(player);
                        Debug.WriteLine("hp:" + player.GetSpeed());

                    }
                    prizeList.Remove(prize);
                    i--;
                }
            }
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

            foreach (Prize prize in prizeList)
            {
                prize.Render();
            }
        }


        public void ShootControl()
        {
            // КАК-ТО ПОПЫТАТЬСЯ ВЫНЕСТИ СТРЕЛЬБУ В МЕТОД ДИРИЖАБЛЯ, НО РЕШИВ ПРОБЛЕМУ С ССЫЛКАМИ
            keyboardState = OpenTK.Input.Keyboard.GetState();
            bool direction = false;

            if (firstPlayer.DirigibleID == firstDirigibleTextureLeft)
            {
                direction = false;
            }
            else
            {
                direction = true;
            }

            if (secondPlayer.DirigibleID == secondDirigibleTextureLeft)
            {
                direction = false;
            }
            else
            {
                direction = true;
            }


            bool firstPlayerFire = keyboardState.IsKeyDown(OpenTK.Input.Key.Space);
            bool secondPlayerFire = keyboardState.IsKeyDown(OpenTK.Input.Key.Enter);


            //============================Точечная стрельба(без спама)============================//
            if (!wasFirstPlayerFirePressed && firstPlayerFire)
            {
                firstPlayerAmmo.Add(new CommonBullet(firstPlayer.GetGunPosition() - new Vector2(0f, -0.05f), commonBulletTexture, false));
            }
            if (!wasSecondPlayerFirePressed && secondPlayerFire)
            {

                secondPlayerAmmo.Add(new CommonBullet(secondPlayer.GetGunPosition() - new Vector2(0f, -0.05f), commonBulletTexture, false));
            }


            wasFirstPlayerFirePressed = firstPlayerFire;
            wasSecondPlayerFirePressed = secondPlayerFire;
        }


    }
}
