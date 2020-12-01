using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lode4ITB
{
    public partial class Form1 : Form
    {
        public event Action PlayerSwitched;

        Player p1, p2;

        static Player currentPlayer;

        public Form1() {
            InitializeComponent();
            p1 = new Player() {
                sea = sea1,
                name = "Player 1",
                ships = Ship.CreateShips()
            };
            p2 = new Player() {
                sea = sea2,
                name = "Player 2",
                ships = Ship.CreateShips()
            };

            currentPlayer = p1;
        }

        public static Player GetCurrentPlayer() {
            return currentPlayer;
        }

        public void SwitchPlayer() {
            if (currentPlayer == p1)
                currentPlayer = p2;
            else
                currentPlayer = p1;

            PlayerSwitched?.Invoke();
        }
    }
}
