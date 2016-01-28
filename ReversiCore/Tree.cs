using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiCore
{
    public struct Node
    {
        public Move m;
        public int max_price;
    }

    public class AB
    {
        public static Node GetOptimalMove(Player p, Board b, int max_depth)//max_depth = 5
        {
            int depth = max_depth;
            Node max = Go_Round(p, b, max_depth,p,depth);
            return max;
        }
        public static Node Go_Round(Player p, Board b, int max_depth,Player p1,int depth)
        {
          //  if (p == p1) Console.WriteLine("yes where max=" + max_depth + "\n");
            List<Node> prices=new List<Node>();
            Node max_price=new Node();
            List <Move> moves = b.GetMoves(p);
            int max = 0, min = 100 ;
            foreach (Move move in moves)
            {
                if (max_depth > 0)
                {
                    Node node=new Node();
                    Board new_board = new Board(b);
                    new_board.PerformMove(p, move);
                    if (p == Player.Black) { node = Go_Round(Player.White, new_board, (max_depth - 1), p1, depth); }
                    else { node = Go_Round(Player.Black, new_board, (max_depth - 1), p1, depth); }
                    prices.Add(node);
                    if (p == p1)
                    {
                        //Console.WriteLine("yes where max=" + max_depth + "\n");
                       // foreach(Node n in prices)
                       // {
                            if (node.max_price > max)
                            {
                                max_price.max_price = node.max_price;
                                max_price.m = move;
                                max = node.max_price; 
                            }
                     }
                  //  }
                    else
                    {
                        //Console.WriteLine("no where max=" + max_depth + "\n");
                        //foreach (Node n in prices)
                       // {
                            if (node.max_price < min)
                            {
                                max_price.max_price = node.max_price;
                                max_price.m = move;
                                min = node.max_price;
                            }
                       // }
                    }

                }
                else if (max_depth==0)
                {
                    if (p == p1) { }//Console.WriteLine("p=p1");
                    else Console.WriteLine("p!!!!!!!!!!p1");
                    max_price.max_price=b.Price(p);
                    max_price.m = move;
                    //Console.WriteLine(b.Price(p));
                }
            }
           /* if (max_depth == depth)
            {
                Console.WriteLine("MLength=" + moves.Count);
                foreach (Node pr in prices)
                {
                    Console.WriteLine("Price=" + pr.max_price + " where x="+pr.m.x+" y="+pr.m.y+"\n");
                }
            }*/
            return max_price; 
        }

    }
}
