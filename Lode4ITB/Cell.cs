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
        private CellActionType cellActionType;
        public CellActionType CellActionType {
            get { return cellActionType; }
            set {
                cellActionType = value;
                BackColor = colors[cellActionType];
            }
        }

        static Dictionary<CellActionType, Color> colors = new Dictionary<CellActionType, Color>();

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
    }
}
