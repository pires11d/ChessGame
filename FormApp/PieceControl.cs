using Lib.Entities;
using Lib.Entities.Pieces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormApp
{
    public class PieceControl
    {
        public Label Label { get; set; }
        public Color ForeColor { get; set; }
        public string Text { get; set; }
        public Font Font { get; set; }
        public Piece Piece { get; set; }
        public static Match Game { get; set; }
        public Color DefaultColor { get; internal set; }

        public PieceControl(string controlName)
        {
            var position = GetPositionFromControlName(controlName);
            Piece = Game.Board.Piece(position);
            if (Piece != null)
                Game.Board.SelectPiece(Piece);
        }

        public static Position GetPositionFromControlName(string controlName)
        {
            var row = int.Parse(controlName.Substring(1, 1));
            var column = int.Parse(controlName.Substring(2, 1));
            return new Position(row, column);
        }

        public bool IsValidTarget(Label label)
        {
            var target = GetPositionFromControlName(label.Name);
            return Piece.IsPossibleMove(Game.CurrentPlayer,target);
        }
    }
}