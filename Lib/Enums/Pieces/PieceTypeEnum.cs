using System.ComponentModel;

namespace Lib.Enums.Pieces
{
    public enum PieceTypeEnum
    {
        [Description("Peão")]
        Pawn = 0,

        [Description("Torre")]
        Rook = 1,

        [Description("Cavalo")]
        Horse = 2,

        [Description("Bispo")]
        Bishop = 3,

        [Description("Rainha")]
        Queen = 4,

        [Description("Rei")]
        King = 5
    }
}
