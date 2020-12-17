using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lode4ITB
{
    public class Ship
    {
        public event Action Placed;
        public event Action<Ship> ShipSinked;

        private bool[,] shape;
        public bool[,] Shape => shape;

        private Point position;

        public List<Cell> cells;

        private bool sinked = false;
        public bool IsSinked { get { return sinked; } }

        public Ship(bool[,] shape) {
            this.shape = shape;
            position = new Point();
            cells = new List<Cell>();
        }

        public void CellHit(Cell cell) {
            if(cells.All(x=>x.CellActionType == CellActionType.Hit)) {
                cells.ForEach(x=>x.CellActionType = CellActionType.SinkedShip);
                sinked = true;
                ShipSinked?.Invoke(this);
            }
        }

        public static List<Ship> CreateShips() {
            return new List<Ship>() {
                    new Ship(new bool[,] {
                        { true, true, true, true, true, true },
                        { false, false, true, true, false, false}
                    }),
                    new Ship(new bool[,] {
                        { false, false, true, false, false },
                        { true, true, true, true, true }
                    }),
                    new Ship(new bool[,] {
                        { false, true, false},
                        { true, true, true},
                        { false, true, false}
                    }),
                    new Ship(new bool[,] {
                        { true, true, true}
                    }),
                    new Ship(new bool[,] {
                        { true, true}
                    })
                };
        }

        public void Place() {
            //MessageBox.Show("Loď umístěna!");
            Placed?.Invoke();
        }
    }
}
