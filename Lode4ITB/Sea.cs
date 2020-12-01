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
            for (int i = 0; i < cells.GetLength(0); i++) {
                for (int j = 0; j < cells.GetLength(1); j++) {
                    Cell cell = new Cell(i, j);
                    cells[i, j] = cell;
                    cell.MouseEnter += OnMouseEnter;
                    cell.MouseLeave += OnMouseLeave;
                    cell.MouseClick += OnMouseClick;
                    Controls.Add(cell);
                }
            }
            Refresh();
        }

        private void OnMouseClick(object sender, MouseEventArgs e) {
            Cell mouse = (Cell) sender;
            int x = mouse.X;
            int y = mouse.Y;
            Ship ship = Form1.GetCurrentPlayer().CurrentShip;

            for (int i = y; i < ship.Shape.GetLength(1) + y; i++) {
                for (int j = x; j < ship.Shape.GetLength(0) + x; j++) {
                    if (i < 10 && j < 10) {
                        if (ship.Shape[j - x, i - y])
                            cells[j, i].CellActionType = CellActionType.Ship;
                    }
                }
            }

            if (true) {  // dodělat podmínky pro placement
                ship.Place(); // Dodělat přepnutí typu lodi na další :) :) 
                
            }
        }

        private void OnMouseEnter(object sender, EventArgs e) {
            Cell mouse = (Cell) sender;
            int x = mouse.X;
            int y = mouse.Y;
            Ship ship = Form1.GetCurrentPlayer().CurrentShip;

            for (int i = y; i < ship.Shape.GetLength(1) + y; i++) {
                for (int j = x; j < ship.Shape.GetLength(0) + x; j++) {
                    if (i < 10 && j < 10) {
                        if (ship.Shape[j - x, i - y])
                            cells[j, i].Highlighted = true;
                    }
                }
            }
            //mouse.CellActionType = CellActionType.HighlightedForPlacement;
        }

        private void OnMouseLeave(object sender, EventArgs e) {
            Cell mouse = (Cell) sender;
            int x = mouse.X;
            int y = mouse.Y;
            Ship ship = Form1.GetCurrentPlayer().CurrentShip;

            for (int i = y; i < ship.Shape.GetLength(1) + y; i++) {
                for (int j = x; j < ship.Shape.GetLength(0) + x; j++) {
                    if (i < 10 && j < 10) {
                        cells[j, i].Highlighted = false;
                    }
                }
            }
        }

    }
}
