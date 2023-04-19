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
using System.Threading;
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
            /* Debug.WriteLine(bd.Health); //100
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
             Debug.WriteLine(bd.Armor); //0*/

            Debug.WriteLine("=========================");
            Debug.WriteLine(bd.Ammo); //10

            bd = new AmmoBoostDecorator(bd, 5);

            Debug.WriteLine("=========================");
            Debug.WriteLine(bd.Ammo); //15





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
            mountineCollider = new RectangleF(0.0f, -0.1f, 1.0f, 0.185f);
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


            if (prizeList.Count < 3)
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

            firstPlayer.Control(firstPlayerInput, firstDirigibleTextureLeft, firstDirigibleTextureRight, screenBorderCollider);

            secondPlayer.Control(secondPlayerInput, secondDirigibleTextureLeft, secondDirigibleTextureRight, screenBorderCollider);


            glControl.InvalidateVisual();


            //Debug.WriteLine(firstPlayer.Fuel);
        }

        int numberOfFirstPlayerPrizes = 0;
        int numberOfSecondPlayerPrizes = 0;
        private void GameRender()
        {
            // что бы игра не была бесконечной, игрок может подобрать только до 15 призов
            firstPlayer.Idle();
            secondPlayer.Idle();
            GameStateCheck();

            for (int i = firstPlayerAmmo.Count - 1; i >= 0; i--)
            {

                firstPlayerAmmo[i].Fire();


                if (secondPlayer.GetCollider().IntersectsWith(firstPlayerAmmo[i].GetCollider()))
                {
                    secondPlayer.GetDamage(firstPlayerAmmo[i].Damage);
                    Debug.WriteLine("Health: " + secondPlayer.Health);
                    Debug.WriteLine("Armor: " + secondPlayer.Armor);
                    firstPlayerAmmo.RemoveAt(i);
                    continue;
                }

                if (!firstPlayerAmmo[i].GetCollider().IntersectsWith(screenBorderCollider))
                {
                    firstPlayerAmmo.RemoveAt(i);
                }
            }

            for (int i = secondPlayerAmmo.Count - 1; i >= 0; i--)
            {
                secondPlayerAmmo[i].Fire();

                if (firstPlayer.GetCollider().IntersectsWith(secondPlayerAmmo[i].GetCollider()))
                {
                    firstPlayer.GetDamage(secondPlayerAmmo[i].Damage);
                    Debug.WriteLine("Health: " + firstPlayer.Health);
                    Debug.WriteLine("Armor: " + firstPlayer.Armor);
                    secondPlayerAmmo.RemoveAt(i);
                    continue;
                }

                if (!secondPlayerAmmo[i].GetCollider().IntersectsWith(screenBorderCollider))
                {
                    secondPlayerAmmo.RemoveAt(i);
                }
            }
            for (int i = 0; i < prizeList.Count; i++)
            {
                Prize prize = prizeList[i];

                if (firstPlayer.GetCollider().IntersectsWith(prize.GetCollider()) && numberOfFirstPlayerPrizes < 15)
                {
                    numberOfFirstPlayerPrizes++;
                    if (prize.GetType().Equals(typeof(AmmoPrize)))
                    {

                        int ammoBoostCount = random.Next(2, 6);
                        firstPlayer = new AmmoBoostDecorator(firstPlayer, ammoBoostCount);
                        Debug.WriteLine("ammo:" + firstPlayer.Ammo);
                        Debug.WriteLine("NUMBER:" + numberOfFirstPlayerPrizes);

                    }
                    if (prize.GetType().Equals(typeof(ArmorPrize)))
                    {
                        int armorBoostCount = random.Next(10, 31);
                        firstPlayer = new ArmorBoostDecorator(firstPlayer, armorBoostCount);

                        Debug.WriteLine("arrmor:" + firstPlayer.Armor);
                        Debug.WriteLine("NUMBER:" + numberOfFirstPlayerPrizes);

                    }
                    if (prize.GetType().Equals(typeof(FuelPrize)))
                    {
                        int fuelBoostCount = random.Next(500, 1001);
                        firstPlayer = new FuelBoostDecorator(firstPlayer, fuelBoostCount);

                        Debug.WriteLine("fuel:" + firstPlayer.Fuel);
                        Debug.WriteLine("NUMBER:" + numberOfFirstPlayerPrizes);

                    }
                    if (prize.GetType().Equals(typeof(HealthPrize)))
                    {
                        int healthBoostCount = random.Next(10, 31);
                        firstPlayer = new HealthBoostDecorator(firstPlayer, healthBoostCount);

                        Debug.WriteLine("hp:" + firstPlayer.Health);
                        Debug.WriteLine("NUMBER:" + numberOfFirstPlayerPrizes);

                    }
                    if (prize.GetType().Equals(typeof(SpeedBoostPrize)))
                    {
                        float speedBoostCount = (float)(random.NextDouble() * 0.002 + 0.0005);
                        firstPlayer = new SpeedBoostDecorator(firstPlayer, speedBoostCount);

                        Debug.WriteLine("speed:" + firstPlayer.Speed);
                        Debug.WriteLine("NUMBER:" + numberOfFirstPlayerPrizes);

                    }
                    prizeList.Remove(prize);
                    i--;
                }
            }
            for (int i = 0; i < prizeList.Count; i++)
            {
                Prize prize = prizeList[i];

                if (secondPlayer.GetCollider().IntersectsWith(prize.GetCollider()) && numberOfSecondPlayerPrizes < 15)
                {
                    if (prize.GetType().Equals(typeof(AmmoPrize)))
                    {

                        int ammoBoostCount = random.Next(2, 6);
                        secondPlayer = new AmmoBoostDecorator(secondPlayer, ammoBoostCount);
                        numberOfSecondPlayerPrizes++;
                        Debug.WriteLine("ammo:" + secondPlayer.Ammo);

                    }
                    if (prize.GetType().Equals(typeof(ArmorPrize)))
                    {
                        int armorBoostCount = random.Next(10, 31);
                        secondPlayer = new ArmorBoostDecorator(secondPlayer, armorBoostCount);
                        numberOfSecondPlayerPrizes++;
                        Debug.WriteLine("arrmor:" + secondPlayer.Armor);

                    }
                    if (prize.GetType().Equals(typeof(FuelPrize)))
                    {
                        int fuelBoostCount = random.Next(500, 1001);
                        secondPlayer = new FuelBoostDecorator(secondPlayer, fuelBoostCount);
                        numberOfSecondPlayerPrizes++;
                        Debug.WriteLine("fuel:" + secondPlayer.Fuel);

                    }
                    if (prize.GetType().Equals(typeof(HealthPrize)))
                    {
                        int healthBoostCount = random.Next(10, 41);
                        secondPlayer = new HealthBoostDecorator(secondPlayer, healthBoostCount);
                        numberOfSecondPlayerPrizes++;
                        Debug.WriteLine("hp:" + secondPlayer.Health);

                    }
                    if (prize.GetType().Equals(typeof(SpeedBoostPrize)))
                    {
                        float speedBoostCount = (float)(random.NextDouble() * 0.002 + 0.0005);
                        secondPlayer = new SpeedBoostDecorator(secondPlayer, speedBoostCount);
                        numberOfSecondPlayerPrizes++;
                        Debug.WriteLine("speed:" + secondPlayer.Speed);

                    }
                    prizeList.Remove(prize);
                    i--;
                }
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
                if (firstPlayer.Ammo > 0)
                {
                    firstPlayerAmmo.Add(new CommonBullet(firstPlayer.GetGunPosition() - new Vector2(0f, -0.05f), commonBulletTexture, firstPlayer.DirigibleID == firstDirigibleTextureRight));
                    firstPlayer.Ammo--;
                    Debug.WriteLine("Кол-во пуль: " + firstPlayer.Ammo);
                }
                else
                {
                    Debug.WriteLine("НЕДОСТАТОЧНО ПУЛЬ!");
                }
            }
            if (!wasSecondPlayerFirePressed && secondPlayerFire)
            {
                if (secondPlayer.Ammo > 0)
                {
                    secondPlayerAmmo.Add(new CommonBullet(secondPlayer.GetGunPosition() - new Vector2(0f, -0.05f), commonBulletTexture, secondPlayer.DirigibleID == secondDirigibleTextureRight));
                    secondPlayer.Ammo--;
                    Debug.WriteLine("Кол-во пуль: " + secondPlayer.Ammo);
                }
                else
                {
                    Debug.WriteLine("НЕДОСТАТОЧНО ПУЛЬ!");
                }
            }


            wasFirstPlayerFirePressed = firstPlayerFire;
            wasSecondPlayerFirePressed = secondPlayerFire;
        }
        /*  public void PlayerShootSystem(AbstractDirigible player, ref List<Bullet> bulletsList)
          {

              for (int i = bulletsList.Count - 1; i >= 0; i--)
              {
                  bulletsList[i].Fire();

                  if (player.GetCollider().IntersectsWith(bulletsList[i].GetCollider()))
                  {
                      player.GetDamage(bulletsList[i].Damage);
                      Debug.WriteLine("Health: " + player.Health);
                      Debug.WriteLine("Armor: " + player.Armor);
                      bulletsList.RemoveAt(i);
                      continue;
                  }

                  if (!bulletsList[i].GetCollider().IntersectsWith(screenBorderCollider))
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
                        player = new AmmoBoostDecorator(player, 5);

                        Debug.WriteLine("ammo:" + player.Ammo);

                    }
                    if (prize.GetType().Equals(typeof(ArmorPrize)))
                    {
                        player = new ArmorBoostDecorator(player, 20);
                        Debug.WriteLine("arrmor:" + player.Armor);

                    }
                    if (prize.GetType().Equals(typeof(FuelPrize)))
                    {
                        player = new FuelBoostDecorator(player, 200);

                        Debug.WriteLine("fuel:" + player.Fuel);

                    }
                    if (prize.GetType().Equals(typeof(HealthPrize)))
                    {
                        player = new HealthBoostDecorator(player, 50);
                        Debug.WriteLine("hp:" + player.Health);

                    }
                    if (prize.GetType().Equals(typeof(SpeedBoostPrize)))
                    {
                        player = new SpeedBoostDecorator(player, 0.005f);
                        Debug.WriteLine("speed:" + player.Speed);

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


       


    }
}
