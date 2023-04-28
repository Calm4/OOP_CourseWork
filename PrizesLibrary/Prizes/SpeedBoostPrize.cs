using GameLibrary;
using OpenTK;

namespace PrizesLibrary.Prizes
{
    public class SpeedBoostPrize : Prize
    {
        public SpeedBoostPrize(Vector2 centerPosition)
        {
            this.textureID = CreateTexture.LoadTexture("speedPrize.png"); ;
            this.centerPosition = centerPosition;
        }

    }
}
