using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha
{
    class Program
    {
        private const string LINE = "+-----------+";
        private const int ROW = 3;
        private const int COLUMN = 3;

        static void Main(string[] args)
        {
            char [][] board = new char[][] {
                new char [] { '1', '2', '3' },
                new char [] { '4', '5', '6' },
                new char [] { '7', '8', '9' }
            };

            int turn = 0; // 0 = X | 1 = O
            bool winner = false;
            bool isValidPlay;
            int position = -1;
            char player = '\0';

            for (int i = 0; i < ROW * COLUMN && !winner; i++)
            {
                CreateBoard(board);

                player = turn == 0 ? 'X' : 'O';

                do
                {
                    Console.WriteLine($"Jogador {player}, informe a sua jogada: ");
                    string strPosition = Console.ReadLine();

                    isValidPlay = CheckPlay(strPosition, out position);

                    if (isValidPlay)
                    {
                        int column = GetColumnByPosition(position);
                        int row = GetRowByPosition(position);

                        if (board[row][column] == 'X' || board[row][column] == 'O')
                        {
                            isValidPlay = false;
                            Console.WriteLine("A posição já está ocupada.");
                        }
                        else
                        {
                            board[row][column] = player;

                            winner = CheckWinner(board);
                        }                        
                    }
                    
                } while (!isValidPlay);                

                turn = turn == 0 ? 1 : 0;
                Console.WriteLine("-----------------------");
            }

            CreateBoard(board);

            if (winner)
            {
                Console.WriteLine($"O vencedor é o jogador: {player}");
            }
            else
            {
                Console.WriteLine("Deu velha!");
            }

            Console.ReadKey();
        }

        public static void CreateBoard(char[][] board)
        {
            Console.WriteLine(LINE);

            foreach(char[] row in board){
                foreach (char column in row)
                {
                    Console.Write($"| {column} ");
                }                

                Console.Write("|");

                Console.WriteLine();

                Console.WriteLine(LINE);
            }

            Console.WriteLine();
        }

        public static int GetRowByPosition(int position)
        {
            if(position <= 3)
            {
                return 0;
            }
            else if(position <= 6)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public static int GetColumnByPosition(int position)
        {
            return --position % COLUMN; 
        }

        public static bool CheckPlay(string play, out int position)
        {
            bool isValid = int.TryParse(play, out position);

            if (!isValid || (position < 0 || position > 9))
            {
                Console.WriteLine("Jogada inválida.");
                return false;
            }

            return true;
        }

        public static bool CheckWinner(char[][] board)
        {
            for(int i = 0; i < 3; i++)
            {
                if(board[0][0] == board[1][1] && board[0][0] == board[2][2] || 
                    board[0][2] == board[1][1] && board[0][2] == board[2][0]
                    )
                {
                    return true;
                }

                if(board[i][0] == board[i][1] && board[i][0] == board[i][2] ||
                    board[0][i] == board[1][i] && board[0][i] == board[2][i]                    
                    )
                {
                    return true;
                }

                
            }

            return false;
        }
    }
}
