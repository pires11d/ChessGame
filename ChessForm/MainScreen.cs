using Lib.Entities;
using Lib.Enums;
using Lib.Enums.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ChessForm
{
    public partial class MainScreen : Form
    {
        public MainScreen(Form menu)
        {
            InitializeComponent();
            Menu = menu;
        }

        public Color LightColor { get; set; } = Color.PaleGoldenrod;
        public Color DarkColor { get; set; } = Color.FromArgb(128, 64, 0);
        public Color SelectedLightColor { get; set; } = Color.LightGray;
        public Color SelectedDarkColor { get; set; } = Color.FromArgb(66, 66, 66);
        public PieceControl CurrentControl { get; set; }
        public Match Game { get; set; }
        public Form Menu { get; set; }
        public List<Label> CapturedLabels
        {
            get
            {
                var labels = new List<Label>();
                labels.AddRange(panel_Captured1.Controls.OfType<Label>().ToList());
                labels.AddRange(panel_Captured2.Controls.OfType<Label>().ToList());
                return labels;
            }
        }

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
                    control.BackColor = LightColor;

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
            Interact(sender);
        }

        private void Interact(object obj)
        {
            var label = (Label)obj;

            if (CurrentControl == null)
            {
                // SELECIONAR PEÇA
                if (label.Text != "")
                {
                    Select(label);
                }
            }
            else
            {
                // DESELECIONAR PEÇA
                if (label == CurrentControl.Label)
                {
                    Deselect(label);
                }
                // MOVER PEÇA
                else if (CurrentControl.IsValidTarget(label))
                {
                    var origin = PieceControl.GetPositionFromControlName(CurrentControl.Label.Name);
                    var destination = PieceControl.GetPositionFromControlName(label.Name);

                    try
                    {
                        var (x, y) = Game.Play(origin, destination);

                        Release(label);

                        Additional(x, y);

                        UpdateGameInfo();
                    }
                    catch (ApplicationException ex)
                    {
                        MessageBox.Show(ex.Message, "Alerta");

                        CurrentControl.Label.ForeColor = CurrentControl.ForeColor;

                        Deselect(label);

                        if (Game.IsOver)
                        {
                            Menu.Show();
                            this.Close();
                        }
                    }
                }
            }
        }

        private void Additional(Position source, Position destination)
        {
            if (source != null && destination != null)
            {
                if (source != destination)
                {
                    Label sourceLabel = GetLabelInBoard(source.Row, source.Column);
                    Label destinationLabel = GetLabelInBoard(destination.Row, destination.Column);

                    destinationLabel.Text = sourceLabel.Text;
                    destinationLabel.ForeColor = sourceLabel.ForeColor;
                    destinationLabel.Font = sourceLabel.Font;

                    sourceLabel.Text = "";
                }
                else
                {
                    Label changingLabel = GetLabelInBoard(source.Row, source.Column);
                    changingLabel.Text = PieceTypeEnum.Queen.GetDescription();
                    float maxFont = panel_ChessBoard.Controls.OfType<Label>().Max(x => x.Font.Size);
                    changingLabel.Font = new Font(changingLabel.Font.FontFamily, maxFont, FontStyle.Regular);
                }
            }
        }

        private void Release(Label label)
        {
            label.Text = CurrentControl.Text;
            label.ForeColor = CurrentControl.ForeColor;
            label.Font = CurrentControl.Font;

            HidePossibleMovements();

            CurrentControl.Label.Text = "";
            CurrentControl.Label.ForeColor = CurrentControl.ForeColor;
            CurrentControl = null;
        }

        private void Select(Label label)
        {
            var position = PieceControl.GetPositionFromControlName(label.Name);
            var selectedPiece = Game.Board.Piece(position);
            if (selectedPiece.CurrentColor == Game.CurrentPlayer.Color)
            {
                CurrentControl = new PieceControl(label.Name);
                CurrentControl.ForeColor = label.ForeColor;
                CurrentControl.Text = label.Text;
                CurrentControl.Font = label.Font;
                CurrentControl.Label = label;

                label.ForeColor = Color.Red;
            }

            ShowPossibleMovements();
        }

        private void Deselect(Label label)
        {
            label.ForeColor = CurrentControl.ForeColor;
            CurrentControl.Piece.Deselect();
            CurrentControl = null;

            HidePossibleMovements();
        }

        private void ShowPossibleMovements()
        {
            if (CurrentControl != null)
            {
                var possibleMoves = CurrentControl.Piece.PossibleMoves(Game.CurrentPlayer);
                for (int i = 0; i < Game.Board.Rows; i++)
                {
                    for (int j = 0; j < Game.Board.Columns; j++)
                    {
                        if (possibleMoves[i, j])
                            PaintLabel(i, j);
                    }
                }
            }
        }

        private void HidePossibleMovements()
        {
            for (int i = 0; i < Game.Board.Rows; i++)
            {
                for (int j = 0; j < Game.Board.Columns; j++)
                {
                    UnpaintLabel(i, j);
                }
            }
        }

        private void PaintLabel(int i, int j)
        {
            Label label = GetLabelInBoard(i, j);
            if (label.BackColor == DarkColor)
                label.BackColor = SelectedDarkColor;
            else if (label.BackColor == LightColor)
                label.BackColor = SelectedLightColor;
        }

        private void UnpaintLabel(int i, int j)
        {
            Label label = GetLabelInBoard(i, j);
            if (label.BackColor == SelectedDarkColor)
                label.BackColor = DarkColor;
            else if (label.BackColor == SelectedLightColor)
                label.BackColor = LightColor;
        }

        private Label GetLabelInBoard(int i, int j)
        {
            return panel_ChessBoard.Controls.OfType<Label>().FirstOrDefault(x => x.Name == $"c{i}{j}");
        }

        private void UpdateGameInfo()
        {
            tb_Info.Text = Game.CurrentPlayer.Color.GetDescription() + "s";
            tb_Info.ForeColor = Color.FromName(Game.CurrentPlayer.Color.ToString());
            panel_ChessBoard.BackColor = Color.FromName(Game.CurrentPlayer.Color.ToString());

            UpdateCapturedPieces();
            UpdateCheckStatus();
        }

        private void UpdateCapturedPieces()
        {
            foreach (Label lbl in CapturedLabels)
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
            Label labelValue = (Label)CapturedLabels.Find(x => x.Name == labelSymbol.Name.Replace("Symbol", "Value"));
            labelValue.Text = quantity + "x";
            labelValue.Visible = quantity > 0;
        }

        private void UpdateCheckStatus()
        {
            lbl_Check1.Visible = Game.Players[1].IsOnCheck;
            lbl_Check2.Visible = Game.Players[2].IsOnCheck;
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Game.IsOver)
                Application.Exit();
        }
    }
}
