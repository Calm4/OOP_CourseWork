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
        DispatcherTimer windTimer;

        readonly PrizeFactory prizeFactory = new PrizeFactory();
        readonly List<Prize> prizeList = new List<Prize>();
        readonly Random random = new Random();

        RectangleF mountineCollider;
        RectangleF screenBorderCollider;
        KeyboardState keyboardState;

        int backGroundTexture;
        int mountainRange;

        private int numberOfFirstPlayerPrizes = 0;
        private int numberOfSecondPlayerPrizes = 0;

        int commonBulletTexture;
        int fastBulletTexture;
        int heavyBulletTexture;
        int firstDirigibleTextureRight;
        int firstDirigibleTextureLeft;
        int secondDirigibleTextureRight;
        int secondDirigibleTextureLeft;

        // ПОДУМАТЬ О РЕАЛИЗАЦИИ ВЕТРА
        readonly private bool isFirstPlayerWindLeft = false; // true - ветер дует налево, false - направо
        readonly private bool isSecondPlayerWindLeft = false;
        private int windCounter = 0;
        private float windSpeedPlayer = 0.0f;
        private int windIsWork = 4;
        private int windTimerTicks = 50;
        private bool isWork = false;

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

        readonly List<OpenTK.Input.Key> firstPlayerFire = new List<OpenTK.Input.Key>()
        {
            OpenTK.Input.Key.Z,
            OpenTK.Input.Key.X,
            OpenTK.Input.Key.C,
        };
        readonly List<OpenTK.Input.Key> secondPlayerFire = new List<OpenTK.Input.Key>()
        {
            OpenTK.Input.Key.Insert,
            OpenTK.Input.Key.PageUp,
            OpenTK.Input.Key.PageDown,
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
            commonBulletTexture = CreateTexture.LoadTexture("CommonBullet.png");
            fastBulletTexture = CreateTexture.LoadTexture("FastBullet.png");
            heavyBulletTexture = CreateTexture.LoadTexture("HeavyBullet.png");
            backGroundTexture = CreateTexture.LoadTexture("clouds2.png");
            mountainRange = CreateTexture.LoadTexture("mountine2.png");
        }
        private void AddObjects()
        {
            firstPlayer = new BasicDirigible(new Vector2(-0.6f, -0.4f), firstDirigibleTextureRight);
            secondPlayer = new BasicDirigible(new Vector2(0.5f, 0f), secondDirigibleTextureLeft);
            firstPlayerAmmo = new List<Bullet>();
            secondPlayerAmmo = new List<Bullet>();
            screenBorderCollider = new RectangleF(0f, 0.125f, 1.025f, 0.875f);
            mountineCollider = new RectangleF(0.0f, -0.1f, 1.0f, 0.185f);
        }
        private void StartTimer()
        {
            gameTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(8.0) }; 
            gameTimer.Tick += GameTimer_Tick;

            prizeTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(8.0) };
            prizeTimer.Tick += PrizeTimer_Tick;

            windTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(8.0) };
            windTimer.Tick += WindTimer_Tick;

            gameTimer.Start();
            prizeTimer.Start();
            windTimer.Start();
        }

        
        private void WindTimer_Tick(object sender, EventArgs e) 
        {
            if (windIsWork == 4)
            {
                if (windCounter <= windTimerTicks)
                {
                    windSpeedPlayer = (float)(random.NextDouble() * (0.005f - 0.0005) + 0.0005);
                    firstPlayer.ChangeDirectionWithWind(new Vector2(windSpeedPlayer, 0.0f));
                    firstPlayer.ChangeWindDirection(true);
                    secondPlayer.ChangeDirectionWithWind(new Vector2(windSpeedPlayer, 0.0f));
                    secondPlayer.ChangeWindDirection(true);
                    windCounter++;
                }
                else if (windCounter >= (windTimerTicks + 1) && windCounter <= windTimerTicks * 2)
                {
                    windSpeedPlayer = (float)(random.NextDouble() * (0.005f - 0.0005) + 0.0005);
                    firstPlayer.ChangeDirectionWithWind(new Vector2(-windSpeedPlayer, 0.0f));
                    firstPlayer.ChangeWindDirection(true);
                    secondPlayer.ChangeDirectionWithWind(new Vector2(-windSpeedPlayer, 0.0f));
                    secondPlayer.ChangeWindDirection(true);
                    windCounter++;
                }
                else
                {
                    isWork = true;
                    windIsWork = random.Next(1, 5);
                    windCounter = 0;
                    windTimerTicks = random.Next(100, 301);
                }
            }
            else
            {

                if (isWork)
                {
                    windIsWork = random.Next(1, 5);
                }
                else
                {
                    firstPlayer.ChangeDirectionWithWind(new Vector2(0, 0.0f));
                    firstPlayer.ChangeWindDirection(false);
                    secondPlayer.ChangeDirectionWithWind(new Vector2(0, 0.0f));
                    secondPlayer.ChangeWindDirection(false);
                }
            }


            WindDirection();
        }
        private void PrizeTimer_Tick(object sender, EventArgs e)
        {
            if (prizeList.Count < 3 && (numberOfFirstPlayerPrizes < 15 || numberOfSecondPlayerPrizes < 15))
            {
                prizeList.Add(prizeFactory.AddNewPrize());
            }
            for (int i = 0; i < prizeList.Count; i++)
            {
                if (numberOfFirstPlayerPrizes >= 15 && numberOfSecondPlayerPrizes >= 15)
                {
                    prizeList.RemoveAt(prizeList.Count - 1);
                }
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // что бы игра не была бесконечной, игрок может подобрать только до 15 призов
            GameStateCheck();

            CheckPlayerDamage(firstPlayerAmmo, ref secondPlayer);
            CheckPlayerDamage(secondPlayerAmmo, ref firstPlayer);

            ApplyPrize(prizeList, ref firstPlayer, ref numberOfFirstPlayerPrizes);
            ApplyPrize(prizeList, ref secondPlayer, ref numberOfSecondPlayerPrizes);


            PlayerShootControl(firstPlayerFire, firstPlayerAmmo, ref firstPlayer);
            PlayerShootControl(secondPlayerFire, secondPlayerAmmo, ref secondPlayer);

            firstPlayer.Idle();
            secondPlayer.Idle();

            firstPlayer.Control(firstPlayerInput, firstDirigibleTextureLeft, firstDirigibleTextureRight, screenBorderCollider);
            secondPlayer.Control(secondPlayerInput, secondDirigibleTextureLeft, secondDirigibleTextureRight, screenBorderCollider);

            firstPlayerInfo.Content = $"HP:{firstPlayer.Health}/200\nArmor:{firstPlayer.Armor}/50\n" +
                                        $"Ammo:{firstPlayer.Ammo}/30\nSpeed:{firstPlayer.Speed * 100:F1}x/2.0x\n" +
                                        $"Fuel:{firstPlayer.Fuel}/3000\nPrizes:{numberOfFirstPlayerPrizes}/15\n";

            secondPlayerInfo.Content = $"HP:{secondPlayer.Health}/200\nArmor:{secondPlayer.Armor}/50\n" +
                                        $"Ammo:{secondPlayer.Ammo}/30\nSpeed:{secondPlayer.Speed * 100:F1}x/2.0x\n" +
                                        $"Fuel:{secondPlayer.Fuel}/3000\nPrizes:{numberOfSecondPlayerPrizes}/15\n";

        }
        private void WindDirection()
        {
            if ((firstPlayer.GetCollider().X <= screenBorderCollider.X) && !isFirstPlayerWindLeft)
            {
                firstPlayer.ChangeWindDirection(false);
            }
            else if ((firstPlayer.GetCollider().X + firstPlayer.GetCollider().Width >= screenBorderCollider.X + screenBorderCollider.Width - 0.04f) && !isFirstPlayerWindLeft)
            {
                firstPlayer.ChangeWindDirection(false);
            }
            else
                firstPlayer.ChangeWindDirection(true);
            if ((secondPlayer.GetCollider().X <= screenBorderCollider.X) && !isSecondPlayerWindLeft)
            {
                secondPlayer.ChangeWindDirection(false);
            }
            else if ((secondPlayer.GetCollider().X + secondPlayer.GetCollider().Width >= screenBorderCollider.X + screenBorderCollider.Width - 0.04f) && !isSecondPlayerWindLeft) // || 
            {
                secondPlayer.ChangeWindDirection(false);
            }
            else
                secondPlayer.ChangeWindDirection(true);
        }
        private void ApplyPrize(List<Prize> prizeList, ref AbstractDirigible player, ref int prizeCounter)
        {
            for (int i = 0; i < prizeList.Count; i++)
            {
                Prize prize = prizeList[i];

                if (player.GetCollider().IntersectsWith(prize.GetCollider()) && prizeCounter < 15)
                {
                    if (prize.GetType().Equals(typeof(AmmoPrize)))
                    {
                        int ammoBoostCount = random.Next(2, 6);
                        player = new AmmoBoostDecorator(player, ammoBoostCount);
                        prizeCounter++;
                    }
                    if (prize.GetType().Equals(typeof(ArmorPrize)))
                    {
                        int armorBoostCount = random.Next(10, 31);
                        player = new ArmorBoostDecorator(player, armorBoostCount);
                        prizeCounter++;
                    }
                    if (prize.GetType().Equals(typeof(FuelPrize)))
                    {
                        int fuelBoostCount = random.Next(250, 751);
                        player = new FuelBoostDecorator(player, fuelBoostCount);
                        prizeCounter++;
                    }
                    if (prize.GetType().Equals(typeof(HealthPrize)))
                    {
                        int healthBoostCount = random.Next(10, 31);
                        player = new HealthBoostDecorator(player, healthBoostCount);
                        prizeCounter++;
                    }
                    if (prize.GetType().Equals(typeof(SpeedBoostPrize)))
                    {
                        float speedBoostCount = (float)(random.NextDouble() * 0.002 + 0.0005);
                        player = new SpeedBoostDecorator(player, speedBoostCount);
                        prizeCounter++;
                    }
                    prizeList.Remove(prize);
                    i--;
                }
            }
        }

        private void PlayerShootControl(List<OpenTK.Input.Key> keys, List<Bullet> bulletsList, ref AbstractDirigible player)
        {
            keyboardState = OpenTK.Input.Keyboard.GetState();

            bool playerFireCommon = keyboardState.IsKeyDown(keys[0]);
            bool playerFireFast = keyboardState.IsKeyDown(keys[1]);
            bool playerFireHeavy = keyboardState.IsKeyDown(keys[2]);

            bool wasPlayerFirePressed = (player == firstPlayer) ? wasFirstPlayerFirePressed : wasSecondPlayerFirePressed;

            if (!wasPlayerFirePressed && (playerFireCommon || playerFireFast || playerFireHeavy))
            {
                if (player.Ammo > 0)
                {
                    if (playerFireCommon)
                    {
                        bulletsList.Add(new CommonBullet(player.GetGunPosition() - new Vector2(0f, -0.05f), commonBulletTexture, player.DirigibleID == firstDirigibleTextureRight));
                    }
                    if (playerFireFast)
                    {
                        bulletsList.Add(new FastBullet(player.GetGunPosition() - new Vector2(0f, -0.05f), fastBulletTexture, player.DirigibleID == firstDirigibleTextureRight));
                    }
                    if (playerFireHeavy)
                    {
                        bulletsList.Add(new HeavyBullet(player.GetGunPosition() - new Vector2(0f, -0.05f), heavyBulletTexture, player.DirigibleID == firstDirigibleTextureRight));
                    }
                    player.Ammo--;
                }
                if (player == firstPlayer)
                {
                    wasFirstPlayerFirePressed = true;
                }
                else
                {
                    wasSecondPlayerFirePressed = true;
                }
            }
            else if (wasPlayerFirePressed && !(playerFireCommon || playerFireFast || playerFireHeavy))
            {
                if (player == firstPlayer)
                {
                    wasFirstPlayerFirePressed = false;
                }
                else
                {
                    wasSecondPlayerFirePressed = false;
                }
            }
        }
        public void CheckPlayerDamage(List<Bullet> bulletList, ref AbstractDirigible player)
        {
            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                bulletList[i].Fire();

                if (player.GetCollider().IntersectsWith(bulletList[i].GetCollider()))
                {
                    player.GetDamage(bulletList[i].Damage);
                    bulletList.RemoveAt(i);
                    continue;
                }
                if (!bulletList[i].GetCollider().IntersectsWith(screenBorderCollider))
                {
                    bulletList.RemoveAt(i);
                }
            }
        }
        private void GameStateCheck()
        {

            if (firstPlayer.GetCollider().IntersectsWith(secondPlayer.GetCollider()))
            {
                gameTimer.Stop();

                MessageBox.Show("НИЧЬЯ", "ИГРА ОКОНЧЕНА", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }

            if (mountineCollider.IntersectsWith(firstPlayer.GetCollider()))
            {
                gameTimer.Stop();
                MessageBox.Show("ПОБЕДИЛ ИГРОК НА [СИНЕМ] ДИРИЖАБЛЕ\n\tИГРОК НА [КРАСНОМ] ДИРИЖАБЛЕ ВРЕЗАЛСЯ В ГОРУ", "ИГРА ОКОНЧЕНА",
                                        MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            if (firstPlayer.Health <= 0)
            {
                gameTimer.Stop();
                MessageBox.Show("\tПОБЕДИЛ ИГРОК НА [СИНЕМ] ДИРИЖАБЛЕ", "ИГРА ОКОНЧЕНА",
                                        MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            if (mountineCollider.IntersectsWith(secondPlayer.GetCollider()))
            {
                gameTimer.Stop();
                MessageBox.Show("ПОБЕДИЛ ИГРОК НА [КРАСНОМ] ДИРИЖАБЛЕ\n\tИГРОК НА [СИНЕМ] ДИРИЖАБЛЕ ВРЕЗАЛСЯ В ГОРУ", "ИГРА ОКОНЧЕНА",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            if (secondPlayer.Health <= 0)
            {
                gameTimer.Stop();
                MessageBox.Show("\tПОБЕДИЛ ИГРОК НА [КРАСНОМ] ДИРИЖАБЛЕ", "ИГРА ОКОНЧЕНА",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }
        private void GlControl_Render(TimeSpan obj)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            ObjectRenderer.RenderObjects(backGroundTexture, new Vector2[4] {
                new Vector2(-1.0f, -1.0f),
                new Vector2(1.0f, -1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(-1.0f, 1.0f),
            });

            ObjectRenderer.RenderObjects(mountainRange, new Vector2[4] {
                new Vector2(-1.0f, 0.775f),
                new Vector2(1.0f, 0.775f),
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
