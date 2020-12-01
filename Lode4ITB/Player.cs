using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lode4ITB
{
    public class Player
    {
        public string name;
        public List<Ship> ships = new List<Ship>();
        public Sea sea;

        public Ship CurrentShip {
            get { return ships[currentShip]; }
        }

        // placement
        public int currentShip = 0;

    }
}
