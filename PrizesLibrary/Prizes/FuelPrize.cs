using GameLibrary;
using OpenTK;

namespace PrizesLibrary.Prizes
{
    public class FuelPrize : Prize
    {
        public FuelPrize(Vector2 centerPosition)
        {
            this.textureID = this.textureID = CreateTexture.LoadTexture("fuelPrize.png");
            this.centerPosition = centerPosition;
        }
    }
}
