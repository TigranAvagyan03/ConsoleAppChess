using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppChess
{
    internal class ChessBoard
    {
        private ChessFigure[,] board;
        public ChessBoard()
        {
           board = new ChessFigure[8,8];
        }

        public void PlaceFigure(PieceType figurchoice, string initialcordinate, PieceColor color = PieceColor.White)
        {
            int row = 8 - int.Parse(initialcordinate[1].ToString());
            int col = char.ToUpper(initialcordinate[0]) - 'A';
        
           switch(figurchoice)
            {
                case PieceType.King:
                    King king = new King();
                    king.Color = color;
                    board[row, col] = king;
                    break;
                case PieceType.Rook:
                    Rook rook = new Rook();
                    rook.Color = color;
                    board[row, col] = rook;
                    break;
                case PieceType.Bishop:
                    Bishop bishop=new Bishop();
                    bishop.Color = color;
                    board[row, col]= bishop;
                    break;
                case PieceType.Queen:
                    Queen queen = new Queen();
                    queen.Color = color;
                    board[row, col] = queen;
                    break;
                case PieceType.Knight:
                    Knight knight = new Knight();
                    knight.Color = color;
                    board[row, col] = knight;
                    break;
            }
        }
        
        public void PrintBoard()
        {
            Console.WriteLine("  A  B  C  D  E  F  G  H ");
            for (int row = 0; row < 8; row++) 
            {
                Console.Write($"{8 - row}");
                for (int col = 0;col < 8; col++)
                {
                    ChessFigure figure = board[row, col];
                    ConsoleColor cellcolor = (row +col)%2==0?ConsoleColor.DarkYellow:ConsoleColor.DarkRed;
                    
                    Console.BackgroundColor=cellcolor;
                    
                    if (figure != null)
                    {
                        if (figure.Color == PieceColor.White)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor=ConsoleColor.Black;
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

        private string GetFigureSymbol(ChessFigure figure)
        {
            if (figure is King)
                return "К";
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

        public bool CanMove(string initialCoordinate, string destCoordinate)
        {
            int initialRow = 8 - Convert.ToInt32(initialCoordinate[1].ToString());
            int initialCol =  char.ToUpper(initialCoordinate[0])-'A';

            int destRow = 8 - Convert.ToInt32(destCoordinate[1].ToString());
            int destCol=  char.ToUpper(destCoordinate[0])-'A';
            ChessFigure selectedFigure = (ChessFigure)board[initialRow, initialCol];

            if (selectedFigure != null)
            {

                return selectedFigure.CanMove(initialCoordinate, destCoordinate);
            }

            return false;


        }
        public bool CanMove(string initialCoordinate, string destCoordinate, ChessFigure[,] board)
        {
            int initialRow = 8 - Convert.ToInt32(initialCoordinate[1].ToString());
            int initialCol = char.ToUpper(initialCoordinate[0])-'A';

            int destRow = 8 - Convert.ToInt32(destCoordinate[1].ToString());
            int destCol =  char.ToUpper(destCoordinate[0])-'A';
            ChessFigure selectedFigure = board[initialRow, initialCol];

            if (selectedFigure != null)
            {

                return selectedFigure.CanMove(initialCoordinate, destCoordinate, board);
            }

            return false;


        }

        public void PrintPossibleMovesForQueen(string initialCoordinate)
        {
            
            
            Console.WriteLine("  A  B  C  D  E  F  G  H ");
            for (int destrow = 0; destrow < 8; destrow++)
            {
                Console.Write($"{8 - destrow}");
                for(int destcol = 0;destcol < 8; destcol++) 
                {
                    ChessFigure chessFigure = board[destrow, destcol];
                    ConsoleColor cellcolor = (destrow+ destcol) % 2 == 0 ? ConsoleColor.DarkYellow : ConsoleColor.DarkRed;
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
                    else if (CanMove(initialCoordinate, $"{(char)('A' + destcol)}{8 - destrow}",board) )
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