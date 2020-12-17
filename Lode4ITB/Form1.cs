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
            sea1.PlayerMissed += OnPlayerMissed;
            sea2.PlayerMissed += OnPlayerMissed;

            p1.ships.ForEach(x=>x.ShipSinked += OnShipSinked);
            p2.ships.ForEach(x=>x.ShipSinked += OnShipSinked);

            currentPlayer = p1;
        }

        private void OnShipSinked(Ship ship) {
            if(GetTheOtherPlayer().ships.All(x=>x.IsSinked)) {
                CurrentPlayerWon();
            } 
        }

        private void CurrentPlayerWon() {

            sea1.RevealShips();
            sea2.RevealShips();

            DialogResult dr = MessageBox.Show("Vyhrál " + currentPlayer.name + "! Chcete spustit novou hru?",
                "Konec hry", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(dr == DialogResult.Yes) {
                Application.Restart();
            } else {
                Application.Exit();
            }
        }

        private void OnPlayerMissed(Sea sea) {
            SwitchPlayer();
        }

        private void OnTurnEnded(Player p) {
            p.sea.HideShips();

            if(currentGamePhase == GamePhase.ShipPlacement) {
                if(p1.allShipsPlaced && p2.allShipsPlaced) {
                    CurrentGamePhase = GamePhase.Fight;
                    sea1.ChangeGamePhase(CurrentGamePhase);
                    sea2.ChangeGamePhase(CurrentGamePhase);
                }
            }

            SwitchPlayer();
        }

        public static Player GetCurrentPlayer() {
            return currentPlayer;
        }

        private Player GetTheOtherPlayer() {
            return currentPlayer == p1 ? p2 : p1;
        }

        public void SwitchPlayer() {

            if(CurrentGamePhase == GamePhase.ShipPlacement) {
                currentPlayer.sea.Enabled = false;
            }
            else {
                currentPlayer.sea.Enabled = true;
            }
            
            if (currentPlayer == p1)
                currentPlayer = p2;
            else
                currentPlayer = p1;

            if(CurrentGamePhase == GamePhase.ShipPlacement) {
                currentPlayer.sea.Enabled = true;
            } else {
                currentPlayer.sea.Enabled = false;
            }

            PlayerSwitched?.Invoke();
        }
    }
}
