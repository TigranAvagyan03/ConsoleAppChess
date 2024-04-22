using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppChess
{
    public enum PieceColor
    {
        White,
        Black
    }

    public enum PieceType
    {
        King = 1,
        Rook,
        Bishop,
        Queen,
        Knight
    }

    public abstract class ChessFigure
    {
        
        public abstract bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board);
        public PieceColor Color { get; set; }
        public PieceType Type { get; set; }
    }
    public class King : ChessFigure
    {
        public King()
        {
            Type = PieceType.King;
        }

        
        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';

            int rowDiff = Math.Abs(destRow - initialRow);
            int colDiff = Math.Abs(destCol - initialCol);


            if (rowDiff <= 1 && colDiff <= 1)
            {

                if (board[destRow, destCol] != null)
                {
                    return false;
                }


                return true;
            }


            return false;
        }
        
    }

    public class Rook : ChessFigure
    {
        public Rook()
        {
            Type = PieceType.Rook;
        }

        
        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';




            if (initialRow == destRow || initialCol == destCol)
            {

                int rowDirection = Math.Sign(destRow - initialRow);
                int colDirection = Math.Sign(destCol - initialCol);


                int currentRow = initialRow + rowDirection;
                int currentCol = initialCol + colDirection;


                while (currentRow != destRow || currentCol != destCol)
                {
                    if (board[currentRow, currentCol] != null)
                    {

                        return false;
                    }

                    currentRow += rowDirection;
                    currentCol += colDirection;
                }


                return true;
            }


            return false;
        }
    }

    public class Bishop : ChessFigure
    {
        public Bishop()
        {
            Type = PieceType.Bishop;
        }

        
        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';

            int rowDiff = Math.Abs(destRow - initialRow);
            int colDiff = Math.Abs(destCol - initialCol);


            if (rowDiff == colDiff)
            {

                int rowDirection = Math.Sign(destRow - initialRow);
                int colDirection = Math.Sign(destCol - initialCol);


                int currentRow = initialRow + rowDirection;
                int currentCol = initialCol + colDirection;


                while (currentRow != destRow || currentCol != destCol)
                {
                    if (board[currentRow, currentCol] != null)
                    {

                        return false;
                    }

                    currentRow += rowDirection;
                    currentCol += colDirection;
                }


                return true;
            }


            return false;
        }
    }

    public class Queen : ChessFigure
    {
        public Queen()
        {
            Type = PieceType.Queen;
        }

       
        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';

            int rowDiff = Math.Abs(destRow - initialRow);
            int colDiff = Math.Abs(destCol - initialCol);


            if (initialRow == destRow || initialCol == destCol || rowDiff == colDiff)
            {

                int rowDirection = Math.Sign(destRow - initialRow);
                int colDirection = Math.Sign(destCol - initialCol);

                int currentRow = initialRow + rowDirection;
                int currentCol = initialCol + colDirection;

                while (currentRow != destRow || currentCol != destCol)
                {
                    if (board[currentRow, currentCol] != null)
                    {

                        return false;
                    }

                    currentRow += rowDirection;
                    currentCol += colDirection;
                }


                return true;
            }


            return false;
        }
    }

    public class Knight : ChessFigure
    {
        public Knight()
        {
            Type = PieceType.Knight;
        }

        
        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';

            int rowDiff = Math.Abs(destRow - initialRow);
            int colDiff = Math.Abs(destCol - initialCol);


            if ((rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2))
            {

                if (board[destRow, destCol] != null)
                {
                    return false;
                }


                return true;
            }


            return false;
        }
    }
}
