using GameLibrary.Dirigible;

namespace GameLibrary.DirigibleDecorators
{
    public class AmmoBoostDecorator : DirigibleDecorator
    {
        private int _extraAmmo;
        private const int _maxAmmo = 30;
        public AmmoBoostDecorator(AbstractDirigible dirigible, int extraAmmo) : base(dirigible)
        {
            _extraAmmo = extraAmmo;
            if (_dirigible.Ammo <= _maxAmmo)
            {
                if (_dirigible.Ammo <= _maxAmmo - _extraAmmo)
                {
                    _dirigible.Ammo += _extraAmmo;
                }
                else
                {
                    _dirigible.Ammo = _maxAmmo;
                }
            }
            else
            {
                _dirigible.Ammo = _maxAmmo;
            }

        }

        public override int Ammo
        {
            get { return _dirigible.Ammo; }
            set { _dirigible.Ammo = value; }
        }
    }
}
