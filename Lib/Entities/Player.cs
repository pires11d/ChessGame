using Lib.Entities.Pieces;
using Lib.Enums.Pieces;
using System;
using System.Collections.Generic;

namespace Lib.Entities
{
    public class Player
    {
        public int Type { get; set; }
        public string Name { get; set; }
        public PieceColorEnum Color { get; set; }
        public List<Piece> PiecesCaptured { get; set; }
        public bool IsOnCheck { get; set; }

        public Player(int type, PieceColorEnum color)
        {
            Type = type;
            Color = color;
            PiecesCaptured = new List<Piece>();
        }

        public void Capture(Piece pieceToCapture)
        {
            PiecesCaptured.Add(pieceToCapture);
        }

        public void UnCapture(Piece pieceCaptured)
        {
            PiecesCaptured.Remove(pieceCaptured);
        }

        public override string ToString()
        {
            return Color.ToString();
        }
    }
}
