using System;

namespace Maze
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Grid g = new Grid(20, 20);

            Console.WriteLine(Grid.Maze(g));
            Console.ReadLine();
        }

    }
}
