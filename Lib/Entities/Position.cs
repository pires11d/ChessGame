using System;

namespace Lib.Entities
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int RowNumber { get; set; }
        public char ColumnLetter { get; set; }
        public static int MaxRows { get; set; }
        public static int MaxColumns { get; set; }

        public Position()
        {
        }

        /// <summary>
        /// Constructor used for when the position coordinates are expressed in the Column-Row format.
        /// <para></para>
        /// e.g.: "A2" => Column A, Row 2
        /// </summary>
        /// <param name="columnRow">Column letter concatenated with the Row number</param>
        public Position(string columnRow)
        {
            ColumnLetter = columnRow.ToUpper()[0];
            Column = ColumnLetter - 'A';
            RowNumber = int.Parse(columnRow[1] + "");
            Row = MaxRows - RowNumber;
        }

        /// <summary>
        /// Constructor used for when the position coordinates are expressed in the Row-Column format.
        /// <para></para>
        /// e.g.: (1,2) => Row 1, Column 2
        /// </summary>
        /// <param name="row">Row Index (0-based)</param>
        /// <param name="column">Column Index (0-based)</param>
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public void Validate()
        {
            if (!IsValid())
                throw new ApplicationException("Posição Inválida!");
        }

        public bool IsValid()
        {
            return !(Row < 0 ||
                    Column < 0 ||
                    Row >= MaxRows ||
                    Column >= MaxColumns);
        }

        public Position Top
        {
             get { return new Position(Row - 1, Column); }
        }

        public Position TopLeft
        {
             get { return new Position(Row - 1, Column - 1); }
        }

        public Position Left
        {
             get { return new Position(Row, Column - 1); }
        }

        public Position BottomLeft
        {
             get { return new Position(Row + 1, Column - 1); }
        }

        public Position Bottom
        {
             get { return new Position(Row + 1, Column); }
        }

        public Position BottomRight
        {
             get { return new Position(Row + 1, Column + 1); }
        }

        public Position Right
        {
            get { return new Position(Row, Column + 1); }
        }

        public Position TopRight
        {
            get { return new Position(Row - 1, Column + 1); }
        }

        public override string ToString()
        {
            return $"({Row},{Column})";
        }
    }
}
