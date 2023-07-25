using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Home_pacman
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int collectBarry = 0;
            bool startingGame = false;
            char[,] map = ReadMap(out int playerPositionX, out int playerPositionY, out int allBarry, out char pacman, out char wall, out char barry);

            Console.CursorVisible = false;

            DrawMap(map);

            while (startingGame == false)
            {
                DrawPanel(collectBarry, allBarry);

                Move(map, ref playerPositionX, ref playerPositionY, wall);
                DrawPacman(playerPositionX, playerPositionY, pacman);

                collectBarry = CollectBarry(map, collectBarry, allBarry, playerPositionX, playerPositionY, barry);
                startingGame = isFinish(collectBarry, allBarry);
            }

            Console.ReadKey();
        }

        static bool isFinish(int collectBarry, int allBarry)
        {
            return collectBarry == allBarry;
        }

        static int CollectBarry(char[,] map, int collectBerry, int allBerry, int playerPositionX, int playerPositionY, char berry)
        {
            char symbol = ' ';

            if (map[playerPositionX, playerPositionY] == berry)
            {
                collectBerry++;
                map[playerPositionX, playerPositionY] = symbol;
            }

            return collectBerry;
        }

        static void DrawPacman(int playerPositionX, int playerPositionY, char pacman)
        {
            Console.SetCursorPosition(playerPositionY, playerPositionX);
            Console.Write(pacman);
        }

        static void Move(char[,] map, ref int playerPositionX, ref int playerPositionY, char wall)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            char symbol = ' ';
            GetDirection(key, out int directionX, out int directionY);

            if (directionX != 0 || directionY != 0)
            {
                if (map[playerPositionX + directionX, playerPositionY + directionY] != wall)
                {
                    DrawPacman(playerPositionX, playerPositionY, symbol);
                    playerPositionX += directionX;
                    playerPositionY += directionY;
                }
            }

        }

        static void GetDirection(ConsoleKeyInfo key, out int directionX, out int directionY)
        {
            directionX = 0;
            directionY = 0;

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    directionX--;
                    break;
                case ConsoleKey.DownArrow:
                    directionX++;
                    break;
                case ConsoleKey.LeftArrow:
                    directionY--;
                    break;
                case ConsoleKey.RightArrow:
                    directionY++;
                    break;
            }
        }

        static char[,] ReadMap(out int playerPositionX, out int playerPositionY, out int allBarry, out char pacman, out char wall, out char barry)
        {
            pacman = '@';
            wall = '#';
            barry = '*';
            char symbol = ' ';

            playerPositionX = 0;
            playerPositionY = 0;
            allBarry = 0;

            char[,] map =
              {
                {wall,wall,wall,wall,wall,wall,wall,wall,wall,wall,wall,wall},
                {wall,symbol,wall,symbol,wall,symbol,wall,symbol,wall,symbol,symbol,wall},
                {wall,symbol,wall,symbol,wall,symbol,wall,symbol,wall,symbol,symbol,wall},
                {wall,symbol,symbol,symbol,symbol,symbol,wall,symbol,wall,symbol,wall,wall},
                {wall,symbol,symbol,wall,symbol,symbol,symbol,symbol,symbol,symbol,symbol,wall},
                {wall,symbol,symbol,wall,symbol,symbol,symbol,wall,symbol,wall,symbol,wall},
                {wall,symbol,wall,wall, symbol, symbol, symbol, symbol, symbol,symbol,symbol,wall},
                {wall,wall,symbol,symbol, symbol, symbol, wall, symbol, symbol,wall,wall,wall},
                {wall,pacman,symbol,symbol,wall,symbol,symbol,symbol,symbol,symbol,symbol,wall},
                {wall,wall,wall,wall,wall,wall,wall,wall,wall,wall,wall, wall},
            };

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == pacman)
                    {
                        playerPositionX = i;
                        playerPositionY = j;
                    }
                    else if (map[i, j] == symbol)
                    {
                        map[i, j] = barry;
                        allBarry++;

                    }
                }
            }

            return map;
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        static void DrawPanel(int collectBarry, int allBarry)
        {
            int panelPositionX = 15;
            int panelPositionY = 0;

            Console.SetCursorPosition(panelPositionX, panelPositionY);
            Console.WriteLine($"Собрано{collectBarry}/{allBarry}");
        }
    }
}

