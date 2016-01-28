using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReversiCore;


namespace Reversi
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board();
            Player p=Player.White;
            //var m2 = new Move { x = 4, y = 2 };
            //b.PerformMove(p, m2);
            Console.WriteLine(b);
            Console.WriteLine(b.Price(p));
            Node max=AB.GetOptimalMove(p, b, 4);
            Console.WriteLine("x="+max.m.x+" y="+max.m.y+" Price="+max.max_price);
        }
    }
}
