using GameLibrary.Dirigible;
using System;


namespace GameLibrary.DirigibleDecorators
{
    /// <summary>
    /// Декоратор на изменение скорости передвижения
    /// </summary>
    public class SpeedBoostDecorator : DirigibleDecorator
    {
        private float _extraSpeed;
        private const float _maxSpeed = 0.02f;
        /// <summary>
        /// Конструктор класса SpeedBoostDecorator
        /// </summary>
        /// <param name="dirigible">Базовый объект дирижабля</param>
        /// <param name="extraSpeed">Дополнителные пули</param>
        public SpeedBoostDecorator(AbstractDirigible dirigible, float extraSpeed) : base(dirigible)
        {
            _extraSpeed = extraSpeed;
         
        }

        /// <summary>
        /// Скорость передвижения
        /// </summary>
        public override float Speed
        {
            get { return Math.Min(_dirigible.Speed + _extraSpeed, _maxSpeed); }
            set { _dirigible.Speed = value; }
        }
    }
}
