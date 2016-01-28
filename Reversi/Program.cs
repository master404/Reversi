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
            Player p = Player.White;
            Console.WriteLine(b);
            while(true)
            {
                Console.WriteLine("White Move: o ");
                Move m = new Move();
                Console.WriteLine("Введите координату x:");
                m.x=Int32.Parse(Console.ReadLine());
                Console.WriteLine("Введите координату y:");
                m.y = Int32.Parse(Console.ReadLine());
                b.PerformMove(p, m);
                Console.WriteLine("\n");
                Console.WriteLine(b);
                if(p==Player.White)p=Player.Black;
                else p=Player.White;
                Node max = AB.GetOptimalMove(p, b, 5);
                b.PerformMove(p, max.m);
                if (p == Player.White) p = Player.Black;
                else p = Player.White;
                Console.WriteLine("\n");
                Console.WriteLine(b);
            }

            


            /*
            Console.WriteLine(b);
            Console.WriteLine(b.Price(p));
            p = Player.White;
            Node max=AB.GetOptimalMove(p, b, 5);
            Console.WriteLine("x="+max.m.x+" y="+max.m.y+" Price="+max.max_price);*/
        }
    }
}
