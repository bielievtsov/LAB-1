using System;
using System.Threading;

namespace Lab_1_LodeRunner
{
    
class Program
    {
        struct Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Icon { get; set; }
        public Cell(int x, int y, char Icon)
        {
            X = x;
            Y = y;
            this.Icon = Icon;
        }
        public void DrawCell()
        {
            Console.Write(Icon);
        }
    }
        static void Main(string[] args)
        {
        int Gold = 0;
            int x_block = 0, y_block = 0;
        Cell[,] gameSpace = new Cell[7, 15];        
        void Fill()
        {
            for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        gameSpace[i, j] = new Cell(i, j, ' ');
                    }
                }
            for (int i = 0; i < 7; i++)
                {
                for (int j = 0; j < 15; j++)
                {
                    gameSpace[3, 11] = new Cell(3, 11, '@');
                    gameSpace[5, 6] = new Cell(5, 6, '@');
                    gameSpace[2, 3] = new Cell(2, 3, '|');
                    gameSpace[3, 3] = new Cell(3, 3, '|');
                    gameSpace[4, 10] = new Cell(4, 10, '|');
                    gameSpace[5, 10] = new Cell(4, 10, '|');
                    gameSpace[4, j] = new Cell(4, j, '#');
                    gameSpace[2, j] = new Cell(2, j, '#');
                    gameSpace[0, j] = new Cell(0, j, '#');
                    gameSpace[6, j] = new Cell(6, j, '#');
                    gameSpace[i, 0] = new Cell(i, 0, '#');
                    gameSpace[i, 14] = new Cell(i, 14, '#');
                }
            }
        }
        Fill();
        Cell Move(int x1, int y1, ConsoleKeyInfo keyInfo)
             {
                Cell hero1 = new Cell(x1, y1, 'I');
                try
                {                    
                    if (gameSpace[hero1.Y + 1, hero1.X].Icon == ' ')
                    {
                        while (gameSpace[hero1.Y + 1, hero1.X].Icon != '#')
                        {
                            hero1.X = hero1.X;
                            hero1.Y++;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.Spacebar && gameSpace[hero1.Y + 1, hero1.X].Icon == '#')
                    {
                        gameSpace[hero1.Y + 1, hero1.X] = new Cell(hero1.Y, hero1.X, ' ');
                        x_block = hero1.X;
                        y_block = hero1.Y + 1;
                    }
                    if (keyInfo.Key == ConsoleKey.RightArrow && gameSpace[hero1.Y, hero1.X + 1].Icon != '#')
                    {
                        if (gameSpace[hero1.Y, hero1.X + 1].Icon == '@')
                        {
                            Gold++;
                            hero1.X++;
                            hero1.Y = hero1.Y;
                            gameSpace[hero1.Y, hero1.X] = new Cell(hero1.Y, hero1.X, ' ');
                        }
                        else
                        {
                            hero1.X++;
                            hero1.Y = hero1.Y;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.LeftArrow && gameSpace[hero1.Y, hero1.X - 1].Icon != '#')
                    {
                        if (gameSpace[hero1.Y, hero1.X + 1].Icon == '@')
                        {
                            Gold++;
                            hero1.X--;
                            hero1.Y = hero1.Y;
                            gameSpace[hero1.Y, hero1.X] = new Cell(hero1.Y, hero1.X, ' ');
                        }
                        else
                        {
                            hero1.X--;
                            hero1.Y = hero1.Y;
                        }

                    }
                    if (keyInfo.Key == ConsoleKey.DownArrow && gameSpace[hero1.Y + 1, hero1.X].Icon != '#')
                    {
                        if (gameSpace[hero1.Y, hero1.X + 1].Icon == '@')
                        {
                            Gold++;
                            hero1.X = hero1.X;
                            hero1.Y++;
                            gameSpace[hero1.Y, hero1.X] = new Cell(hero1.Y, hero1.X, ' ');
                        }
                        else
                        {
                            hero1.X = hero1.X;
                            hero1.Y++;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.UpArrow && gameSpace[hero1.Y - 1, hero1.X].Icon != '#')
                    {
                        if (gameSpace[hero1.Y, hero1.X + 1].Icon == '@')
                        {
                            Gold++;
                            hero1.X = hero1.X;
                            hero1.Y--;
                            gameSpace[hero1.Y, hero1.X] = new Cell(hero1.Y, hero1.X, ' ');
                        }
                        else
                        {
                            hero1.X = x1;
                            hero1.Y = y1 - 1;
                        }
                    }
                }
                catch
                {
                    
                }
                return hero1;
             }
        void Draw(Cell player)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {

                        if (i == player.Y && j == player.X)
                            player.DrawCell();
                        else
                        {
                            Console.Write(gameSpace[i, j].Icon);
                        }
                    }
                    Console.Write("\n");
                }
            }
        Cell hero = new Cell(3, 5, 'I');
        void Recreate(object x)
            {
                gameSpace[y_block, x_block] = new Cell(x_block, y_block, '#');
            }
        TimerCallback t = new TimerCallback(Recreate);
        Timer timer4 = new Timer(t, null, 0, 1500);
        while( true )
            {
                
                var keyInfo = Console.ReadKey();
                Console.Clear();
                hero = (Move(hero.X, hero.Y, keyInfo));
                Draw(hero);
                Console.WriteLine($"The nummber of taken gold is {Gold}");
            }
        }    
    }
}