namespace ConsoleAppchess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose a game you want to play ");
            Console.WriteLine("1. Guss can move");
            Console.WriteLine("2. Simple chess");
            int gameChoise;
            if (int.TryParse(Console.ReadLine(), out gameChoise) && gameChoise >= 1 && gameChoise <= 2)
            {   if(gameChoise == 1) 
                {
                    Console.WriteLine("Choose a figure to place on the chessboard:");
                    Console.WriteLine("1. King");
                    Console.WriteLine("2. Queen");
                    Console.WriteLine("3. Rook");
                    Console.WriteLine("4. Knight");
                    Console.WriteLine("5. Bishop");

                    Console.Write("Enter the number corresponding to your choice:");
                    int figurchoice;
                    if(int.TryParse(Console.ReadLine(),out figurchoice) && figurchoice >= 1 && figurchoice <= 5)
                    {
                        Console.Write("Enter the initial coordinate (e.g., A3):");
                            string initialCoordinate=Console.ReadLine();
                        if (IsWalidCoordinate(initialCoordinate))
                        {
                            ChessBoard chessBoard = new ChessBoard();
                            chessBoard.PlaceFigure((PieceType)figurchoice, initialCoordinate);
                            chessBoard.PrintBoard();
                            Console.Write("Enter the destination coordinate (e.g., B4): ");
                              string destCoordinate = Console.ReadLine();
                            if (IsWalidCoordinate(destCoordinate))
                            {
                                bool canMove = chessBoard.CanMove(initialCoordinate, destCoordinate);
                                Console.WriteLine(canMove ? "Yes, the figure can move there." : "No, the figure cannot move there.");
                            }
                            else
                            {
                                Console.WriteLine(" Invalid destination coordinate format. Please enter a valid coordinate (e.g., B6).");
                            }
                        
                        
                        }

                        else
                        {
                            Console.WriteLine(" Invalid initial coordinate format. Please enter a valid coordinate (e.g., A4).");
                        }

                    }
                    
                    else 
                    {
                        Console.WriteLine(" Invalid choice. Please enter a number between 1 and 5.");
                    }
                }
                else
                {   
                    ChessBoard chessBoard = new ChessBoard();
                    
                    chessBoard.PlaceFigure(PieceType.Queen, "A4", PieceColor.Black);
                    chessBoard.PlaceFigure(PieceType.Bishop, "A5", PieceColor.Black);
                    chessBoard.PrintPossibleMovesForQueen("A4");

                }
            }
            
        
        }
    
    
    
    
            static bool IsWalidCoordinate(string coordinate)
            {
               if(coordinate.Length == 2 && char.IsLetter(coordinate[0]) 
                && char.IsDigit(coordinate[1]) && coordinate[1] >= '1' && coordinate[1] <= '8')
               {
                return true;
               }
               return false;
            }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}