using GameLibrary;
using OpenTK;

namespace PrizesLibrary.Prizes
{
    public class AmmoPrize : Prize
    {
        public AmmoPrize(Vector2 centerPosition)
        {
            this.textureID = this.textureID = CreateTexture.LoadTexture("ammoPrize.png");
            this.centerPosition = centerPosition;
        }
    }
}
