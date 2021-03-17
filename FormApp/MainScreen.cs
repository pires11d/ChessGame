using Lib.Entities;
using Lib.Enums;
using Lib.Enums.Pieces;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FormApp
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        public PieceControl CurrentControl { get; set; }
        public Match Game { get; set; }

        private void MainScreen_Load(object sender, System.EventArgs e)
        {
            SetControlStyle(Controls);

            PlayChess();
        }

        private void SetControlStyle(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.BackColor == Color.FromArgb(255, 192, 128))
                    control.BackColor = Color.Khaki;
                //control.BackColor = Color.FromArgb(240, 160, 100);
                else if (control.BackColor == Color.FromArgb(128, 64, 0))
                    control.BackColor = Color.FromArgb(120, 80, 40);

                if (control.ForeColor == Color.DarkOrange)
                    control.ForeColor = Color.White;

                if (control.Controls.Count > 0)
                    SetControlStyle(control.Controls);
            }
        }

        private void PlayChess()
        {
            Game = new Match(PieceColorEnum.Black, PieceColorEnum.White);
            PieceControl.Game = Game;

            UpdateGameInfo();
        }

        private void c00_Click(object sender, EventArgs e)
        {
            CallByName(sender);
        }

        private void CallByName(object obj)
        {
            var label = (Label)obj;

            if (CurrentControl == null)
            {
                // SELECIONAR PEÇA
                if (label.Text != "")
                {
                    var position = PieceControl.GetPositionFromControlName(label.Name);
                    var selectedPiece = Game.Board.Piece(position);
                    if (selectedPiece.Color == Game.CurrentPlayer.Color)
                    {
                        CurrentControl = new PieceControl(label.Name);
                        CurrentControl.ForeColor = label.ForeColor;
                        CurrentControl.Text = label.Text;
                        CurrentControl.Font = label.Font;
                        CurrentControl.Label = label;

                        label.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                // DESELECIONAR PEÇA
                if (label == CurrentControl.Label)
                {
                    label.ForeColor = CurrentControl.ForeColor;
                    CurrentControl.Piece.Deselect();
                    CurrentControl = null;
                }
                // MOVER PEÇA
                else if (CurrentControl.IsValidTarget(label))
                {
                    label.Text = CurrentControl.Text;
                    label.ForeColor = CurrentControl.ForeColor;
                    label.Font = CurrentControl.Font;

                    var origin = PieceControl.GetPositionFromControlName(CurrentControl.Label.Name);
                    var destination = PieceControl.GetPositionFromControlName(label.Name);

                    Game.MovePiece(origin, destination);

                    CurrentControl.Label.Text = "";
                    CurrentControl.Label.ForeColor = CurrentControl.ForeColor;
                    CurrentControl = null;

                    UpdateGameInfo();
                }
            }
        }

        private void UpdateGameInfo()
        {
            tb_Info.Text = Game.CurrentPlayer.Color.GetDescription() + "s";
            tb_Info.ForeColor = Color.FromName(Game.CurrentPlayer.Color.ToString());
            //panel_ChessBoard.BackColor = Color.FromName(Game.CurrentPlayer.Color.ToString());
            
            UpdateCapturedPieces();
        }

        private void UpdateCapturedPieces()
        {
            foreach (Label lbl in panel_Captured.Controls)
            {
                var playerIndex = int.Parse(lbl.Name.LastOrDefault().ToString());
                var piecesCaptured = Game.Players[playerIndex].PiecesCaptured;

                var types = EnumHelper.GetEnumsByNamespace("Lib.Enums.Pieces")["PieceTypeEnum"];
                var pieceType = types.FirstOrDefault(x => x.Description == lbl.Text);

                if (pieceType != null)
                {
                    var piecesCapturedByType = piecesCaptured.Where(x => x.Type.GetDescription() == pieceType.Description).Count();
                    ChangeLabel(lbl, piecesCapturedByType);
                };
            }
        }

        private void ChangeLabel(Label labelSymbol, int quantity)
        {
            Label labelValue = (Label)panel_Captured.Controls.Find(labelSymbol.Name.Replace("Symbol", "Value"), false).FirstOrDefault();
            labelValue.Text = quantity + "x";
            labelValue.Visible = quantity > 0;
            //labelSymbol.Visible = quantity > 0;
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
