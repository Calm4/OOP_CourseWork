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
            Ammo = 15;
            Speed = 0.01f; // ????
            Fuel = 5000; //2000
            IsShoot = false;
            gunOffset = new Vector2(0, 0f);
            dirigibleWindEffect = new Vector2(0.0f, 0.0f);

        }
        public override void ChangeDirectionWithWind(Vector2 newWindSpeed)
        {
            dirigibleWindEffect = newWindSpeed;
        }
        public override void ChangeWindDirection(bool turnOver)
        {
            IsWindWork = turnOver;
        }
        public Vector2 PassiveSpeed { get; set; }

        public override int Health { get; set; }
        public override int Armor { get; set; }
        public override int Fuel { get; set; }
        public override int Ammo { get; set; }
        public override float Speed { get; set; }
        public override int DirigibleID { get; set; }

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
                moveVectorFirstPlayer = Vector2.Normalize(moveVectorFirstPlayer) * Speed;
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
        public override void GetDamage(int damage)
        {
            int tempHealth = damage - Armor;
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
            if (IsWindWork)
                PositionCenter += dirigibleWindEffect;
        }
    }
}
