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

        GamePhase currentGamePhase;

        public Sea() {
            InitializeComponent();
            currentGamePhase = GamePhase.ShipPlacement;
            for (int i = 0; i < cells.GetLength(0); i++) {
                for (int j = 0; j < cells.GetLength(1); j++) {
                    Cell cell = new Cell(i, j);
                    cells[i, j] = cell;
                    cell.MouseEnter += OnMouseEnterPositioning;
                    cell.MouseLeave += OnMouseLeavePositioning;
                    cell.MouseClick += OnMouseClickPositioning;
                    Controls.Add(cell);
                }
            }
            Refresh();
        }

        public void ChangeGamePhase(GamePhase newPhase) {
            currentGamePhase = newPhase;
            foreach (var cell in cells) {
                cell.MouseEnter -= OnMouseEnterPositioning;
                cell.MouseLeave -= OnMouseLeavePositioning;
                cell.MouseClick -= OnMouseClickPositioning;

                cell.MouseEnter += OnMouseEnterFighting;
                cell.MouseLeave += OnMouseLeaveFighting;
                cell.MouseClick += OnMouseClickFighting;
            }
        }

        public void HideShips() {
            foreach (var cell in cells) {
                cell.HideCell();
            }
        }

        private void OnMouseClickFighting(object sender, MouseEventArgs e) {
        }
        private void OnMouseEnterFighting(object sender, EventArgs e) {
            Cell mouse = (Cell) sender;
            int x = mouse.X;
            int y = mouse.Y;
            mouse.Highlighted = true;
        }
        private void OnMouseLeaveFighting(object sender, EventArgs e) {
            Cell mouse = (Cell) sender;
            int x = mouse.X;
            int y = mouse.Y;
            mouse.Highlighted = false;
        }

        private void OnMouseClickPositioning(object sender, MouseEventArgs e) {
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


                foreach (var cell in cells) {
                    cell.Highlighted = false;
                }
            }
        }

        private void OnMouseEnterPositioning(object sender, EventArgs e) {
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

        private void OnMouseLeavePositioning(object sender, EventArgs e) {
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
