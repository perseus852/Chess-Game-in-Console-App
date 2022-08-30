using System;
using System.Threading;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace Chess
{
    class Program
    {
        static string[,] table = new string[,] {{"br", "bn", "bb", "bq", "bk", "bb", "bn","br"},//gametable for mode 1
                                               { "bp", "bp", "bp", "bp", "bp", "bp", "bp", "bp"},
                                               {".", "." , "." , "." , "." , "." , "." , "."},
                                               {".", "." , "." , "." , "." , "." , "." , "."},
                                               {".", "." , "." , "." , "." , "." , "." , "."},
                                               {".", "." , "." , "." , "." , "." , "." , "."},
                                               { "wp", "wp", "wp", "wp", "wp", "wp", "wp", "wp"},
                                               { "wr", "wn", "wb", "wq", "wk", "wb", "wn","wr"}};
        static string[,] coordinates = new string[,] {{ "a8", "b8", "c8", "d8", "e8", "f8", "g8","h8"},//gametable for mode 2
                                                       { "a7", "b7", "c7", "d7", "e7", "f7", "g7", "h7"},
                                                       {"a6", "b6" , "c6" , "d6" , "e6" , "f6" , "g6" , "h6"},
                                                       {"a5", "b5" , "c5" , "d5" , "e5" , "f5" , "g5" , "h5"},
                                                       {"a4", "b4" , "c4" , "d4" , "e4" , "f4" , "g4" , "h4"},
                                                       {"a3", "b3" , "c3" , "d3" , "e3" , "f3" , "g3" , "h3"},
                                                       { "a2", "b2", "c2", "d2", "e2", "f2", "g2", "h2"},
                                                       {"a1", "b1", "c1", "d1", "e1", "f1", "g1","h1"}};

        static Char[] columns = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        static Char[] rows = new char[] { '8', '7', '6', '5', '4', '3', '2', '1' };
        static bool PawnBlack(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if (start_x == 1 && ((row == start_x + 2)) && col == start_y)
            {

                flag = true;
            }
            else if (row == start_x + 1 && col == start_y && table[row, col] == ".")
            {
                flag = true;
            }

            else if ((row == start_x + 1 && (start_y == col + 1 || start_y == col - 1)) && table[row, col] != ".")
            {
                flag = true;
            }
            return flag;
        }
        static bool PawnWhite(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if (start_x == 6 && ((row == start_x - 2)) && col == start_y)
            {
                flag = true;
            }
            else if (row == start_x - 1 && col == start_y && table[row, col] == ".")
            {
                flag = true;
            }
            else if ((row == start_x - 1 && (start_y == col - 1 || start_y == col + 1)) && table[row, col] != ".")
            {
                flag = true;
            }
            return flag;
        }
        static string Promotion(int row, int col, int cursorx, int cursory, int colour)
        {
            string promotion = "";
            string stone = "";
            Console.SetCursorPosition(5, 30);
            while (promotion != "Q" && promotion != "R" && promotion != "B" && promotion != "N")
            {
                Console.WriteLine("Please select piese: Q, R, B, N");
                promotion = Console.ReadLine();
            }
            if (promotion == "Q")
            {
                if (colour % 4 == 0 || colour % 4 == 1)
                    stone = "wq";
                else if (colour % 4 == 2 || colour % 4 == 3)
                    stone = "bq";

            }
            else if (promotion == "R")
            {
                if (colour % 4 == 0 || colour % 4 == 1)
                    stone = "wr";
                else if (colour % 4 == 2 || colour % 4 == 3)
                    stone = "br";
            }
            else if (promotion == "B")
            {
                if (colour % 4 == 0 || colour % 4 == 1)
                    stone = "wb";
                else if (colour % 4 == 2 || colour % 4 == 3)
                    stone = "bb";
            }
            else if (promotion == "N")
            {
                if (colour % 4 == 0 || colour % 4 == 1)
                    stone = "wn";
                else if (colour % 4 == 2 || colour % 4 == 3)
                    stone = "bn";
            }
            Console.SetCursorPosition(cursorx, cursory);
            Console.Write(promotion);


            return stone;
        }
        static bool RookBlack(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if (start_y == col && start_x < row)
            {
                for (int i = start_x; i < row; i++)
                {
                    if (table[i, col] != ".")
                    {
                        flag = false;
                        break;
                    }
                    else flag = true;
                }
            }
            else if (start_y == col && start_x > row)
            {
                for (int i = start_x; i > row; i--)
                {
                    if (table[i, col] != ".")
                    {
                        flag = false;
                        break;
                    }
                    else flag = true;
                }
            }
            else if (start_x == row && start_y < col)
            {
                for (int i = start_y; i < col; i++)
                {
                    if (table[row, i] != ".")
                    {
                        flag = false;
                        break;
                    }
                    else flag = true;
                }
            }
            else if (start_x == row && start_y > col)
            {
                for (int i = start_y; i > col; i--)
                {
                    if (table[row, i] != ".")
                    {
                        flag = false;
                        break;
                    }
                    else flag = true;
                }
            }
            return flag;
        }
        static bool Rook(int start_x, int start_y, int row, int col)//rook function for game mod 2
        {
            bool flag = false;
            for (int i = start_x - 1; i >= 0; i--)
            {
                if (row == i && col == start_y)
                {
                    flag = true;
                }

            }
            for (int i = start_y - 1; i >= 0; i--)
            {
                if (col == i && row == start_x)
                {
                    flag = true;
                }
            }
            for (int i = start_x + 1; i <= 8; i++)
            {
                if (row == i && col == start_y)
                {
                    flag = true;
                }
            }
            for (int i = start_y + 1; i <= 8; i++)
            {
                if (col == i && row == start_x)
                {
                    flag = true;
                }
            }
            return flag;
        }
        static bool RookWhite(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if (start_y == col && start_x < row)
            {
                for (int i = start_x; i < row; i++)
                {
                    if (table[i, col] != ".")
                    {
                        flag = false;
                        break;
                    }
                    else flag = true;
                }
            }
            else if (start_y == col && start_x > row)
            {
                for (int i = start_x; i > row; i--)
                {
                    if (table[i, col] != ".")
                    {
                        flag = false;
                        break;
                    }
                    else flag = true;
                }
            }
            else if (start_x == row && start_y < col)
            {
                for (int i = start_y; i < col; i++)
                {
                    if (table[row, i] != ".")
                    {
                        flag = false;
                        break;
                    }
                    else flag = true;
                }
            }
            else if (start_x == row && start_y > col)
            {
                for (int i = start_y; i > col; i--)
                {
                    if (table[row, i] != ".")
                    {
                        flag = false;
                        break;
                    }
                    else flag = true;
                }
            }
            return flag;
        }
        static bool KnightBlack(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if ((start_x == row + 2 && (start_y == col + 1 || start_y == col - 1)) || (start_x == row - 2 && (start_y == col + 1 || start_y == col - 1))
                || (start_y == col - 2 && (start_x == row + 1 || start_x == row - 1)) || (start_y == col + 2 && (start_x == row + 1 || start_x == row - 1)))
            {
                flag = true;
            }
            return flag;
        }
        static bool KnightWhite(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if ((start_x == row + 2 && (start_y == col + 1 || start_y == col - 1)) || (start_x == row - 2 && (start_y == col + 1 || start_y == col - 1))
               || (start_y == col - 2 && (start_x == row + 1 || start_x == row - 1)) || (start_y == col + 2 && (start_x == row + 1 || start_x == row - 1)))
            {
                flag = true;
            }
            return flag;
        }
        static bool BishopBlack(int start_x, int start_y, int row, int col)
        {
            int aaa, bbb;
            string c;
            bool flag = false;
            if (start_x < row && start_y < col)
            {
                aaa = start_x;
                bbb = start_y;
                c = "0";
                for (int i = 0; i < (row - start_x) - 1; i++)
                {
                    aaa += 1;
                    bbb += 1;
                    if (aaa < 8 && bbb < 8)
                    {
                        if (table[aaa, bbb] != ".")
                        {
                            c = "1";
                            break;
                        }
                    }

                }
                if (c == "0")
                {
                    flag = true;
                }
            }
            else if (start_x < row && start_y > col)
            {
                aaa = start_x;
                bbb = start_y;
                c = "0";
                for (int i = 0; i < (row - start_x) - 1; i++)
                {
                    aaa += 1;
                    bbb -= 1;
                    if (aaa < 8 && bbb >= 0)
                    {
                        if (table[aaa, bbb] != ".")
                        {
                            c = "1";
                            break;
                        }
                    }
                }
                if (c == "0")
                {
                    flag = true;
                }
            }
            else if (start_x > row && start_y < col)
            {
                aaa = start_x;
                bbb = start_y;
                c = "0";
                for (int i = 0; i < (start_x - row) - 1; i++)
                {
                    aaa -= 1;
                    bbb += 1;
                    if (aaa >= 0 && bbb < 8)
                    {
                        if (table[aaa, bbb] != ".")
                        {
                            c = "1";
                            break;
                        }
                    }
                }
                if (c == "0")
                {
                    flag = true;
                }
            }
            else if (start_x > row && start_y > col)
            {
                aaa = start_x;
                bbb = start_y;
                c = "0";
                for (int i = 0; i < (start_x - row) - 1; i++)
                {
                    aaa -= 1;
                    bbb -= 1;
                    if (aaa >= 0 && bbb >= 8)
                    {
                        if (table[aaa, bbb] != ".")
                        {
                            c = "1";
                            break;
                        }
                    }
                }
                if (c == "0")
                {
                    flag = true;
                }
            }
            return flag;
        }
        static bool BishopWhite(int start_x, int start_y, int row, int col)
        {
            int aaa, bbb;
            string c;
            bool flag = false;
            if (start_x < row && start_y < col)
            {
                aaa = start_x;
                bbb = start_y;
                c = "0";
                for (int i = 0; i < (row - start_x) - 1; i++)
                {
                    aaa += 1;
                    bbb += 1;
                    if (aaa < 8 && bbb < 8)
                    {
                        if (table[aaa, bbb] != ".")
                        {
                            c = "1";
                            break;
                        }
                    }
                }
                if (c == "0")
                {
                    flag = true;
                }
            }
            else if (start_x < row && start_y > col)
            {
                aaa = start_x;
                bbb = start_y;
                c = "0";
                for (int i = 0; i < (row - start_x) - 1; i++)
                {
                    aaa += 1;
                    bbb -= 1;
                    if (aaa < 8 && bbb >= 0)
                    {
                        if (table[aaa, bbb] != ".")
                        {
                            c = "1";
                            break;
                        }
                    }
                }
                if (c == "0")
                {
                    flag = true;
                }
            }
            else if (start_x > row && start_y < col)
            {
                aaa = start_x;
                bbb = start_y;
                c = "0";
                for (int i = 0; i < (start_x - row) - 1; i++)
                {
                    aaa -= 1;
                    bbb += 1;
                    if (aaa >= 0 && bbb < 8)
                    {
                        if (table[aaa, bbb] != ".")
                        {
                            c = "1";
                            break;
                        }
                    }
                }
                if (c == "0")
                {
                    flag = true;
                }
            }
            else if (start_x > row && start_y > col)
            {
                aaa = start_x;
                bbb = start_y;
                c = "0";
                for (int i = 0; i < (start_x - row) - 1; i++)
                {
                    aaa -= 1;
                    bbb -= 1;
                    if (aaa >= 0 && bbb >= 0)
                    {
                        if (table[aaa, bbb] != ".")
                        {
                            c = "1";
                            break;
                        }
                    }
                }
                if (c == "0")
                {
                    flag = true;
                }
            }

            return flag;
        }
        static bool QueenBlack(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if (RookBlack(start_x, start_y, row, col) == true)
            {
                flag = true;
            }
            else if (BishopBlack(start_x, start_y, row, col) == true)
            {
                flag = true;
            }
            return flag;
        }
        static bool QueenWhite(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if (RookWhite(start_x, start_y, row, col) == true)
            {
                flag = true;
            }
            else if (BishopWhite(start_x, start_y, row, col) == true)
            {
                flag = true;
            }
            return flag;
        }
        static bool KingBlack(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if ((start_x == row && (start_y == col + 1 || start_y == col - 1)) || (start_y == col && (start_x == row + 1 || start_x == row - 1))
                || (start_x == row + 1 && (start_y == col + 1 || start_y == col - 1)) || (start_x == row - 1 && (start_y == col + 1 || start_y == col - 1)))
            {
                flag = true;
            }
            return flag;
        }
        static bool KingWhite(int start_x, int start_y, int row, int col)
        {
            bool flag = false;
            if ((start_x == row && (start_y == col + 1 || start_y == col - 1)) || (start_y == col && (start_x == row + 1 || start_x == row - 1))
                || (start_x == row + 1 && (start_y == col + 1 || start_y == col - 1)) || (start_x == row - 1 && (start_y == col + 1 || start_y == col - 1)))
            {
                flag = true;
            }
            return flag;
        }
        static bool WhiteKing_check(int wkingx, int wkingy)
        {
            bool flag = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (table[i, j] == "bn" && KnightBlack(i, j, wkingx, wkingy) == true)
                        flag = true;
                    if (table[i, j] == "br" && (Rook(i, j, wkingx, wkingy) == true && (i == wkingx || j == wkingy)))
                        flag = true;
                    if (table[i, j] == "bb" && BishopBlack(i, j, wkingx, wkingy) == true && (i - wkingx == j - wkingy || i - wkingx == wkingy - j))
                        flag = true;
                    if (table[i, j] == "bq")
                    {
                        if ((RookBlack(i, j, wkingx, wkingy) == true && (i == wkingx || j == wkingy)) || (BishopBlack(i, j, wkingx, wkingy) == true && (i - wkingx == j - wkingy || i - wkingx == wkingy - j)))
                            flag = true;
                    }
                    if (table[i, j] == "bp" && PawnBlack(i, j, wkingx, wkingy) == true)
                        flag = true;
                }
            }
            return flag;
        }
        static bool BlackKing_check(int bkingx, int bkingy)
        {
            bool flag = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (table[i, j] == "wn" && KnightWhite(i, j, bkingx, bkingy) == true)
                        flag = true;

                    if (table[i, j] == "wr" && (Rook(i, j, bkingx, bkingy) == true && (i == bkingx || j == bkingy)))
                        flag = true;
                    if (table[i, j] == "wb" && BishopWhite(i, j, bkingx, bkingy) == true && (i - bkingx == j - bkingy || i - bkingx == bkingy - j))
                        flag = true;
                    if (table[i, j] == "wq")
                    {
                        if ((RookWhite(i, j, bkingx, bkingy) == true && (i == bkingx || j == bkingy)) || (BishopWhite(i, j, bkingx, bkingy) == true && (i - bkingx == j - bkingy || i - bkingx == bkingy - j)))
                            flag = true;
                    }
                    if (table[i, j] == "wp" && PawnWhite(i, j, bkingx, bkingy) == true)
                        flag = true;
                }
            }
            return flag;
        }
        static void ErrorMessage(string message)
        {
            Console.SetCursorPosition(10, 1);
            Console.WriteLine(message + " can't go this way!");
            Thread.Sleep(1000);
            Console.SetCursorPosition(10, 1);
            Console.WriteLine("                                  ");
        }
        static void WriteNotation(int xxx, int yyy, string notation, int count)
        {
            Console.SetCursorPosition(xxx, yyy);
            Console.ResetColor();
            Console.Write(count.ToString() + '.' + notation);
        }
        static void SaveGame(string notation, int turn)
        {
            File.OpenWrite("C:/Users/serda/OneDrive/Belgeler/c#/savedgame.txt").Close();
            StreamWriter saved = File.AppendText("C:/Users/serda/OneDrive/Belgeler/c#/savedgame.txt");
            if (turn % 2 == 0)
            {
                saved.Write(notation + ' ');
            }
            else if (turn % 2 == 1)
            {
                saved.WriteLine(notation);
            }
            saved.Close();
        }
        static void DeleteSavedGame(string file, int choise)
        {
            if (choise == 1 || choise == 2)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
        }
        static void Hint(int start_x, int start_y, int num)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (table[start_x, start_y] == "wk" && num == 0)
                    {
                        if (KingWhite(start_x, start_y, i, j) == true && table[i, j].StartsWith('b'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Kx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "bk" && num == 1)
                    {
                        if (KingBlack(start_x, start_y, i, j) == true && table[i, j].StartsWith('w'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Kx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "wr" && num == 0)
                    {
                        if (Rook(start_x, start_y, i, j) == true && table[i, j].StartsWith('b'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Rx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "br" && num == 1)
                    {
                        if (Rook(start_x, start_y, i, j) == true && table[i, j].StartsWith('w'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Rx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "wp" && num == 0)
                    {
                        if (PawnWhite(start_x, start_y, i, j) == true && table[i, j].StartsWith('b'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + columns[start_y] + "x" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "bp" && num == 1)
                    {
                        if (PawnBlack(start_x, start_y, i, j) == true && table[i, j].StartsWith('w'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + columns[start_y] + "x" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "wb" && num == 0)
                    {
                        if (BishopWhite(start_x, start_y, i, j) == true && table[i, j].StartsWith('b'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Bx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "bb" && num == 1)
                    {
                        if (BishopBlack(start_x, start_y, i, j) == true && table[i, j].StartsWith('w'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Bx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "wn" && num == 0)
                    {
                        if (KnightWhite(start_x, start_y, i, j) == true && table[i, j].StartsWith('b'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Nx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "bn" && num == 1)
                    {
                        if (KnightBlack(start_x, start_y, i, j) == true && table[i, j].StartsWith('w'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Nx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "wq" && num == 0)
                    {
                        if (QueenWhite(start_x, start_y, i, j) == true && table[i, j].StartsWith('b'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Qx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }
                    if (table[start_x, start_y] == "bq" && num == 1)
                    {
                        if (QueenWhite(start_x, start_y, i, j) == true && table[i, j].StartsWith('w'))
                        {
                            Console.SetCursorPosition(0, 28);
                            Console.WriteLine("Pressed H!");
                            Console.Write("Hint: " + "Qx" + columns[j] + rows[i]);
                            Thread.Sleep(5000);
                            Console.SetCursorPosition(0, 28);
                            Console.Write("                  ");
                            Console.SetCursorPosition(0, 29);
                            Console.Write("                  ");
                        }
                    }

                }
            }
        }

        static void Main(string[] args)
        {
            Console.SetCursorPosition(6, 2);//creating game table
            Console.Write("+ - - - - - - - - - - - - - - - +");
            Console.SetCursorPosition(6, 3);
            for (int i = 0; i < 15; i++)
            {
                Console.SetCursorPosition(6, 3 + i);
                Console.Write("|");
            }

            Console.SetCursorPosition(6, 18);
            Console.Write("+ - - - - - - - - - - - - - - - +");
            int cursoryy = 3;
            int cursorxx = 8;

            string[,] gametable = new string[8, 8];
            Console.SetCursorPosition(8, 3);
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(cursorxx, cursoryy);
                for (int a = 0; a < 8; a++)
                {
                    gametable[i, a] = ".";
                    Console.Write(gametable[i, a] + "   ");
                }
                cursoryy = cursoryy + 2;
            }
            for (int i = 0; i < 15; i++)
            {
                Console.SetCursorPosition(38, 3 + i);
                Console.Write("|");
            }
            int x = 5;
            int y = 3;
            for (int i = 8; i >= 1; i--)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(i);
                y = y + 2;
            }
            Console.SetCursorPosition(8, 19);
            Console.Write("a   b   c   d   e   f   g   h");

            Char[,] black = new char[,] { { 'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R' },
                                           { 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'} };//black chess pieces

            Char[,] white = new char[,] { { 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P' },
                                           { 'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R'} };//white chess pieces

            int aa = 8;

            for (int i = 0; i <= 7; i++)// placing chess pieces on the table
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(aa, 3);
                Console.Write(black[0, i]);
                aa += 4;
            }
            aa = 8;
            for (int i = 0; i <= 7; i++)
            {
                Console.SetCursorPosition(aa, 5);
                Console.Write(black[1, i]);
                aa += 4;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            int bb = 8;
            for (int i = 0; i <= 7; i++)
            {
                Console.SetCursorPosition(bb, 15);
                Console.Write(white[0, i]);
                bb += 4;
            }
            bb = 8;
            for (int i = 0; i <= 7; i++)
            {
                Console.SetCursorPosition(bb, 17);
                Console.Write(white[1, i]);
                bb += 4;
            }


            ConsoleKeyInfo cki;
            int cursorx = 8, cursory = 3;
            int counter = 1;
            string stone = "z";
            int row = 0;
            int col = 0;
            int colour = 0;
            int start_x = 0; int start_y = 0;
            int coordinatex = 50; int coordinatey = 5;
            int counteer = 0;
            int turn = 0;
            int even = 0;
            int wkingx = 7; int wkingy = 4; int bkingx = 0; int bkingy = 4;
            bool gameover = false;
            int wcheck = 0; int bcheck = 0;

            Console.ResetColor();
            Console.SetCursorPosition(5, 22);
            Console.WriteLine("Please choose game mod:");
            Console.SetCursorPosition(5, 23);
            Console.WriteLine("1 - for playing with keyboard, 2 - for playing by writing coordinate, 3 - for open saved game ");
            Console.SetCursorPosition(5, 24);
            int choise = Convert.ToInt32(Console.ReadLine());
            DeleteSavedGame("C:/Users/serda/OneDrive/Belgeler/c#/savedgame.txt", choise);
            Console.SetCursorPosition(10, 0);
            Console.ResetColor();
            Console.WriteLine("White's turn");
            while (true)
            {
                if (choise == 1)
                {
                    while (gameover != true)//The game continues unless there is a checkmate.                        
                    {
                        Console.SetCursorPosition(cursorx, cursory);
                        if (Console.KeyAvailable)
                        {
                            cki = Console.ReadKey(true);
                            if (cki.Key == ConsoleKey.RightArrow && cursorx < 34)
                            {
                                Console.WriteLine("");
                                cursorx += 4;
                                col += 1;
                            }
                            if (cki.Key == ConsoleKey.LeftArrow && cursorx >= 10)
                            {
                                Console.WriteLine("");
                                cursorx -= 4;
                                col -= 1;
                            }
                            if (cki.Key == ConsoleKey.UpArrow && cursory > 3)
                            {
                                Console.WriteLine("");
                                cursory -= 2;
                                row -= 1;
                            }
                            if (cki.Key == ConsoleKey.DownArrow && cursory < 16)
                            {
                                Console.WriteLine("");
                                cursory += 2;
                                row += 1;
                            }

                            if (cki.Key == ConsoleKey.H)//which stone the player can eat appears on the screen.
                            {
                                if (colour % 4 == 0 || colour % 4 == 1)
                                {
                                    for (int i = 0; i < 8; i++)
                                    {
                                        for (int j = 0; j < 8; j++)
                                        {
                                            Hint(i, j, 0);
                                        }
                                    }
                                }
                                else if (colour % 4 == 2 || colour % 4 == 3)
                                {
                                    for (int i = 0; i < 8; i++)
                                    {
                                        for (int j = 0; j < 8; j++)
                                        {
                                            Hint(i, j, 1);
                                        }
                                    }
                                }
                            }
                            if (cki.Key == ConsoleKey.R)//leaving the wrong stone to its old place
                            {
                                if (stone == "P" && (colour % 4 == 2 || colour % 4 == 3) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "bp";
                                }
                                else if (stone == "R" && (colour % 4 == 2 || colour % 4 == 3) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "br";
                                }
                                else if (stone == "N" && (colour % 4 == 2 || colour % 4 == 3) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "bn";
                                }
                                else if (stone == "B" && (colour % 4 == 2 || colour % 4 == 3) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "bb";
                                }
                                else if (stone == "Q" && (colour % 4 == 2 || colour % 4 == 3) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "bq";
                                }
                                else if (stone == "K" && (colour % 4 == 2 || colour % 4 == 3) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "bk";
                                }
                                else if (stone == "P" && (colour % 4 == 0 || colour % 4 == 1) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "wp";
                                }
                                else if (stone == "R" && (colour % 4 == 0 || colour % 4 == 1) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "wr";
                                }
                                else if (stone == "N" && (colour % 4 == 0 || colour % 4 == 1) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "wn";
                                }
                                else if (stone == "B" && (colour % 4 == 0 || colour % 4 == 1) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "wb";
                                }
                                else if (stone == "Q" && (colour % 4 == 0 || colour % 4 == 1) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "wq";
                                }
                                else if (stone == "K" && (colour % 4 == 0 || colour % 4 == 1) && start_x == row && start_y == col)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.Write(stone);
                                    colour -= 1;
                                    counter *= -1;
                                    table[row, col] = "wk";
                                }

                            }

                            /*if (BlackKing_check(bkingx, bkingy) == true)
                            {
                                bcheck++;
                                Console.SetCursorPosition(40, 1);
                                Console.WriteLine("Black king check");
                            }
                            else if (BlackKing_check(bkingx, bkingy) == false)
                            {
                                bcheck = 0;
                                Console.SetCursorPosition(40, 1);
                                Console.WriteLine("                  ");
                            }*/
                            if (cki.Key == ConsoleKey.Spacebar)
                            {

                                if (colour % 4 == 2 || colour % 4 == 3)
                                {
                                    Console.ResetColor();
                                    if (BlackKing_check(bkingx, bkingy) == true)
                                    {
                                        bcheck++;
                                        Console.SetCursorPosition(40, 1);
                                        Console.WriteLine("Black king check");
                                    }
                                    else if (BlackKing_check(bkingx, bkingy) == false)
                                    {
                                        bcheck = 0;
                                        Console.SetCursorPosition(40, 1);
                                        Console.WriteLine("                  ");
                                    }
                                    if ((counter == 1) & (table[row, col] != ".") & ((table[row, col] == "bp") || (table[row, col] == "br") || (table[row, col] == "bn") || (table[row, col] == "bb") || (table[row, col] == "bq") || (table[row, col] == "bk")))//is the place where the desired stone to be played is determined for black.
                                    {
                                        Console.SetCursorPosition(10, 0);
                                        Console.WriteLine("Black's turn");
                                        Console.SetCursorPosition(cursorx, cursory);
                                        if (table[row, col] == "bp")//black pawn selected
                                        {
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "P";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "br")//black rook selected
                                        {
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "R";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "bn")//black knight selected
                                        {
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "N";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "bb")//black bishop selected
                                        {
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "B";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "bq")//black queen selected
                                        {
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "Q";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "bk")//black king selected
                                        {
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "K";
                                            table[row, col] = ".";
                                        }

                                    }

                                    else if ((counter == -1) & (table[row, col].StartsWith('w') || table[row, col] == "."))//where the selected stone is placed for black pieces.
                                    {
                                        if (stone == "P")
                                        {
                                            if (row == 7)//promotion
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                stone = Promotion(row, col, cursorx, cursory, 3);
                                                table[row, col] = stone;
                                                colour += 1;
                                                counter *= -1;
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);
                                                Console.ResetColor();
                                                Console.Write(columns[start_y] + "1" + stone.Substring(1).ToUpper());
                                                SaveGame(columns[start_y] + "1" + stone.Substring(1).ToUpper(), 1);
                                            }
                                            else if (PawnBlack(start_x, start_y, row, col) == true)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.WriteLine("White's turn");
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write(columns[start_y] + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame(columns[start_y] + "x" + columns[col] + "" + rows[row], 1);
                                                }
                                                else
                                                {
                                                    if (start_x + 2 == row)
                                                    {
                                                        even = colour;
                                                    }
                                                    Console.Write(columns[col] + "" + rows[row]);
                                                    SaveGame(columns[col] + "" + rows[row], 1);
                                                }

                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "bp";
                                            }
                                            else if (start_x == 4 && colour == even + 2 && (table[start_x, start_y + 1] == "wp" || table[start_x, start_y - 1] == "wp") && start_x == row - 1 && (start_y == col + 1 || start_y == col - 1))//en passant
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("White's turn");
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);


                                                Console.Write(columns[start_y] + "x" + columns[col] + "" + rows[row]);
                                                SaveGame(columns[start_y] + "x" + columns[col] + "" + rows[row], 1);


                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "bp";

                                                Console.SetCursorPosition(cursorx, cursory - 2);
                                                table[row - 1, col] = ".";
                                                Console.ResetColor();
                                                Console.Write(".");

                                            }
                                            else
                                            {
                                                ErrorMessage("Pawn");
                                            }

                                        }
                                        else if (stone == "R")
                                        {
                                            if (RookBlack(start_x, start_y, row, col) == true && (start_x == row || start_y == col))
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.WriteLine("White's turn");
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write("R" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("R" + "x" + columns[col] + "" + rows[row], 1);
                                                }
                                                else
                                                {
                                                    Console.Write("R" + columns[col] + "" + rows[row]);
                                                    SaveGame("R" + columns[col] + "" + rows[row], 1);
                                                }
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "br";
                                            }
                                            else
                                            {
                                                ErrorMessage("Rock");
                                            }
                                        }
                                        else if (stone == "N")
                                        {
                                            if (KnightBlack(start_x, start_y, row, col) == true)
                                            {
                                                Console.SetCursorPosition(10, 0);

                                                Console.WriteLine("White's turn");
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write("N" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("N" + "x" + columns[col] + "" + rows[row], 1);
                                                }
                                                else
                                                {
                                                    Console.Write("N" + columns[col] + "" + rows[row]);
                                                    SaveGame("N" + columns[col] + "" + rows[row], 1);
                                                }
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "bn";
                                            }
                                            else
                                            {
                                                ErrorMessage("Knight");
                                            }
                                        }
                                        else if (stone == "B")
                                        {
                                            if (BishopBlack(start_x, start_y, row, col) == true && (start_x - row == start_y - col || start_x - row == col - start_y))
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.WriteLine("White's turn");
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write("B" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("B" + "x" + columns[col] + "" + rows[row], 1);
                                                }
                                                else
                                                {
                                                    Console.Write("B" + columns[col] + "" + rows[row]);
                                                    SaveGame("B" + columns[col] + "" + rows[row], 1);
                                                }
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "bb";
                                            }
                                            else
                                            {
                                                ErrorMessage("Bishop");
                                            }
                                        }
                                        else if (stone == "Q")
                                        {
                                            if (QueenBlack(start_x, start_y, row, col) == true)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.WriteLine("White's turn");
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write("Q" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("Q" + "x" + columns[col] + "" + rows[row], 1);
                                                }
                                                else
                                                {
                                                    Console.Write("Q" + columns[col] + "" + rows[row]);
                                                    SaveGame("Q" + columns[col] + "" + rows[row], 1);
                                                }
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "bq";
                                            }
                                            else
                                            {
                                                ErrorMessage("Queen");
                                            }
                                        }
                                        else if (stone == "K")
                                        {
                                            if (start_x == 0 && start_y == 4 && table[0, 7] == "br" && table[0, 6] == "." && table[0, 5] == "." && row == 0 && col == 6)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("White's turn");
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);

                                                Console.Write("O-O");//Kingside castling
                                                SaveGame("O-O", 1);

                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                Console.Write(stone);

                                                table[row, col] = "bk";
                                                Console.SetCursorPosition(cursorx - 4, cursory);
                                                Console.Write("R");
                                                table[row, col - 1] = "br";
                                                table[row, col + 1] = ".";
                                                Console.SetCursorPosition(cursorx + 4, cursory);
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.Write(".");
                                                colour += 1;
                                                counter *= -1;
                                                wkingx = row;
                                                wkingy = col;
                                            }
                                            else if (start_x == 0 && start_y == 4 && table[0, 0] == "br" && table[0, 1] == "." && table[0, 2] == "." && table[0, 3] == "." && row == 0 && col == 2)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("White's turn");
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);


                                                Console.Write("O-O-O");//Queenside castling
                                                SaveGame("O-O-O", 1);

                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);

                                                table[row, col] = "bk";
                                                Console.SetCursorPosition(cursorx + 4, cursory);
                                                Console.Write("R");
                                                table[row, col + 1] = "br";
                                                table[row, col - 2] = ".";
                                                Console.SetCursorPosition(cursorx - 8, cursory);
                                                Console.Write(".");
                                                counter *= -1;
                                                wkingx = row;
                                                wkingy = col;
                                            }

                                            else if (KingWhite(start_x, start_y, row, col) == true)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("White's turn");
                                                Console.SetCursorPosition(coordinatex + 10, coordinatey - 1);

                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write("K" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("K" + "x" + columns[col] + "" + rows[row], 1);
                                                }
                                                else
                                                {
                                                    Console.Write("K" + columns[col] + rows[row]);
                                                    SaveGame("K" + columns[col] + rows[row], 1);
                                                }
                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "bk";
                                                wkingx = row;
                                                wkingy = col;
                                            }
                                            else
                                            {
                                                ErrorMessage("King");
                                            }

                                        }
                                    }
                                }


                                if (colour % 4 == 0 || colour % 4 == 1)
                                {
                                    if (WhiteKing_check(wkingx, wkingy) == true)
                                    {
                                        wcheck++;
                                        Console.SetCursorPosition(20, 1);
                                        Console.WriteLine("White king check");
                                    }
                                    else if (WhiteKing_check(wkingx, wkingy) == false)
                                    {
                                        wcheck = 0;
                                        Console.SetCursorPosition(20, 1);
                                        Console.WriteLine("                  ");
                                    }

                                    if (((counter == 1) & (table[row, col] != ".")) & ((table[row, col] == "wp") || (table[row, col] == "wr") || (table[row, col] == "wn") || (table[row, col] == "wb") || (table[row, col] == "wq") || (table[row, col] == "wk")))//is the place where the desired stone to be played is determined for white.
                                    {
                                        Console.ResetColor();
                                        Console.SetCursorPosition(10, 0);
                                        Console.WriteLine("White's turn");
                                        Console.SetCursorPosition(cursorx, cursory);
                                        if (table[row, col] == "wp")//white pawn selected.
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "P";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "wr")//white rook selected
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "R";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "wn")//white knight selected
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "N";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "wb")//white bishop selected
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "B";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "wq")//white queen selected
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "Q";
                                            table[row, col] = ".";
                                        }
                                        else if (table[row, col] == "wk")//white king selected
                                        {
                                            Console.ForegroundColor = ConsoleColor.White;
                                            colour += 1;
                                            Console.Write(".");
                                            counter *= -1;
                                            start_x = row;
                                            start_y = col;
                                            stone = "K";
                                            table[row, col] = ".";
                                        }

                                    }

                                    else if ((counter == -1) & ((table[row, col] == "bp") || (table[row, col] == "br") || (table[row, col] == "bn") || (table[row, col] == "bb") || (table[row, col] == "bq") || (table[row, col] == "bk") || (table[row, col] == ".")))//where the selected stone is placed for white pieces.
                                    {

                                        if (stone == "P")
                                        {
                                            if (row == 0)//promotion
                                            {
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                stone = Promotion(row, col, cursorx, cursory, 1);
                                                table[row, col] = stone;
                                                colour += 1;
                                                counter *= -1;
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;
                                                coordinatey += 1;
                                                Console.ResetColor();
                                                Console.Write(counteer + ". " + columns[start_y] + "8" + stone.Substring(1).ToUpper());
                                                SaveGame(columns[start_y] + "8" + stone.Substring(1).ToUpper(), 0);
                                            }
                                            else if (PawnWhite(start_x, start_y, row, col) == true)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("Black's turn");
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write(counteer + ". " + columns[start_y] + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame(columns[start_y] + "x" + columns[col] + "" + rows[row], 0);
                                                }
                                                else
                                                {
                                                    if (start_x - 2 == row)
                                                    {
                                                        even = colour;
                                                    }
                                                    Console.Write(counteer + ". " + columns[col] + rows[row]);
                                                    SaveGame("" + columns[col] + rows[row], 0);
                                                }
                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "wp";
                                            }
                                            else if (start_x == 3 && colour == even + 2 && (table[start_x, start_y + 1] == "bp" || table[start_x, start_y - 1] == "bp") && start_x == row + 1 && (start_y == col + 1 || start_y == col - 1))//en passant
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("Black's turn");
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;

                                                Console.Write(counteer + ". " + columns[start_y] + "x" + columns[col] + "" + rows[row]);
                                                SaveGame(columns[start_y] + "x" + columns[col] + "" + rows[row], 0);

                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "wp";

                                                Console.SetCursorPosition(cursorx, cursory + 2);
                                                Console.ResetColor();
                                                table[row + 1, col] = ".";
                                                Console.Write(".");

                                            }
                                            else
                                            {
                                                ErrorMessage("Pawn");
                                            }

                                        }
                                        else if (stone == "R")
                                        {
                                            if (RookWhite(start_x, start_y, row, col) == true && (start_x == row || start_y == col))
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("Black's turn");
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write(counteer + ". " + "R" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("R" + "x" + columns[col] + "" + rows[row], 0);
                                                }
                                                else
                                                {
                                                    Console.Write(counteer + ". " + "R" + columns[col] + rows[row]);
                                                    SaveGame("R" + columns[col] + rows[row], 0);
                                                }
                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "wr";
                                            }
                                            else
                                            {
                                                ErrorMessage("Rock");
                                            }
                                        }
                                        else if (stone == "N")
                                        {
                                            if (KnightWhite(start_x, start_y, row, col) == true)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();

                                                Console.WriteLine("Black's turn");
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write(counteer + ". " + "N" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("N" + "x" + columns[col] + "" + rows[row], 0);
                                                }
                                                else
                                                {
                                                    Console.Write(counteer + ". " + "N" + columns[col] + rows[row]);
                                                    SaveGame("N" + columns[col] + rows[row], 0);
                                                }
                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "wn";
                                            }
                                            else
                                            {
                                                ErrorMessage("Knight");
                                            }
                                        }
                                        else if (stone == "B")
                                        {
                                            if (BishopWhite(start_x, start_y, row, col) == true && (start_x - row == start_y - col || start_x - row == col - start_y))
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("Black's turn");
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write(counteer + ". " + "B" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("B" + "x" + columns[col] + "" + rows[row], 0);
                                                }
                                                else
                                                {
                                                    Console.Write(counteer + ". " + "B" + columns[col] + rows[row]);
                                                    SaveGame("B" + columns[col] + rows[row], 0);
                                                }
                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "wb";
                                            }
                                            else
                                            {
                                                ErrorMessage("Bishop");
                                            }
                                        }
                                        else if (stone == "Q")
                                        {
                                            if (QueenWhite(start_x, start_y, row, col) == true)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("Black's turn");
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write(counteer + ". " + "Q" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("Q" + "x" + columns[col] + "" + rows[row], 0);
                                                }
                                                else
                                                {
                                                    Console.Write(counteer + ". " + "Q" + columns[col] + rows[row]);
                                                    SaveGame("Q" + columns[col] + rows[row], 0);
                                                }
                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "wq";
                                            }
                                            else
                                            {
                                                ErrorMessage("Queen");
                                            }
                                        }
                                        else if (stone == "K")
                                        {
                                            if (start_x == 7 && start_y == 4 && table[7, 7] == "wr" && table[7, 6] == "." && table[7, 5] == "." && row == 7 && col == 6)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("Black's turn");
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;
                                                Console.Write(counteer + ". " + "O-O");//Kingside castling
                                                SaveGame("O-O", 0);
                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                Console.Write(stone);

                                                table[row, col] = "wk";
                                                Console.SetCursorPosition(cursorx - 4, cursory);
                                                Console.Write("R");
                                                table[row, col - 1] = "wr";
                                                table[row, col + 1] = ".";
                                                Console.SetCursorPosition(cursorx + 4, cursory);
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.Write(".");
                                                colour += 1;
                                                counter *= -1;
                                                wkingx = row;
                                                wkingy = col;
                                            }
                                            else if (start_x == 7 && start_y == 4 && table[7, 0] == "wr" && table[7, 1] == "." && table[7, 2] == "." && table[7, 3] == "." && row == 7 && col == 2)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("Black's turn");
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;

                                                Console.Write(counteer + ". " + "O-O-O");//Queenside castling
                                                SaveGame("O-O-O", 0);

                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);

                                                table[row, col] = "wk";
                                                Console.SetCursorPosition(cursorx + 4, cursory);
                                                Console.Write("R");
                                                table[row, col + 1] = "wr";
                                                table[row, col - 2] = ".";
                                                Console.SetCursorPosition(cursorx - 8, cursory);
                                                Console.Write(".");
                                                counter *= -1;
                                                wkingx = row;
                                                wkingy = col;
                                            }

                                            else if (KingWhite(start_x, start_y, row, col) == true)
                                            {
                                                Console.SetCursorPosition(10, 0);
                                                Console.ResetColor();
                                                Console.WriteLine("Black's turn");
                                                Console.SetCursorPosition(coordinatex, coordinatey);
                                                counteer += 1;
                                                if (table[row, col] != ".")
                                                {
                                                    Console.Write(counteer + ". " + "K" + "x" + columns[col] + "" + rows[row]);
                                                    SaveGame("K" + "x" + columns[col] + "" + rows[row], 0);
                                                }
                                                else
                                                {
                                                    Console.Write(counteer + ". " + "K" + columns[col] + rows[row]);
                                                    SaveGame("K" + columns[col] + rows[row], 0);
                                                }
                                                coordinatey += 1;
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.SetCursorPosition(cursorx, cursory);

                                                colour += 1;
                                                Console.Write(stone);
                                                counter *= -1;
                                                table[row, col] = "wk";
                                                wkingx = row;
                                                wkingy = col;
                                            }
                                            else
                                            {
                                                ErrorMessage("King");
                                            }

                                        }

                                    }
                                    if (WhiteKing_check(wkingx, wkingy) == true && wcheck > 2)
                                    {
                                        Console.SetCursorPosition(40, 2);
                                        Console.WriteLine("Game over! Black wins!");
                                        gameover = true;
                                    }
                                    if (BlackKing_check(bkingx, bkingy) == true && bcheck > 2)
                                    {
                                        Console.SetCursorPosition(40, 2);
                                        Console.WriteLine("Game over! White wins!");
                                        gameover = true;
                                    }
                                }

                            }
                        }

                    }
                }
                else if (choise == 2)
                {
                    while (true)
                    {
                        Console.ResetColor();
                        Console.SetCursorPosition(3, 25);
                        Console.WriteLine("                          \n                  \n                       \n                        \n                  ");
                        Console.SetCursorPosition(3, 25);
                        Console.WriteLine("Enter notation");                                                                         //Ask user to input chess notation
                        string notation = Console.ReadLine();
                        Console.WriteLine(notation);
                        if (notation.Length == 2)                                                                                    //Control notation for pawn movement
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (coordinates[i, j].Equals(notation))
                                    {
                                        row = i;
                                        col = j;
                                        if (colour % 4 == 0 || colour % 4 == 1)
                                        {
                                            for (int k = 0; k < 8; k++)
                                            {
                                                if (table[k, col] == "wp")
                                                {
                                                    start_x = k;                                                                  //Taking initial coordinates
                                                    start_y = col;
                                                    break;
                                                }
                                            }
                                        }
                                        else if (colour % 4 == 2 || colour % 4 == 3)
                                        {
                                            for (int k = 7; k >= 0; k--)
                                            {
                                                if (table[k, col] == "bp")
                                                {
                                                    start_x = k;
                                                    start_y = col;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (notation.Length >= 3)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (coordinates[i, j].Equals(notation.Substring(1)) || coordinates[i, j].Equals(notation.Substring(2)) || coordinates[i, j].Equals(notation.Substring(0)))
                                    {
                                        row = i;
                                        col = j;
                                        if (colour % 4 == 0 || colour % 4 == 1)
                                        {
                                            if (columns.Contains(notation[0]) && notation.Contains('x') && table[row, col] != ".")                                            //Control notation for pawn capture
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wp" && PawnWhite(k, t, row, col) == true)
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'N')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wn" && KnightWhite(k, t, row, col) == true && table[row, col] == ".")                                   //Control notation for knight movement
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wn" && KnightWhite(k, t, row, col) == true && table[row, col] != ".")   //Control notation for knight capture
                                                        {
                                                            start_x = k;
                                                            start_y = t;

                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'R')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wr" && Rook(k, t, row, col) == true && table[row, col] == ".")                                       //Control notation for rook movement
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                            break;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wr" && Rook(k, t, row, col) == true && table[row, col] != ".")         //Control notation for rook capture
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'B')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wb" && BishopWhite(k, t, row, col) == true && table[row, col] == ".")                                //Control notation for bishop movement
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wb" && BishopWhite(k, t, row, col) == true && table[row, col] != ".")   //Control notation for bishop capture
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'Q')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wq" && (Rook(k, t, row, col) == true || BishopWhite(k, t, row, col) == true) && table[row, col] == ".")        //Control notation for queen movement
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wq" && (Rook(k, t, row, col) == true || BishopWhite(k, t, row, col) == true) && table[row, col] != ".")   //Control notation for queen capture
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'K')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wk" && KingWhite(k, t, row, col) == true && table[row, col] == ".")                                     //Control notation for king movement
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wk" && KingWhite(k, t, row, col) == true && table[row, col] != ".")      //Control notation for king capture
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (colour % 4 == 2 || colour % 4 == 3)
                                        {
                                            if (columns.Contains(notation[0]) && notation.Contains('x') && table[row, col] != ".")
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bp" && PawnBlack(k, t, row, col) == true)
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'N')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bn" && KnightBlack(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "bn" && KnightBlack(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'R')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "br" && Rook(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                            break;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "br" && Rook(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'B')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bb" && BishopBlack(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "bb" && BishopBlack(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'Q')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bq" && (Rook(k, t, row, col) == true || BishopWhite(k, t, row, col) == true) && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "bq" && (Rook(k, t, row, col) == true || BishopWhite(k, t, row, col) == true) && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'K')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bk" && KingBlack(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "bk" && KingBlack(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        cursorx = start_y * 4 + 8;                      //Set initial and final cursor positions to move chess pieces
                        cursory = start_x * 2 + 3;
                        cursorxx = col * 4 + 8;
                        cursoryy = row * 2 + 3;

                        while (true)
                        {
                            if (colour % 4 == 2 || colour % 4 == 3)
                            {
                                Console.ResetColor();

                                if ((counter == 1) & (table[start_x, start_y] != ".") & ((table[start_x, start_y] == "bp") || (table[start_x, start_y] == "br") || (table[start_x, start_y] == "bn") || (table[start_x, start_y] == "bb") || (table[start_x, start_y] == "bq") || (table[start_x, start_y] == "bk")))
                                {
                                    Console.SetCursorPosition(10, 0);
                                    Console.WriteLine("Black's turn");
                                    Console.SetCursorPosition(cursorx, cursory);
                                    if (table[start_x, start_y] == "bp")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "P";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "br")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "R";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "bn")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "N";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "bb")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "B";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "bq")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "Q";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "bk")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "K";
                                        table[start_x, start_y] = ".";
                                    }

                                }
                                else if ((counter == -1) & ((table[row, col] == "wp") || (table[row, col] == "wr") || (table[row, col] == "wn") || (table[row, col] == "wb") || (table[row, col] == "wq") || (table[row, col] == "wk") || (table[row, col] == ".")))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    if (stone == "P")
                                    {
                                        if (PawnBlack(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 1);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bp";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Pawn");
                                            table[start_x, start_y] = "bp";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "R")
                                    {
                                        if (RookBlack(start_x, start_y, row, col) == true && (start_x == row || start_y == col))
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 1);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "br";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Rock");
                                            table[start_x, start_y] = "br";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "N")
                                    {
                                        if (KnightBlack(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 1);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bn";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Knight");
                                            table[start_x, start_y] = "bn";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "B")
                                    {
                                        if (BishopBlack(start_x, start_y, row, col) == true && (start_x - row == start_y - col || start_x - row == col - start_y))
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 1);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bb";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Bishop");
                                            table[start_x, start_y] = "bb";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "Q")
                                    {
                                        if (QueenBlack(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 1);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bq";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Queen");
                                            table[start_x, start_y] = "bq";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "K")
                                    {
                                        if (KingBlack(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 1);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bk";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("King");
                                            table[start_x, start_y] = "bk";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                }
                                else break;
                            }
                            else if (colour % 4 == 0 || colour % 4 == 1)
                            {
                                Console.ResetColor();
                                if (((counter == 1) & (table[start_x, start_y] != ".")) & ((table[start_x, start_y] == "wp") || (table[start_x, start_y] == "wr") || (table[start_x, start_y] == "wn") || (table[start_x, start_y] == "wb") || (table[start_x, start_y] == "wq") || (table[start_x, start_y] == "wk")))
                                {
                                    Console.SetCursorPosition(10, 0);
                                    Console.WriteLine("White's turn");
                                    Console.SetCursorPosition(cursorx, cursory);
                                    if (table[start_x, start_y] == "wp")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "P";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wr")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "R";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wn")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "N";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wb")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "B";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wq")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "Q";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wk")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "K";
                                        table[start_x, start_y] = ".";
                                    }
                                }

                                else if ((counter == -1) & ((table[row, col] == "bp") || (table[row, col] == "br") || (table[row, col] == "bn") || (table[row, col] == "bb") || (table[row, col] == "bq") || (table[row, col] == "bk") || (table[row, col] == ".")))
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    if (stone == "P")
                                    {
                                        if (row == 7)
                                        {
                                            stone = Promotion(row, col, cursorx, cursory, 1);
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 0);
                                            colour += 1;
                                            //Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = stone;
                                            break;
                                        }
                                        if (PawnWhite(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 0);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wp";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Pawn");
                                            table[start_x, start_y] = "wp";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "R")
                                    {
                                        if (RookWhite(start_x, start_y, row, col) == true && (start_x == row || start_y == col))
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 0);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wr";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Rock");
                                            table[start_x, start_y] = "wr";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "N")
                                    {
                                        if (KnightWhite(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 0);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wn";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Knight");
                                            table[start_x, start_y] = "wn";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "B")
                                    {
                                        if (BishopWhite(start_x, start_y, row, col) == true && (start_x - row == start_y - col || start_x - row == col - start_y))
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 0);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wb";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Bishop");
                                            table[start_x, start_y] = "wb";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "Q")
                                    {
                                        if (QueenWhite(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 0);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wq";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Queen");
                                            table[start_x, start_y] = "wq";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "K")
                                    {
                                        if (KingWhite(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);
                                            SaveGame(notation, 0);
                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wk";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("King");
                                            table[start_x, start_y] = "wk";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                }
                                else break;
                            }
                            else break;
                        }
                    }
                }
                else if (choise == 3)
                {
                    //string[] lines = File.ReadAllLines("C:/Users/serda/OneDrive/Belgeler/c#/savedgame.txt");//Open and read chess file
                    string[] lines = File.ReadAllLines("savedgame.txt");//Open and read chess file
                    List<string> words = new List<string>();                                          //Create a list to hold notations in
                    for (int i = 0; i < lines.Length; i++)
                    {
                        foreach (string word in lines[i].Split(' '))                                  //Split notations and put them into the list
                        {
                            words.Add(word);
                        }
                    }
                    foreach (string word in words)
                    {
                        string notation = word;                                                       //Taking values from the list and control it
                        if (notation.Length == 2)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (coordinates[i, j].Equals(notation))
                                    {
                                        row = i;
                                        col = j;
                                        if (colour % 4 == 0 || colour % 4 == 1)
                                        {
                                            for (int k = 0; k < 8; k++)
                                            {
                                                if (table[k, col] == "wp")
                                                {
                                                    start_x = k;
                                                    start_y = col;
                                                }
                                            }
                                        }
                                        else if (colour % 4 == 2 || colour % 4 == 3)
                                        {
                                            for (int k = 7; k >= 0; k--)
                                            {
                                                if (table[k, col] == "bp")
                                                {
                                                    start_x = k;
                                                    start_y = col;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (notation.Length >= 3)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (coordinates[i, j].Equals(notation.Substring(1)) || coordinates[i, j].Equals(notation.Substring(2)))
                                    {
                                        row = i;
                                        col = j;
                                        if (colour % 4 == 0 || colour % 4 == 1)
                                        {
                                            if (columns.Contains(notation[0]) && notation.Contains('x') && table[row, col] != ".")
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wp" && PawnWhite(k, t, row, col) == true)
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'N')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wn" && KnightWhite(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wn" && KnightWhite(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;

                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'R')
                                            {
                                                int num = 0;
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (num == 0 && table[k, t] == "wr" && Rook(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                            num++;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wr" && Rook(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'B')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wb" && BishopWhite(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wb" && BishopWhite(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'Q')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wq" && (Rook(k, t, row, col) == true || BishopWhite(k, t, row, col) == true) && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wq" && (Rook(k, t, row, col) == true || BishopWhite(k, t, row, col) == true) && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'K')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "wk" && KingWhite(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "wk" && KingWhite(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (colour % 4 == 2 || colour % 4 == 3)
                                        {
                                            if (columns.Contains(notation[0]) && notation.Contains('x') && table[row, col] != ".")
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bp" && PawnBlack(k, t, row, col) == true)
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'N')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bn" && KnightBlack(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "bn" && KnightBlack(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'R')
                                            {
                                                int num = 0;
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (num == 0 && table[k, t] == "br" && Rook(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                            num++;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "br" && Rook(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'B')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bb" && BishopBlack(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "bb" && BishopBlack(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'Q')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bq" && (Rook(k, t, row, col) == true || BishopWhite(k, t, row, col) == true) && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "bq" && (Rook(k, t, row, col) == true || BishopWhite(k, t, row, col) == true) && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                            else if (notation[0] == 'K')
                                            {
                                                for (int k = 0; k < 8; k++)
                                                {
                                                    for (int t = 0; t < 8; t++)
                                                    {
                                                        if (table[k, t] == "bk" && KingBlack(k, t, row, col) == true && table[row, col] == ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                        else if (notation.Contains('x') && table[k, t] == "bk" && KingBlack(k, t, row, col) == true && table[row, col] != ".")
                                                        {
                                                            start_x = k;
                                                            start_y = t;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        cursorx = start_y * 4 + 8;
                        cursory = start_x * 2 + 3;
                        cursorxx = col * 4 + 8;
                        cursoryy = row * 2 + 3;

                        while (true)
                        {
                            if (colour % 4 == 2 || colour % 4 == 3)
                            {
                                Console.ResetColor();

                                if ((counter == 1) & (table[start_x, start_y] != ".") & ((table[start_x, start_y] == "bp") || (table[start_x, start_y] == "br") || (table[start_x, start_y] == "bn") || (table[start_x, start_y] == "bb") || (table[start_x, start_y] == "bq") || (table[start_x, start_y] == "bk")))
                                {
                                    Console.SetCursorPosition(10, 0);
                                    Console.WriteLine("Black's turn");
                                    Console.SetCursorPosition(cursorx, cursory);
                                    if (table[start_x, start_y] == "bp")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "P";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "br")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "R";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "bn")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "N";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "bb")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "B";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "bq")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "Q";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "bk")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "K";
                                        table[start_x, start_y] = ".";
                                    }

                                }
                                else if ((counter == -1) & ((table[row, col] == "wp") || (table[row, col] == "wr") || (table[row, col] == "wn") || (table[row, col] == "wb") || (table[row, col] == "wq") || (table[row, col] == "wk") || (table[row, col] == ".")))
                                {

                                    if (stone == "P")
                                    {
                                        if (PawnBlack(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bp";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Pawn");
                                            table[start_x, start_y] = "bp";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "R")
                                    {
                                        if (RookBlack(start_x, start_y, row, col) == true && (start_x == row || start_y == col))
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "br";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Rock");
                                            table[start_x, start_y] = "br";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "N")
                                    {
                                        if (KnightBlack(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bn";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Knight");
                                            table[start_x, start_y] = "bn";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "B")
                                    {
                                        if (BishopBlack(start_x, start_y, row, col) == true && (start_x - row == start_y - col || start_x - row == col - start_y))
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bb";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Bishop");
                                            table[start_x, start_y] = "bb";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "Q")
                                    {
                                        if (QueenBlack(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bq";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Queen");
                                            table[start_x, start_y] = "bq";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "K")
                                    {
                                        if (KingBlack(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.WriteLine("White's turn");
                                            WriteNotation(coordinatex + 10, coordinatey, notation, turn);
                                            coordinatey++;
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "bk";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("King");
                                            table[start_x, start_y] = "bk";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                }
                                else break;
                            }
                            else if (colour % 4 == 0 || colour % 4 == 1)
                            {
                                Console.ResetColor();
                                if (((counter == 1) & (table[start_x, start_y] != ".")) & ((table[start_x, start_y] == "wp") || (table[start_x, start_y] == "wr") || (table[start_x, start_y] == "wn") || (table[start_x, start_y] == "wb") || (table[start_x, start_y] == "wq") || (table[start_x, start_y] == "wk")))
                                {
                                    Console.SetCursorPosition(10, 0);
                                    Console.WriteLine("White's turn");
                                    Console.SetCursorPosition(cursorx, cursory);
                                    if (table[start_x, start_y] == "wp")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "P";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wr")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "R";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wn")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "N";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wb")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "B";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wq")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "Q";
                                        table[start_x, start_y] = ".";
                                    }
                                    else if (table[start_x, start_y] == "wk")
                                    {
                                        colour += 1;
                                        Console.Write(".");
                                        counter *= -1;
                                        stone = "K";
                                        table[start_x, start_y] = ".";
                                    }
                                }

                                else if ((counter == -1) & ((table[row, col] == "bp") || (table[row, col] == "br") || (table[row, col] == "bn") || (table[row, col] == "bb") || (table[row, col] == "bq") || (table[row, col] == "bk") || (table[row, col] == ".")))
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    if (stone == "P")
                                    {
                                        if (PawnWhite(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wp";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Pawn");
                                            table[start_x, start_y] = "wp";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "R")
                                    {
                                        if (RookWhite(start_x, start_y, row, col) == true && (start_x == row || start_y == col))
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wr";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Rock");
                                            table[start_x, start_y] = "wr";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "N")
                                    {
                                        if (KnightWhite(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wn";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Knight");
                                            table[start_x, start_y] = "wn";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "B")
                                    {
                                        if (BishopWhite(start_x, start_y, row, col) == true && (start_x - row == start_y - col || start_x - row == col - start_y))
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wb";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Bishop");
                                            table[start_x, start_y] = "wb";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "Q")
                                    {
                                        if (QueenWhite(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wq";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("Queen");
                                            table[start_x, start_y] = "wq";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                    else if (stone == "K")
                                    {
                                        if (KingWhite(start_x, start_y, row, col) == true)
                                        {
                                            Console.SetCursorPosition(10, 0);
                                            Console.ResetColor();
                                            Console.WriteLine("Black's turn");
                                            turn++;
                                            WriteNotation(coordinatex, coordinatey, notation, turn);
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.SetCursorPosition(cursorxx, cursoryy);

                                            colour += 1;
                                            Console.Write(stone);
                                            counter *= -1;
                                            table[row, col] = "wk";
                                            break;
                                        }
                                        else
                                        {
                                            ErrorMessage("King");
                                            table[start_x, start_y] = "wk";
                                            Console.SetCursorPosition(cursorx, cursory);
                                            Console.Write(stone);
                                            colour -= 1;
                                            counter *= -1;
                                            break;
                                        }
                                    }
                                }
                                else break;
                            }
                            else break;
                        }
                    }
                    choise = 1;                                                                           //Continue game
                }
            }
        }
    }
}