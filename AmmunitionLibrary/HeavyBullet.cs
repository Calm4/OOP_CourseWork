using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmmunitionLibrary
{
    public class HeavyBullet : Bullet
    {
        
        public override int Damage { get; set; } = 40;
        public override float Speed { get; set; } = 0.015f;

        public HeavyBullet(Vector2 startPosition, int textureID, bool direction) : base()
        {
            PositionCenter = startPosition;
            TextureID = textureID;
            this.direction = direction ? new Vector2(Speed, 0f) : new Vector2(-Speed, 0f);
        }
        public override Vector2[] GetPosition()
        {
            return new Vector2[4]
            {
                PositionCenter + new Vector2(-0.06f, -0.045f),
                PositionCenter + new Vector2(0.06f, -0.045f),
                PositionCenter + new Vector2(0.06f, 0.045f),
                PositionCenter + new Vector2(-0.06f, 0.045f),
            };
        }
    }
}
