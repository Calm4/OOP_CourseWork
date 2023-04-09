using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Dirigible
{
    public class BasicDirigible : AbstractDirigible
    {
        public int Health { get; set; } = 100;
        public int Armor { get; set; } = 100;
        public float Speed { get; set; } = 100;
        public int Ammo { get; set; } = 30;

        public override void Controls()
        {
            throw new NotImplementedException();
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
            return Speed;
        }

        public override bool IsAlive()
        {
            return Health > 0 && Armor > 0;
        }
    }
}
