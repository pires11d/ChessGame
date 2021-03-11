using System.ComponentModel;

namespace Lib.Enums.Pieces
{
    public enum PieceColorEnum
    {
        [Description("Preta")]
        Black = 0,

        [Description("Cinza")]
        Gray = 7,

        [Description("Cinza escuro")]
        DarkGray = 8,

        [Description("Azul")]
        Blue = 9,

        [Description("Verde")]
        Green = 10,

        [Description("Azul claro")]
        Cyan = 11,

        [Description("Vermelha")]
        Red = 12,

        [Description("Rosa")]
        Magenta = 13,

        [Description("Amarela")]
        Yellow = 14,

        [Description("Branca")]
        White = 15
    }
}
