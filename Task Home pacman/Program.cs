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
            int collectedBerries = 0;
            bool doseBarryCollect = false;
            char[,] map = ReadMap(out int playerPositionX, out int playerPositionY, out int allBerries, out char pacman, out char wall, out char berry);

            Console.CursorVisible = false;

            DrawMap(map);

            while (doseBarryCollect == false)
            {
                DrawPanel(collectedBerries, allBerries);

                Move(map, ref playerPositionX, ref playerPositionY, wall);
                DrawPacman(playerPositionX, playerPositionY, pacman);

                collectedBerries = CollectedBerries(map, collectedBerries, playerPositionX, playerPositionY, berry);
                doseBarryCollect = collectedBerries == allBerries;
            }

            Console.ReadKey();
        }

        static int CollectedBerries(char[,] map, int collectedBerries, int playerPositionX, int playerPositionY, char berry)
        {
            char symbol = ' ';

            if (map[playerPositionX, playerPositionY] == berry)
            {
                collectedBerries++;
                map[playerPositionX, playerPositionY] = symbol;
            }

            return collectedBerries;
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

        static char[,] ReadMap(out int playerPositionX, out int playerPositionY, out int allBarries, out char pacman, out char wall, out char berry)
        {
            pacman = '@';
            wall = '#';
            berry = '*';
            char symbol = ' ';

            playerPositionX = 0;
            playerPositionY = 0;
            allBarries = 0;

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
                        map[i, j] = berry;
                        allBarries++;

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

        static void DrawPanel(int collectedBerries, int allBerryies)
        {
            int panelPositionX = 15;
            int panelPositionY = 0;

            Console.SetCursorPosition(panelPositionX, panelPositionY);
            Console.WriteLine($"Собрано{collectedBerries}/{allBerryies}");
        }
    }
}

