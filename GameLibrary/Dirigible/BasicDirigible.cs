using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Dirigible
{
    public class BasicDirigible : AbstractDirigible
    {
        public BasicDirigible(Vector2 startPosition, int textrureID)
        {
            PositionCenter = startPosition;
            DirigibleID = textrureID;
            this.PassiveSpeed = new Vector2(0, 0.001f);

            Health = 100;
            Armor = 50;
            Ammo = 10;
            ActiveSpeed = 0.01f;
            Fuel = 5000;
            IsShoot = false;
            gunOffset = new Vector2(0, 0f);

        }
        public Vector2 PassiveSpeed { get; set; }

        public override int Health { get; set; }
        public override int Armor { get; set; }
        public override int Fuel { get; set; }
        public override int Ammo { get; set; }

        public override void Control(List<Key> keys, int textureIdLeft, int textureIdRight, RectangleF playArea)
        {
            // W S A D
            // Вверх Низ Лево Право
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 moveVectorFirstPlayer = Vector2.Zero;

            if (keyboardState.IsKeyDown(keys[0]) && (GetCollider().Y < playArea.Width - playArea.Y))
            {

                moveVectorFirstPlayer += new Vector2(0f, -0.001f);

            }
            if (keyboardState.IsKeyDown(keys[1]))
            {
                moveVectorFirstPlayer += new Vector2(0f, 0.001f);
            }

            if (keyboardState.IsKeyDown(keys[2]) && (GetCollider().X > playArea.X))
            {

                DirigibleID = textureIdLeft;
                moveVectorFirstPlayer += new Vector2(-0.001f, 0f);
            }

            if (keyboardState.IsKeyDown(keys[3]) && (GetCollider().X < playArea.Width - 0.1f))
            {
                DirigibleID = textureIdRight;
                moveVectorFirstPlayer += new Vector2(0.001f, 0f);

            }
            if (moveVectorFirstPlayer != Vector2.Zero)
            {
                moveVectorFirstPlayer = Vector2.Normalize(moveVectorFirstPlayer) * GetSpeed();
            }
            Move(moveVectorFirstPlayer);
        }

        /* public override void Shoot(List<Key> keys, int[] texture, KeyboardState keyboardState)
         {
             keyboardState = OpenTK.Input.Keyboard.GetState();

             bool direction = false;

             bool playerFire = keyboardState.IsKeyDown(OpenTK.Input.Key.Space);



             //============================Точечная стрельба(без спама)============================//
             if (!IsShoot && playerFire)
             {
                // PlayerAmmo.Add(new CommonBullet(firstPlayer.PositionCenter - new Vector2(0f, -0.05f), commonBulletTexture, true));
             }




             IsShoot = playerFire;


         }*/
        public override Vector2 GetGunPosition()
        {
            // Позиция пушки относительно координат дирижабля
            Vector2 gunPosition = PositionCenter + gunOffset;

            // Если дирижабль смотрит влево, инвертируем координату X позиции пушки
            if (!IsShoot)
            {
                gunPosition.X = PositionCenter.X - gunOffset.X;
            }

            return gunPosition;
        }
   
        public override void SetArmor(int value)
        {
            Armor = value;
        }
               
        public override float GetSpeed()
        {
            return ActiveSpeed;
        }
        

        public override void GetDamage(int damage)
        {
            int tempHealth = damage - Armor; // 30 - 20 = 10
            if (Armor > 0)
            {
                if (Armor > damage)
                {
                    Armor -= damage;
                }
                else
                {
                    Armor = 0;
                    Health -= tempHealth;
                }
            }
            else
            {
                Health -= damage;
            }
            // Health -= damage;
        }

        public override void Idle()
        {
            IsMove = true;

            PositionCenter += PassiveSpeed;

            IsMove = false;
        }

        public override void Move(Vector2 movement)
        {
            if (IsMove || Fuel <= 0)
                return;
            PositionCenter += movement;
            Fuel--;
        }

        public override void Render()
        {
            ObjectRenderer.RenderObjects(DirigibleID, GetPosition());
        }
        protected override Vector2[] GetPosition()
        {
            return new Vector2[4]
           {
                PositionCenter + new Vector2(-0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, 0.1f),
                PositionCenter + new Vector2(-0.1f, 0.1f),
           };
        }

        public override RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition();

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth - 0.005f, colliderHeight - 0.03f);

            return collider;
        }
        protected override float[] Convert(float pointX, float pointY)
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
