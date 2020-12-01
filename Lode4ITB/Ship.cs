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

        private bool[,] shape;
        public bool[,] Shape => shape;

        private Point position;

        public Ship(bool[,] shape) {
            this.shape = shape;
            position = new Point();
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
            MessageBox.Show("Loď umístěna!");
            Placed?.Invoke();
        }
    }
}
