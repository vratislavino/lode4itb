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
        private GamePhase currentGamePhase = GamePhase.ShipPlacement;
        public GamePhase CurrentGamePhase {
            get { return currentGamePhase; }
            set {
                currentGamePhase = value;
            }
        }

        Player p1, p2;

        static Player currentPlayer;

        public Form1() {
            InitializeComponent();
            p1 = new Player(Ship.CreateShips()) {
                sea = sea1,
                name = "Player 1"
            };
            p2 = new Player(Ship.CreateShips()) {
                sea = sea2,
                name = "Player 2"
            };
            p1.TurnEnded += OnTurnEnded;
            p2.TurnEnded += OnTurnEnded;
            currentPlayer = p1;
        }

        private void OnTurnEnded(Player p) {
            p.sea.HideShips();

            SwitchPlayer();

            p.sea.Enabled = CurrentGamePhase != GamePhase.ShipPlacement;

            if(currentGamePhase == GamePhase.ShipPlacement) {
                if(p1.allShipsPlaced && p2.allShipsPlaced) {
                    CurrentGamePhase = GamePhase.Fight;
                    sea1.ChangeGamePhase(CurrentGamePhase);
                    sea2.ChangeGamePhase(CurrentGamePhase);
                }
            }
        }

        public static Player GetCurrentPlayer() {
            return currentPlayer;
        }

        public void SwitchPlayer() {
            if (currentPlayer == p1)
                currentPlayer = p2;
            else
                currentPlayer = p1;
            currentPlayer.sea.Enabled = CurrentGamePhase == GamePhase.ShipPlacement;
            PlayerSwitched?.Invoke();
        }
    }
}
