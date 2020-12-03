using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lode4ITB
{
    public class Player
    {
        public event Action<Player> TurnEnded;

        public string name;
        public List<Ship> ships = new List<Ship>();
        public Sea sea;

        public bool allShipsPlaced = false;

        public Player(List<Ship> ships) {
            this.ships = ships;
            ships.ForEach(x => x.Placed += OnShipPlaced);
        }

        private void OnShipPlaced() {
            if (currentShip == ships.Count - 1) {
                allShipsPlaced = true;
                TurnEnded?.Invoke(this);
            } else {
                currentShip++;
            }
        }

        public Ship CurrentShip {
            get { return ships[currentShip]; }
        }

        // placement
        public int currentShip = 0;

    }
}
