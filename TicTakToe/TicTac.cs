namespace TicTacToe
{
    public class TicTac
    {
        #region Fileds
        private string[] points, examplePoints;

        string filedNumString;
        string warningMessage = "Ну кто так ходит D: \n Выбери номер поля из диапазона [1..9]";
        string wrongMoveMessage = "Кабинка занята!";
        bool isFirstPlayer = true;
        bool isGameOver;
        string[] winTextLines =
        {
            "GG WP", "Ты молодец!=)", "Тебе бы в битву экстрасенсов :D", "Славная победа добывается потом и кровью.", "Я не устал. А ты?", "Никак вы, ?№*^, не научитесь!"
        };
        #endregion

        public TicTac()
        {
            filedNumString = ""; 
        }

        public void StartGame()
        {
            isGameOver = false;
            points = new string[9] { " ", " ", " ", " ", " ", " ", " ", " ", " " };
            examplePoints = new string[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            int fieldNum;            
            do
            {
                Render();
                Console.ForegroundColor = ConsoleColor.White;
                
                if(isFirstPlayer)
                    Console.WriteLine("Ход Х: ");
                else
                    Console.WriteLine("Ход O: ");

                filedNumString = Console.ReadLine();
                if (!int.TryParse(filedNumString, out fieldNum))
                {
                    ShowMessage(warningMessage);
                    continue;
                }
                UpdateValues(fieldNum);
            }
            while (!isGameOver);
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

        private void UpdateValues(int fieldNum)
        {
            if (fieldNum > 9 || fieldNum < 1)
            {
                ShowMessage(warningMessage);
            } else
            {
                if (points[fieldNum-1] != "X" && points[fieldNum - 1] != "O")
                {
                    points[fieldNum-1] = isFirstPlayer ? "X" : "O";
                    isFirstPlayer = !isFirstPlayer;

                    if(IsGameOver("X"))
                    {
                        Render();
                        Random rand = new Random();
                        ShowMessage("Победитель - Х", rand.Next(winTextLines.Length), ConsoleColor.Green);
                        isGameOver = true;
                    } else if(IsGameOver("O"))
                    {
                        Render();
                        Random rand = new Random();
                        ShowMessage("Победитель - Y", rand.Next(winTextLines.Length), ConsoleColor.Blue);
                        isGameOver = true;
                    }                    
                }
                else
                {
                    ShowMessage(wrongMoveMessage);
                }
            }
        }

        private void DrawField()
        {
            Console.WriteLine("Здесь играть     Номера полей");
            Console.WriteLine($" {points[0]} | {points[1]} | {points[2]}        {examplePoints[0]} | {examplePoints[1]} | {examplePoints[2]}");
            Console.WriteLine("---|---|---      ---|---|---");
            Console.WriteLine($" {points[3]} | {points[4]} | {points[5]}        {examplePoints[3]} | {examplePoints[4]} | {examplePoints[5]}");
            Console.WriteLine("---|---|---      ---|---|---");
            Console.WriteLine($" {points[6]} | {points[7]} | {points[8]}        {examplePoints[6]} | {examplePoints[7]} | {examplePoints[8]}");
        }

        private bool IsGameOver(string playerSign)
        {
            return (
                (points[0] == playerSign && points[1] == playerSign && points[2] == playerSign) ||
                (points[3] == playerSign && points[4] == playerSign && points[5] == playerSign) ||
                (points[6] == playerSign && points[8] == playerSign && points[9] == playerSign) ||
                
                (points[0] == playerSign && points[3] == playerSign && points[6] == playerSign) ||
                (points[1] == playerSign && points[4] == playerSign && points[7] == playerSign) ||
                (points[2] == playerSign && points[5] == playerSign && points[8] == playerSign) ||
                
                (points[0] == playerSign && points[4] == playerSign && points[8] == playerSign) ||
                (points[2] == playerSign && points[4] == playerSign && points[6] == playerSign)
            );
        }

        private void ShowMessage(string message, ConsoleColor color = ConsoleColor.Red)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n " + message);
            Console.WriteLine(" Нажмите любую клавишу");
            Console.ReadKey();
        }

        private void ShowMessage(string message, int index, ConsoleColor color = ConsoleColor.Red)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n " + message);
            Console.WriteLine(winTextLines[index]);
            Console.WriteLine(" Нажмите любую клавишу");
            Console.ReadKey();
        }
        #endregion
    }
}