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
        public event Action<Sea> PlayerMissed;

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

        public void RevealShips() {
            foreach(var cell in cells) {
                cell.RevealCell();
            }
        }

        private void OnMouseClickFighting(object sender, MouseEventArgs e) {
            Cell mouse = (Cell) sender;
            mouse.RevealCell();
            if(mouse.CellActionType == CellActionType.Ship) {
                mouse.CellActionType = CellActionType.Hit;
                mouse.ProcessHit();
            } else if(mouse.CellActionType == CellActionType.None){
                mouse.CellActionType = CellActionType.Miss;
                PlayerMissed?.Invoke(this);
            }
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

            if (CheckBorders(ship, x, y) && CheckNeighbours(ship, x, y)) {  // dodělat podmínky pro placement

                for (int i = y; i < ship.Shape.GetLength(1) + y; i++) {
                    for (int j = x; j < ship.Shape.GetLength(0) + x; j++) {
                        if (i < 10 && j < 10) {
                            if (ship.Shape[j - x, i - y]) {
                                cells[j, i].Ship = ship;
                                ship.cells.Add(cells[j, i]);
                                cells[j, i].CellActionType = CellActionType.Ship;
                            }
                        }
                    }
                }
                ship.Place();
                foreach (var cell in cells) {
                    cell.Highlighted = false;
                }
            }
        }

        private bool CheckNeighbours(Ship ship, int x, int y) {

            bool allRight = true;
            for (int i = y; i < ship.Shape.GetLength(1) + y; i++) {
                for (int j = x; j < ship.Shape.GetLength(0) + x; j++) {
                    if (i < 10 && j < 10) {
                        if (ship.Shape[j - x, i - y]) {
                            if(!(!IsInSea(j+1, i) || cells[j+1, i].CellActionType != CellActionType.Ship)) {
                                allRight = false;
                            }
                        }
                    }
                }
            }
            return allRight;
        }

        private bool IsInSea(int x, int y) {
            return x < cells.GetLength(0) && x > 0 && y < cells.GetLength(1) && y > 0;
        }

        private bool CheckBorders(Ship ship, int x, int y) {
            if(x+ship.Shape.GetLength(0) <= cells.GetLength(0)) {
                if(y+ship.Shape.GetLength(1) <= cells.GetLength(1)) {
                    return true;
                }
            }
            return false;
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
