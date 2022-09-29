namespace TicTacToe
{
    public class TicTac
    {
        #region Fileds
        private string[,] points = new string[3, 3]
        {
            {" ", " ", " " },
            {" ", " ", " " },
            {" ", " ", " " },
        };

        string[] coordinatesString;
        string warningMessage = "Ошибка ввода! Введите координаты столбца и строки должны быть в пределах [0..2]";
        string wrongMoveMessage = "По этим координатам ход уже был сделан!";
        bool isFirstPlayer = true;
        #endregion

        public TicTac()
        {
            coordinatesString = new string[] {" ", " "}; 
        }

        public void StartGame()
        {
            int row, col;            
            do
            {
                Render();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Введите координаты поля через пробел: ");
                coordinatesString = Console.ReadLine().Split(" ");
                if (coordinatesString.Length < 2 || !int.TryParse(coordinatesString[0], out row) || !int.TryParse(coordinatesString[1], out col))
                {
                    ShowWarning(warningMessage);
                    continue;
                }
                UpdateValues(row, col);
            }
            while (!IsGameOver());
        }

        #region Methods

        private void Render()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Tic-Tak-Toe");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            DrawField();
        }

        private void UpdateValues(int row, int col)
        {
            if (row > 2 || row < 0 || col > 2 || col < 0)
            {
                ShowWarning(warningMessage);
            } else
            {
                if (points[row, col] == " ")
                {
                    points[row, col] = isFirstPlayer ? "X" : "O";
                    isFirstPlayer = !isFirstPlayer;

                    // TODO: Add CheckForWinner() method

                }
                else
                {
                    ShowWarning(wrongMoveMessage);
                }
            }
        }

        private void DrawField()
        {
            Console.WriteLine($" {points[0, 0]} | {points[0, 1]} | {points[0, 2]} ");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {points[1, 0]} | {points[1, 1]} | {points[1, 2]} ");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {points[2, 0]} | {points[2, 1]} | {points[2, 2]} ");
        }

        private bool IsGameOver()
        {
            return false;
        }

        static void ShowWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n " + message);
            Console.WriteLine(" Нажмите любую клавишу");
            Console.ReadKey();
        }

        #endregion
    }
}