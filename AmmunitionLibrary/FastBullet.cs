using OpenTK;

namespace AmmunitionLibrary
{
    public class FastBullet : Bullet
    {
        public override int Damage { get; set; } = 20;
        public override float Speed { get; set; } = 0.035f;

        public FastBullet(Vector2 startPosition, int textureID, bool direction) : base()
        {
            PositionCenter = startPosition;
            TextureID = textureID;
            this.direction = direction ? new Vector2(Speed, 0f) : new Vector2(-Speed, 0f);
        }

        public override Vector2[] GetPosition()
        {
            return new Vector2[4]
            {
                PositionCenter + new Vector2(-0.05f, -0.03f),
                PositionCenter + new Vector2(0.05f, -0.03f),
                PositionCenter + new Vector2(0.05f, 0.03f),
                PositionCenter + new Vector2(-0.05f, 0.03f),
            };
        }
    }
}
