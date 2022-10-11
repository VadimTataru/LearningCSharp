namespace TicTacToe
{
    public class TicTac
    {
        #region Fileds
        private string[] points, trainingPoints;

        string filedNumString;
        string warningMessage = "Ну кто так ходит D: \n Выбери номер поля из диапазона [1..9]";
        //поле занято, перебрось кубик
        string wrongMoveMessage = "Кабинка занята!";
        string[,] outputField;
        bool isFirstPlayer = true;
        bool isGameOver;
        //пасхалочки
        string[] winTextLines =
        {
            "GG WP", "Ты молодец!=)", 
            "Тебе бы в битву экстрасенсов :D", 
            "Славная победа добывается потом и кровью.", 
            "Я не устал. А ты?", 
            "Чудесно!"
        };

        int lastX, lastO;
        int moveCounter = 0;
        (int, int, int)? winCombo;

        #endregion

        public TicTac()
        {
            lastX = 0;
            lastO = 0;
            filedNumString = "";
            points = new string[9] { " ", " ", " ", " ", " ", " ", " ", " ", " " };
            trainingPoints = new string[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            outputField = new string[,]
            {
                {$" {points[0]} | {points[1]} | {points[2]}" },
                {"---|---|---" },
                {$" {points[3]} | {points[4]} | {points[5]}" },
                {"---|---|---" },
                {$" {points[6]} | {points[7]} | {points[8]}" }
            };
        }

        #region Methods
        /// <summary>
        /// Стартуем игру
        /// </summary>
        public void StartGame()
        {
            isGameOver = false;            
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
                if (UpdateValues(fieldNum) && moveCounter > 4)
                    CheckForWinner();
            }
            while (!isGameOver);
        }        

        /// <summary>
        /// Отрисовка всего что можно
        /// </summary>
        private void Render()
        {
            Console.Clear();            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Tic-Tak-Toe");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            DrawTrainingField();
            if (winCombo != null)
                Draw(winCombo.Value, !isFirstPlayer);
            else
                Draw();
        }

        /// <summary>
        /// Обновляем значения полей
        /// </summary>
        /// <param name="fieldNum">Номер поля, куда был сделан ход</param>
        /// <returns>Удалось ли сходить</returns>
        private bool UpdateValues(int fieldNum)
        {
            if (fieldNum > 9 || fieldNum < 1)
            {
                ShowMessage(warningMessage);
                return false;
            }
            else
            {
                if (points[fieldNum - 1] != "X" && points[fieldNum - 1] != "O")
                {
                    if (isFirstPlayer)
                    {
                        points[fieldNum - 1] = "X";
                        lastX = fieldNum;
                    }
                    else
                    {
                        points[fieldNum - 1] = "O";
                        lastO = fieldNum;
                    }
                    isFirstPlayer = !isFirstPlayer;
                    moveCounter++;
                    return true;
                }
                else
                {
                    ShowMessage(wrongMoveMessage);
                    return false;
                }
            }
        }

        /// <summary>
        /// Метод, определяющий победителя
        /// </summary>
        private void CheckForWinner()
        {
            if (IsGameOver("X", out winCombo))
            {
                Render();
                Random rand = new Random();
                ShowMessage("Победитель - Х", rand.Next(winTextLines.Length), ConsoleColor.Green);
                isGameOver = true;
            }
            else if (IsGameOver("O", out winCombo))
            {
                Render();
                Random rand = new Random();
                ShowMessage("Победитель - O", rand.Next(winTextLines.Length), ConsoleColor.Blue);
                isGameOver = true;
            }
            else if (moveCounter >= 9)
            {
                ShowMessage("Победитель: ...а где он, кстати?", ConsoleColor.Yellow);
                isGameOver = true;
            }
        }

        /// <summary>
        /// Поле с обозначениями клеток
        /// </summary>
        private void DrawTrainingField()
        {
            Console.WriteLine("Номера полей");
            Console.WriteLine($" {trainingPoints[0]} | {trainingPoints[1]} | {trainingPoints[2]}");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {trainingPoints[3]} | {trainingPoints[4]} | {trainingPoints[5]}");
            Console.WriteLine("---|---|---");
            Console.WriteLine($" {trainingPoints[6]} | {trainingPoints[7]} | {trainingPoints[8]}");
            Console.WriteLine();
        }

        /// <summary>
        /// Рисуем после каждого хода
        /// </summary>
        private void Draw()
        {
            var indecesX = GetIndex(lastX);
            var indecesO = GetIndex(lastO);

            outputField = new string[,]
            {
                {" ", $"{points[0]}", " ", "|", " ", $"{points[1]}", " ", "|", " ", $"{points[2]}", " " },
                {"-", "-", "-", "|", "-", "-", "-", "|", "-", "-", "-"},
                {" ", $"{points[3]}", " ", "|", " ", $"{points[4]}", " ", "|", " ", $"{points[5]}", " " },
                {"-", "-", "-", "|", "-", "-", "-", "|", "-", "-", "-"},
                {" ", $"{points[6]}", " ", "|", " ", $"{points[7]}", " ", "|", " ", $"{points[8]}", " " }
            };
            Console.WriteLine("Здесь играть");
            for (int i = 0; i < outputField.GetLength(0); i++)
            {
                for (int j = 0; j < outputField.GetLength(1); j++)
                {
                    if (i == indecesX.Item1 && j == indecesX.Item2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    } else if(i == indecesO.Item1 && j == indecesO.Item2)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;                        
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(outputField[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Рисуем, когда кто-то выиграл
        /// </summary>
        /// <param name="winCombo">Победная комбинация (Тройной tuple)</param>
        /// <param name="isXWin">Победил Х?</param>
        private void Draw((int, int, int) winCombo, bool isXWin)
        {
            var indeces1 = GetIndex(winCombo.Item1+1);
            var indeces2 = GetIndex(winCombo.Item2+1);
            var indeces3 = GetIndex(winCombo.Item3+1);

            outputField = new string[,]
            {
                {" ", $"{points[0]}", " ", "|", " ", $"{points[1]}", " ", "|", " ", $"{points[2]}", " " },
                {"-", "-", "-", "|", "-", "-", "-", "|", "-", "-", "-"},
                {" ", $"{points[3]}", " ", "|", " ", $"{points[4]}", " ", "|", " ", $"{points[5]}", " " },
                {"-", "-", "-", "|", "-", "-", "-", "|", "-", "-", "-"},
                {" ", $"{points[6]}", " ", "|", " ", $"{points[7]}", " ", "|", " ", $"{points[8]}", " " }
            };
            Console.WriteLine("Здесь играть");
            for (int i = 0; i < outputField.GetLength(0); i++)
            {
                for (int j = 0; j < outputField.GetLength(1); j++)
                {
                    if ((i == indeces1.Item1 && j == indeces1.Item2) || (i == indeces2.Item1 && j == indeces2.Item2) || (i == indeces3.Item1 && j == indeces3.Item2))
                    {
                        if(isXWin)
                            Console.ForegroundColor = ConsoleColor.Red;
                        else
                            Console.ForegroundColor = ConsoleColor.Blue;
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(outputField[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private (int, int) GetIndex(int num)
        {
            switch(num)
            {
                case 1: return (0, 1);
                case 2: return (0, 5);
                case 3: return (0, 9);

                case 4: return (2, 1);
                case 5: return (2, 5);
                case 6: return (2, 9);

                case 7: return (4, 1);
                case 8: return (4, 5);
                case 9: return (4, 9);

                default: return (-1, -1);
            }
        }

        /// <summary>
        /// Проверка факта окончания игры
        /// </summary>
        /// <param name="playerSign">Символ игрока</param>
        /// <param name="comb">Комбинация для проверки факта окончания игры</param>
        /// <returns>Игра окончена?</returns>
        private bool IsGameOver(string playerSign, out (int, int, int)? comb)
        {
            //строки
            if(points[0] == playerSign && points[1] == playerSign && points[2] == playerSign)
            {
                comb = (0, 1, 2);
                return true;
            }
            if (points[3] == playerSign && points[4] == playerSign && points[5] == playerSign)
            {
                comb = (3, 4, 5);
                return true;
            }
            if (points[6] == playerSign && points[7] == playerSign && points[8] == playerSign)
            {
                comb = (6, 7, 8);
                return true;
            }
            //колонны
            if (points[0] == playerSign && points[3] == playerSign && points[6] == playerSign)
            {
                comb = (0, 3, 6);
                return true;
            }
            if (points[1] == playerSign && points[4] == playerSign && points[7] == playerSign)
            {
                comb = (1, 4, 7);
                return true;
            }
            if (points[2] == playerSign && points[5] == playerSign && points[8] == playerSign)
            {
                comb = (2, 5, 8);
                return true;
            }
            //диагонали
            if (points[0] == playerSign && points[4] == playerSign && points[8] == playerSign)
            {
                comb = (0, 4, 8);
                return true;
            }
            if (points[2] == playerSign && points[4] == playerSign && points[6] == playerSign)
            {
                comb = (2, 4, 6);
                return true;
            }

            comb = null;
            return false;
        }

        /// <summary>
        /// Вывод сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="color">Текст сообщения</param>
        private void ShowMessage(string message, ConsoleColor color = ConsoleColor.Red)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n " + message);
            Console.WriteLine(" Нажмите любую клавишу");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Вывод сообщения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="index">Индекс доп. сообщения</param>
        /// <param name="color">Цвет текста</param>
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