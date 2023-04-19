﻿using OpenTK;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.DirigibleDecorators;
using System.Drawing;

namespace GameLibrary.Dirigible
{
    public abstract class AbstractDirigible
    {
        public int DirigibleID { get; set; }
        public Vector2 PositionCenter;

        public bool IsMove { get; set; }
        public abstract int Health { get; set; } 
        public abstract int Armor { get; set; }
        public int Ammo { get; set; } 
        public float ActiveSpeed { get; set; }
        public int Fuel { get; set; }
       // public abstract int GetHealth();
        public abstract void GetDamage(int damage);
        public abstract float GetSpeed();
       // public abstract int GetArmor();
        public abstract void SetArmor(int armor);
        public abstract int GetAmmo();
        public abstract int GetFuel();
        public abstract void Control(List<Key> keys, int textureIdLeft, int textureIdRight);
        public abstract void Shoot(List<Key> keys, int[] texture);

        public abstract void Move(Vector2 movement);
        public abstract void Idle();
        
       

        protected abstract float[] Convert(float x, float y);
        public abstract RectangleF GetCollider();
        protected virtual Vector2[] GetPosition()
        {
            return new Vector2[4]
           {
                PositionCenter + new Vector2(-0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, -0.1f),
                PositionCenter + new Vector2(0.1f, 0.1f),
                PositionCenter + new Vector2(-0.1f, 0.1f),
           };
        }
        public abstract void Render();
    }
}
