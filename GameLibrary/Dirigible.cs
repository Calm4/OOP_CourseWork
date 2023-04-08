using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{

    public class Dirigible
    {
        public int Health { get; set; } = 100;
        public int Armor { get; set; } = 100;

        public bool IsAlive(int health,int armor)
        {
            if (health <= 0 && armor <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
