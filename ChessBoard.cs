using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleAppChess
{
    public class Chessboard
    {
        public ChessFigure[,] Board { get; }

        public Chessboard()
        {
            Board = new ChessFigure[8, 8];
        }

        public void PlaceFigure(PieceType figureChoice, string initialCoordinate, PieceColor color = PieceColor.White)
        {
            int row = 8 - int.Parse(initialCoordinate[1].ToString());
            int col = char.ToUpper(initialCoordinate[0]) - 'A';

            switch (figureChoice)
            {
                case PieceType.King:
                    King king = new King();
                    king.Color = color;
                    Board[row, col] = king;
                    break;
                case PieceType.Rook:
                    Rook rook = new Rook();
                    rook.Color = color;
                    Board[row, col] = rook;
                    break;
                case PieceType.Bishop:
                    Bishop bishop = new Bishop();
                    bishop.Color = color;
                    Board[row, col] = bishop;
                    break;
                case PieceType.Queen:
                    Queen queen = new Queen();
                    queen.Color = color;
                    Board[row, col] = queen;
                    break;
                case PieceType.Knight:
                    Knight knight = new Knight();
                    knight.Color = color;
                    Board[row, col] = knight;
                    break;
            }
        }
        public void CopyBoard(Chessboard original)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (original.Board[row, col] != null)
                    {
                        ChessFigure originalPiece = original.Board[row, col];
                        ChessFigure copiedPiece = null;

                        if (originalPiece is King)
                        {
                            copiedPiece = new King();
                        }
                        else if (originalPiece is Rook)
                        {
                            copiedPiece = new Rook();
                        }
                        else if (originalPiece is Bishop)
                        {
                            copiedPiece = new Bishop();
                        }
                        else if (originalPiece is Queen)
                        {
                            copiedPiece = new Queen();
                        }
                        else if (originalPiece is Knight)
                        {
                            copiedPiece = new Knight();
                        }

                        if (copiedPiece != null)
                            copiedPiece.Color = originalPiece.Color;

                        Board[row, col] = copiedPiece;
                    }
                    else
                    {
                        Board[row, col] = null;
                    }
                }
            }
        }

        public void PrintChessboard()
        {
            Console.WriteLine("   A  B  C  D  E  F  G  H ");
            for (int row = 0; row < 8; row++)
            {
                Console.Write($"{8 - row} ");
                for (int col = 0; col < 8; col++)
                {
                    ChessFigure figure = Board[row, col];
                    ConsoleColor cellColor = (row + col) % 2 == 0 ? ConsoleColor.DarkYellow : ConsoleColor.DarkRed;

                    Console.BackgroundColor = cellColor;

                    if (figure != null)
                    {
                        if (figure.Color == PieceColor.White)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Console.Write($" {GetFigureSymbol(figure)} ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }

                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }



        public void MovePiece(string initialPosition, string finalPosition)
        {
            int initialRow = 8 - int.Parse(initialPosition[1].ToString());
            int initialCol = char.ToUpper(initialPosition[0]) - 'A';
            int finalRow = 8 - int.Parse(finalPosition[1].ToString());
            int finalCol = char.ToUpper(finalPosition[0]) - 'A';

            ChessFigure piece = Board[initialRow, initialCol];
            Board[initialRow, initialCol] = null;
            Board[finalRow, finalCol] = piece;
        }




        private string GetFigureSymbol(ChessFigure figure)
        {
            if (figure is King)
                return "K";
            else if (figure is Rook)
                return "R";
            else if (figure is Bishop)
                return "B";
            else if (figure is Queen)
                return "Q";
            else if (figure is Knight)
                return "N";
            else
                return " ";
        }

        
        public bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            int initialRow = 8 - Convert.ToInt32(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0]) - 'A';

            int destRow = 8 - Convert.ToInt32(destCoordinate[1].ToString());
            int destCol = char.ToUpper(destCoordinate[0]) - 'A';
            ChessFigure selectedFigure = Board[initialRow, initialCol];

            if (selectedFigure != null)
            {

                return selectedFigure.CanMove(initialCoordinate, destCoordinate, Board);
            }

            return false;


        }

        public ChessFigure GetFigureAtPosition(string position)
        {
            int col = char.ToUpper(position[0]) - 'A';
            int row = 8 - int.Parse(position[1].ToString());

            return Board[row, col];
        }


        public List<string> FindAllPiecePositions(PieceColor pieceColor, Chessboard chessboard)
        {
            List<string> positions = new List<string>();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    ChessFigure currentPiece = chessboard.Board[row, col];

                    if (currentPiece != null && currentPiece.Color == pieceColor)
                    {
                        positions.Add($"{(char)('A' + col)}{8 - row}");
                    }
                }
            }

            return positions;
        }

        public string FindPiecePosition(PieceType pieceType, PieceColor pieceColor, Chessboard chessboard)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    ChessFigure currentPiece = chessboard.Board[row, col];

                    if (currentPiece != null && currentPiece.Type == pieceType && currentPiece.Color == pieceColor)
                    {
                        return $"{(char)('A' + col)}{8 - row}";
                    }
                }
            }

            return null;
        }

        public bool IsWhiteKingOnTopRows(string whiteKingPosition)
        {


            if (whiteKingPosition != null)
            {
                int row = int.Parse(whiteKingPosition[1].ToString());


                if (row >= 0 && row <= 3)
                {
                    return true;
                }
            }

            return false;
        }

        public void PrintPossibleMovesForFigures(string initialCoordinate)
        {

            Console.WriteLine("  A  B  C  D  E  F  G  H ");
            for (int destrow = 0; destrow < 8; destrow++)
            {
                Console.Write($"{8 - destrow}");
                for (int destcol = 0; destcol < 8; destcol++)
                {
                    ChessFigure chessFigure = Board[destrow, destcol];
                    ConsoleColor cellcolor = (destrow + destcol) % 2 == 0 ? ConsoleColor.DarkYellow : ConsoleColor.DarkRed;
                    Console.BackgroundColor = cellcolor;
                    if (chessFigure != null)
                    {
                        if (chessFigure.Color == PieceColor.White)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                        }

                        Console.Write($" {GetFigureSymbol(chessFigure)} ");


                    }
                    else if (CanMove(initialCoordinate, $"{(char)('A' + destcol)}{8 - destrow}", Board))
                    {
                        ConsoleColor originalColor = Console.BackgroundColor;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("   ");
                    }

                    else
                    {
                        Console.Write("   ");
                    }

                    Console.ResetColor();


                }

                Console.WriteLine();

            }


        }
    }

}
