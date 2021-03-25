using System.ComponentModel;

namespace Lib.Enums.Pieces
{
    public enum PieceTypeEnum
    {
        [Description("♟")]
        Pawn = 0,

        [Description("♜")]
        Rook = 1,

        [Description("♞")]
        Night = 2,

        [Description("♝")]
        Bishop = 3,

        [Description("♛")]
        Queen = 4,

        [Description("♚")]
        King = 5
    }
}
