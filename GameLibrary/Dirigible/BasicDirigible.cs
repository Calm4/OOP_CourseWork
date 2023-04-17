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

        }
        public Vector2 PassiveSpeed { get; set; }
        public Vector2 PositionCenter;

        public int DirigibleID { get; set; }
        public bool IsMove { get; set; }
        public int Health { get; set; } = 100;
        public int Armor { get; set; } = 100;
        public int Ammo { get; set; } = 30;
        public float ActiveSpeed { get; set; } = 0.01f;
        public int Fuel { get; set; } = 5000;


        public override void Controls(List<Key> keys, int textureIdLeft, int textureIdRight)
        {
            // W S A D
            // Вверх Низ Лево Право
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 moveVectorFirstPlayer = Vector2.Zero;

            if (keyboardState.IsKeyDown(keys[0]))
            {
                moveVectorFirstPlayer += new Vector2(0f, -0.001f);

            }
            if (keyboardState.IsKeyDown(keys[1]))
            {
                moveVectorFirstPlayer += new Vector2(0f, 0.001f);
            }

            if (keyboardState.IsKeyDown(keys[2]))
            {
                DirigibleID = textureIdLeft;
                moveVectorFirstPlayer += new Vector2(-0.001f, 0f);
            }

            if (keyboardState.IsKeyDown(keys[3]))
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


        public override int GetAmmo()
        {
            return Ammo;
        }

        public override int GetArmor()
        {
            return Armor;
        }

        public override int GetHealth()
        {
            return Health;
        }

        public override float GetSpeed()
        {
            return ActiveSpeed;
        }


        public override bool IsAlive()
        {
            return Health > 0 && Armor > 0;
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
        public void Render()
        {
            ObjectRenderer.RenderObjects(DirigibleID, GetPosition());
        }
        private Vector2[] GetPosition()
        {
            return new Vector2[4]
           {
                PositionCenter + new Vector2(-0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, 0.1f),
                PositionCenter + new Vector2(-0.1f, 0.1f),
           };
        }

        public RectangleF GetCollider()
        {
            Vector2[] colliderPosition = GetPosition();

            float colliderWidth = (colliderPosition[2].X - colliderPosition[3].X) / 2.0f;
            float colliderHeight = (colliderPosition[3].Y - colliderPosition[0].Y) / 2.0f;

            float[] convertedLeftTop = Convert(colliderPosition[3].X, colliderPosition[3].Y);

            RectangleF collider = new RectangleF(convertedLeftTop[0], convertedLeftTop[1], colliderWidth - 0.005f, colliderHeight - 0.03f);

            return collider;
        }
        private static float[] Convert(float pointX, float pointY)
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
