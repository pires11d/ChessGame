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

        public Player(int type, PieceColorEnum color)
        {
            Type = type;
            Color = color;
            PiecesCaptured = new List<Piece>();
        }

        public void Capture(Piece currentPiece)
        {
            PiecesCaptured.Add(currentPiece);
        }
    }
}
