using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lode4ITB
{
    public partial class Sea : UserControl
    {
        Cell[,] cells = new Cell[10, 10];


        public Sea() {
            InitializeComponent();
            for(int i = 0; i < cells.GetLength(0); i++) {
                for (int j = 0; j < cells.GetLength(1); j++) {
                    Cell cell = new Cell(i, j);
                    cells[i, j] = cell;
                    cell.MouseEnter += OnMouseEnter;
                    cell.MouseLeave += OnMouseLeave;
                    Controls.Add(cell);
                }
            }
            Refresh();
        }
        private void OnMouseEnter(object sender, EventArgs e) {
            Cell mouse = (Cell) sender;
            int x = mouse.X;
            int y = mouse.Y;
            Ship ship = Form1.GetCurrentPlayer().CurrentShip;
            
        }

        private void OnMouseLeave(object sender, EventArgs e) {

        }

    }
}
