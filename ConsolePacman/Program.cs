using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsolePacman
{
    internal class Program
    {
        static int score = 0;
        static void Main(string[] args)
        {
            char[,] Map = ReadMapByFile("Map.txt");
            int[] PacmanPosition = new int[2] 
            { 
                1,
                1
            };
            Console.CursorVisible = false;
            new Thread(() =>
            {
                while (true)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    GetNewPacmanPosition(key, ref PacmanPosition, Map);
                }
            }).Start();
            while (true) 
            { 
                Console.Clear();
                DrawText(ref Map, "score: " + score, 33, 0);
                DrawMap(Map);
                DrawPacman(PacmanPosition, '@');
                Thread.Sleep(500);
            }
        }
        static void DrawText(ref char[,] map, string text, int x, int y)
        {
            for (int i = 0; i < text.Length; i++)
            {
                map[y, x + i] = text[i];
            }
            
        }
        static void GetNewPacmanPosition(ConsoleKey key, ref int[] PacmanPosition, char[,] Map)
        {
            int x = PacmanPosition[0];
            int y = PacmanPosition[1];
            if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
            {
                x++;
            }
            else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A )
            {
                x--;
            }
            else if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
            {
                y--;
            }
            else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
            {
                y++;
            }
            if (Map[y, x] == ' ' || Map[y, x] == '.')
            {
                PacmanPosition = new int[] { x, y };
                if (Map[y, x] == '.')
                {
                    Map[y, x] = ' ';
                    score++;
                }
            }
        }
        static void DrawPacman(int[] position, char symbol)
        {
            ConsoleColor consoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(position[0], position[1]);
            Console.Write(symbol);
            Console.ForegroundColor = consoleColor;
        }
        static void DrawMap(char[,] map)
        {
            ConsoleColor consoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    Console.SetCursorPosition(x, y);
                    if (map[y, x] == '.')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(map[y, x]);
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
            }   
            Console.ForegroundColor = consoleColor;
        }
        static char[,] ReadMapByFile(string path)
        {
            string[] array = File.ReadAllLines(path);
            char[,] result = new char[array.Length, GetMaxLenghtByArray(array)];
            for (int y = 0; y < array.Length; y++)
            {
                for (int x = 0; x < array[y].Length; x++)
                {
                    result[y, x] = (array[y])[x];
                }
            }
            return result;
        }
        static int GetMaxLenghtByArray(string[] array)
        {
            int result = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (result < array[i].Length)
                {
                    result = array[i].Length;
                }
            }
            return result;
        }
    }
}
