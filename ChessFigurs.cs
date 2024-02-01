using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppchess
{
    public enum PieceColor
    {
        White,
        Black
    }
    public enum PieceType
    {
        King=1,
        Queen,
        Rook,
        Knight, 
        Bishop
    }

    public abstract class ChessFigure
    {
        public abstract bool CanMove(string initialCoordinate, string destCoordinate);
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

        public override bool CanMove(string initialCoordinate, string destCoordinate)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';

            int rowDiff = Math.Abs(destRow - initialRow);
            int colDiff = Math.Abs(destCol - initialCol);

            return rowDiff <= 1 && colDiff <= 1;
        }
        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            throw new NotImplementedException();
        }

    }

    public class Rook : ChessFigure
    {
        public Rook()
        {
            Type = PieceType.Rook;
        }

        public override bool CanMove(string initialCoordinate, string destCoordinate)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';

            return initialRow == destRow || initialCol == destCol;
        }
        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int destCol = char.ToUpper(initialCoordinate[0]) - 'A';
            if(initialRow==destRow || initialCol == destCol)
            {
                int rowDirection = Math.Sign(destRow - initialRow);
                int colDirection = Math.Sign(destCol - initialCol);

                int currentRow = initialRow + rowDirection;
                int currentCol = initialCol + colDirection;
                while(currentRow!=destRow || currentCol!=destCol) 
                {
                    if (board[currentCol,currentRow]!=null) 
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

        public override bool CanMove(string initialCoordinate, string destCoordinate)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';

            int rowDiff = Math.Abs(destRow - initialRow);
            int colDiff = Math.Abs(destCol - initialCol);

            return rowDiff == colDiff;
       
        }

        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            throw new NotImplementedException();
        }
    }

    public class Queen : ChessFigure
    {
        public Queen()
        {
            Type = PieceType.Queen;
        }

        public override bool CanMove(string initialCoordinate, string destCoordinate)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';

            int rowDiff = Math.Abs(destRow - initialRow);
            int colDiff = Math.Abs(destCol - initialCol);

            return initialRow == destRow || initialCol == destCol || rowDiff == colDiff;

        }


        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());//4
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';//0
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());//3
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';//1

            int rowDiff = Math.Abs(destRow - initialRow);
            int colDiff = Math.Abs(destCol - initialCol);

            // Check if the Queen is moving along a row, column, or diagonal
            if (initialRow == destRow || initialCol == destCol || rowDiff == colDiff)
            {
                // Check if there are any figures in the path
                int rowDirection = Math.Sign(destRow - initialRow);//-1
                int colDirection = Math.Sign(destCol - initialCol);//1

                int currentRow = initialRow + rowDirection;//3
                int currentCol = initialCol + colDirection;//1

                while (currentRow != destRow || currentCol != destCol)
                {
                    if (board[currentRow, currentCol] != null)
                    {
                        // There is a figure in the path
                        return false;
                    }

                    currentRow += rowDirection;
                    currentCol += colDirection;
                }

                // No figures in the path
                return true;
            }

            // Queen is not moving along a valid path
            return false;
        }


    }

    public class Knight : ChessFigure
    {
        public Knight()
        {
            Type = PieceType.Knight;
        }

        public override bool CanMove(string initialCoordinate, string destCoordinate)
        {
            int initialRow = 8 - int.Parse(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';
            int destRow = 8 - int.Parse(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';

            int rowDiff = Math.Abs(destRow - initialRow);
            int colDiff = Math.Abs(destCol - initialCol);

            return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
        }

        public override bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            throw new NotImplementedException();
        }
    }









}
