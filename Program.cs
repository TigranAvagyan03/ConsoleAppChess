using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace ConsoleAppChess
{
    class ChessboardGame
    {
        static void Main()
        {

            Console.WriteLine(" Choice a game you want to play ");
            Console.WriteLine("1. Gess can move");
            Console.WriteLine("2. Simple chess");
            Console.WriteLine("3. Print Move Lines");
            Console.Write("Game choise:");
            int gameChoise = int.Parse(Console.ReadLine());
            switch (gameChoise)
            {
                case 1:
                    GameChoice1();
                    break;
                case 2:
                    GameChoice2();
                    break;
                case 3:
                    GameChoice3();
                    break;
            }


        }

          static void GameChoice1()
          {
            Console.WriteLine("Choose a figure to place on the chessboard:");
            Console.WriteLine("1. King");
            Console.WriteLine("2. Rook");
            Console.WriteLine("3. Bishop");
            Console.WriteLine("4. Queen");
            Console.WriteLine("5. Knight");


            Console.Write("Enter the number corresponding to your choice: ");
            int figureChoice;
            if (int.TryParse(Console.ReadLine(), out figureChoice) && figureChoice >= 1 && figureChoice <= 5)
            {

                Console.Write("Enter the initial coordinate (e.g., A3): ");
                string initialCoordinate = Console.ReadLine();


                if (IsValidCoordinate(initialCoordinate))
                {

                    Chessboard chessboard = new Chessboard();
                    chessboard.PlaceFigure((PieceType)figureChoice, initialCoordinate);

                    chessboard.PrintChessboard();


                    Console.Write("Enter the destination coordinate (e.g., B4): ");
                    string destCoordinate = Console.ReadLine();


                    if (IsValidCoordinate(destCoordinate))
                    {

                        bool canMove = chessboard.CanMove(initialCoordinate, destCoordinate,chessboard.Board);

                        Console.WriteLine(canMove ? "\nYes, the figure can move there." : "\nNo, the figure cannot move there.");
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid destination coordinate format. Please enter a valid coordinate (e.g., B4).");
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid initial coordinate format. Please enter a valid coordinate (e.g., A3).");
                }
            }
            else
            {
                Console.WriteLine(" Invalid choice. Please enter a number between 1 and 5.");
            }

          }
        
        static void GameChoice2()
        {
            Chessboard chessboard = new Chessboard();


            Console.Write("Enter the coordinates for the white king (e.g., A1): ");
            string whiteKingPosition = Console.ReadLine();
            chessboard.PlaceFigure(PieceType.King, whiteKingPosition);


            Console.Write("Enter the coordinates for the black king (e.g., H8): ");
            string blackKingPosition = Console.ReadLine();

            if (!IsPositionOccupied(chessboard, blackKingPosition))
            {
                chessboard.PlaceFigure(PieceType.King, blackKingPosition, PieceColor.Black);
            }
            else
            {
                Console.WriteLine("Position already occupied. Try another position.");
            }


            Console.Write("Enter the coordinates for the first black rook (e.g., B8): ");
            string blackRook1Position = Console.ReadLine();
            if (!IsPositionOccupied(chessboard, blackRook1Position))
            {
                chessboard.PlaceFigure(PieceType.Rook, blackRook1Position, PieceColor.Black);
            }
            else
            {
                Console.WriteLine("Position already occupied. Try another position.");
            }

            Console.Write("Enter the coordinates for the second black rook (e.g., G7): ");
            string blackRook2Position = Console.ReadLine();
            if (!IsPositionOccupied(chessboard, blackRook2Position))
            {
                chessboard.PlaceFigure(PieceType.Rook, blackRook2Position, PieceColor.Black);
            }
            else
            {
                Console.WriteLine("Position already occupied. Try another position.");
            }


            Console.Write("Enter the coordinates for the black queen (e.g., D8): ");
            string blackQueenPosition = Console.ReadLine();
            if (!IsPositionOccupied(chessboard, blackQueenPosition))
            {
                chessboard.PlaceFigure(PieceType.Queen, blackQueenPosition, PieceColor.Black);
            }
            else
            {
                Console.WriteLine("Position already occupied. Try another position.");
            }


            chessboard.PrintChessboard();


            bool isWhiteKingInCheck = IsKingInCheck(chessboard, whiteKingPosition);


            if (isWhiteKingInCheck)
            {
                Console.WriteLine("\nThe white king is in check!");
            }
            else
            {
                Console.WriteLine("\nThe white king is not in check.");
            }

            while (true)
            {

                Console.Write("Enter your move (e.g., A2 to B2): ");
                string userMove = Console.ReadLine();
                if (!IsValidCoordinate(userMove))
                {
                    Console.WriteLine("Invalid coordinates. Please enter a valid move.");
                    continue;
                }
                bool isValidMove = chessboard.CanMove(whiteKingPosition, userMove, chessboard.Board);

                if (!isValidMove || IsMovingKingIntoCheck(chessboard, whiteKingPosition, userMove))
                {
                    Console.WriteLine("Invalid move or moving the king into check. Please enter a valid move.");
                    continue;
                }


                chessboard.MovePiece(whiteKingPosition, userMove);


                ComputerMove(chessboard);

                chessboard.PrintChessboard();
                whiteKingPosition = userMove;

                bool gameOverAfterComputerMove = IsCheckmate(chessboard);
                bool gameIsStalmateAfterComputerMove = IsStalemate(chessboard);
                if (gameOverAfterComputerMove)
                {
                    Console.WriteLine("\n Checkmate Computer wins!!!");

                    break;
                }
                else if (gameIsStalmateAfterComputerMove)
                {
                    Console.WriteLine("\n Stalemate");
                    break;
                }
            }

        }

        static void GameChoice3()
        {
            Chessboard chessBoard = new Chessboard();

            chessBoard.PlaceFigure(PieceType.Queen, "A4", PieceColor.Black);
            chessBoard.PlaceFigure(PieceType.Bishop, "C6", PieceColor.Black);
            chessBoard.PrintPossibleMovesForFigures("A4");
        }
        
        static  bool IsCheckmate(Chessboard chessboard)
         {
                string whiteKingPosition = chessboard.FindPiecePosition(PieceType.King, PieceColor.White, chessboard);


                bool isWhiteKingInCheck = IsKingInCheck(chessboard, whiteKingPosition);

                if (!isWhiteKingInCheck)
                {

                    return false;
                }


                for (int row = 0; row < 8; row++)
                {
                    for (int col = 0; col < 8; col++)
                    {
                        string newPosition = Convert.ToChar('A' + col).ToString() + (8 - row).ToString();


                        if (chessboard.CanMove(whiteKingPosition, newPosition,chessboard.Board))
                        {

                            Chessboard tempBoard = new Chessboard();
                            tempBoard.CopyBoard(chessboard);


                            tempBoard.MovePiece(whiteKingPosition, newPosition);


                            if (!IsKingInCheck(tempBoard, newPosition))
                            {

                                return false;
                            }
                        }
                    }
                }


                return true;
        }
        static  bool IsStalemate(Chessboard chessboard)
         {
                string whiteKingPosition = chessboard.FindPiecePosition(PieceType.King, PieceColor.White, chessboard);


                bool isWhiteKingInCheck = IsKingInCheck(chessboard, whiteKingPosition);

                if (isWhiteKingInCheck)
                {

                    return false;
                }


                for (int row = 0; row < 8; row++)
                {
                    for (int col = 0; col < 8; col++)
                    {
                        string newPosition = Convert.ToChar('A' + col).ToString() + (8 - row).ToString();


                        if (chessboard.CanMove(whiteKingPosition, newPosition, chessboard.Board))
                        {

                            Chessboard tempBoard = new Chessboard();
                            tempBoard.CopyBoard(chessboard);


                            tempBoard.MovePiece(whiteKingPosition, newPosition);


                            if (!IsKingInCheck(tempBoard, newPosition))
                            {

                                return false;
                            }
                        }
                    }
                }


                return true;
         }

            static void ComputerMove(Chessboard chessboard)
            {
                string whiteKingPosition = chessboard.FindPiecePosition(PieceType.King, PieceColor.White, chessboard);

                string blackKingPosition = chessboard.FindPiecePosition(PieceType.King, PieceColor.Black, chessboard);

                List<string> blackFigures = chessboard.FindAllPiecePositions(PieceColor.Black, chessboard);
                blackFigures.Remove(blackKingPosition);

                if (chessboard.IsWhiteKingOnTopRows(whiteKingPosition))
                {

                    int whiteKingPositionRow = Convert.ToInt32(whiteKingPosition[1].ToString());
                    string position = Convert.ToString(whiteKingPositionRow + 1);

                    if (CheckExistenceInPosition(position, blackFigures))
                    {
                        string blackFigure = GetFigureToMove(position, blackFigures);

                        string blackFigureMove = blackFigure[0] + whiteKingPosition[1].ToString();

                        chessboard.MovePiece(blackFigure, blackFigureMove);


                    }
                    else
                    {
                        string blackFigure = GetFigureToMove(position, blackFigures);

                        string blackFigureMove = blackFigure[0] + Convert.ToString(Convert.ToInt32(whiteKingPosition[1].ToString()) + 1);

                        chessboard.MovePiece(blackFigure, blackFigureMove);

                    }

                }
                else
                {
                    int whiteKingPositionRow = Convert.ToInt32(whiteKingPosition[1].ToString());
                    string position = Convert.ToString(whiteKingPositionRow - 1);

                    if (CheckExistenceInPosition(position, blackFigures))
                    {
                        string blackFigure = GetFigureToMove(position, blackFigures);

                        string blackFigureMove = blackFigure[0] + whiteKingPosition[1].ToString();

                        chessboard.MovePiece(blackFigure, blackFigureMove);

                    }
                    else
                    {
                        string blackFigure = GetFigureToMove(position, blackFigures);

                        string blackFigureMove = blackFigure[0] + Convert.ToString(Convert.ToInt32(whiteKingPosition[1].ToString()) + 1);

                        chessboard.MovePiece(blackFigure, blackFigureMove);

                    }

                }
            }

            static bool CheckExistenceInPosition(string position, List<string> blackFiguresPositions)
            {
                for (int i = 0; i < blackFiguresPositions.Count; i++)
                {
                    if (blackFiguresPositions[i][1].ToString() == position)
                    {
                        return true;
                    }
                }

                return false;

            }

            static string GetFigureToMove(string position, List<string> blackFiguresPositions)
            {

                for (int i = 0; i < blackFiguresPositions.Count; i++)
                {
                    if (blackFiguresPositions[i][1].ToString() != position)
                    {
                        return blackFiguresPositions[i].ToString();
                    }

                }
                return blackFiguresPositions.First();
            }


            static bool IsKingInCheck(Chessboard chessboard, string whiteKingPosition)
            {
                for (int row = 0; row < 8; row++)
                {
                    for (int col = 0; col < 8; col++)
                    {
                        string currentPos = $"{(char)('A' + col)}{(8 - row)}";
                        ChessFigure figure = chessboard.GetFigureAtPosition(currentPos);


                        if (currentPos == whiteKingPosition)
                        {
                            continue;
                        }

                        if (figure != null && figure.CanMove(currentPos, whiteKingPosition, chessboard.Board))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }


            static bool IsValidCoordinate(string coordinate)
            {
                if (coordinate.Length == 2 &&
                    char.IsLetter(coordinate[0]) &&
                    char.IsDigit(coordinate[1]) &&
                    coordinate[1] >= '1' && coordinate[1] <= '8')
                {
                    return true;
                }
                return false;
            }


            static bool IsMovingKingIntoCheck(Chessboard chessboard, string kingPosition, string newPosition)
            {

                Chessboard tempBoard = new Chessboard();
                tempBoard.CopyBoard(chessboard);


                tempBoard.MovePiece(kingPosition, newPosition);


                return IsKingInCheck(tempBoard, newPosition);
            }


            static bool IsPositionOccupied(Chessboard chessboard, string position)
            {
                return chessboard.GetFigureAtPosition(position) != null;
            }

        
    }

}