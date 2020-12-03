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
    public partial class Cell : UserControl
    {
        int x, y;
        private CellActionType cellActionType = CellActionType.None;
        public CellActionType CellActionType {
            get { return cellActionType; }
            set {
                cellActionType = value;
                
                if(highlighted) {
                    BackColor = Color.LightBlue;
                } else {
                    if(Hidden) {
                        BackColor = colors[CellActionType.None];
                    } else {
                        BackColor = colors[cellActionType];
                    }
                }
            }
        }

        private bool highlighted = false;
        public bool Highlighted {
            get { return highlighted; }
            set {
                highlighted = value;
                CellActionType = cellActionType;
            }
        }

        private bool hidden = false;
        public bool Hidden {
            get { return hidden; }
            private set {
                hidden = value;
                CellActionType = cellActionType;
            }
        }

        static Dictionary<CellActionType, Color> colors = new Dictionary<CellActionType, Color>() {
            { CellActionType.None, Color.Blue },
            { CellActionType.Ship, Color.DarkGray }
        };

        public int X => x;
        public int Y => y;

        public Cell() {
            InitializeComponent();
        }

        public Cell(int x, int y) {
            
            InitializeComponent();
            this.x = x;
            this.y = y;
            this.Location = new Point(x * Width, y * Height);
        }

        public void HideCell() {
            Hidden = true;
        }
    }
}
