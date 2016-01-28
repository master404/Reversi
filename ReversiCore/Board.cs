using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiCore
{
    public enum Player
    {
        White, Black
    }

    public enum Cell
    {
        Empty, White, Black
    }

    public struct Move {
        public int x, y;
    }

    public class Board
    {
        Cell[,] board;

        int[,] combinations = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 }, { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 }};
        public Board()
        {
            board = new Cell[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    board[i, j] = Cell.Empty;
            board[3, 3] = board[4, 4] = Cell.White;
            board[3, 4] = board[4, 3] = Cell.Black;
        }

        public Board(Board b)
        {
            board = b.board.Clone() as Cell[,];
        }

        public override string ToString()
        {
            var res = new StringBuilder();
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++)
                    if (board[i, j] == Cell.Empty)
                        res.Append(" . ");
                    else if (board[i, j] == Cell.Black)
                        res.Append(" * ");
                    else
                        res.Append(" o ");
                res.AppendLine();
            }
            return res.ToString();
        }

        public bool IsMoveCorrect(Player p, Move m)
        {
            if (board[m.x, m.y] != Cell.Empty)
                return false;
            else
            {
                if (p == Player.White)
                    for (int i = 0; i < 8; i++)
                    {
                        try
                        {
                            if (board[m.x + combinations[i, 0], m.y + combinations[i, 1]] == Cell.Black)
                            {
                                for (int j = 2; j < 7; j++)
                                {
                                    if (m.x + j * combinations[i, 0] > 7 || m.y + j * combinations[i, 1] > 7)
                                    { break; }
                                    if (board[m.x + j * combinations[i, 0], m.y + j * combinations[i, 1]] == Cell.Empty)
                                    { break; }
                                    if (board[m.x + j * combinations[i, 0], m.y + j * combinations[i, 1]] == Cell.White)
                                    { return true; }
                                }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        { }
                    }
                else
                {
                    for (int i = 0; i < 8; i++)
                    {
                        try
                        {
                            if (board[m.x + combinations[i, 0], m.y + combinations[i, 1]] == Cell.White)
                            {
                                for (int j = 2; j < 7; j++)
                                {
                                    if (m.x + j * combinations[i, 0] > 7 || m.y + j * combinations[i, 1] > 7)
                                    { break; }
                                    if (board[m.x + j * combinations[i, 0], m.y + j * combinations[i, 1]] == Cell.Empty)
                                    { break; }
                                    if (board[m.x + j * combinations[i, 0], m.y + j * combinations[i, 1]] == Cell.Black)
                                    { return true; }
                                }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        { }
                    }
                }
            }
            return false;
        }

        public void PerformMove(Player p, Move m)
        {
            if (IsMoveCorrect(p, m))
                if (p == Player.Black)
                {
                    board[m.x, m.y] = Cell.Black;

                    for (int i = 0; i < 8; i++)
                    {
                        try
                        {
                            if (board[m.x + combinations[i, 0], m.y + combinations[i, 1]] == Cell.White)
                                {
                                    for (int j = 2; j < 7; j++)
                                    {
                                        if (m.x + j * combinations[i, 0] > 7 || m.y + j * combinations[i, 1] > 7)
                                        { break; }
                                        if (board[m.x + j * combinations[i, 0], m.y + j * combinations[i, 1]] == Cell.Empty)
                                        { break; }
                                        if (board[m.x + j * combinations[i, 0], m.y + j * combinations[i, 1]] == Cell.Black)
                                        { for (int k = 1; k < j; k++) { board[m.x + k * combinations[i, 0], m.y + k * combinations[i, 1]] = Cell.Black; } break; }
                                    }
                                }
                        }
                        catch (IndexOutOfRangeException)
                        { }
                    }
                }
                else if (p == Player.White)
                {
                    board[m.x, m.y] = Cell.White;
                    for (int i = 0; i < 8; i++)
                    {
                        try
                        {
                            if (board[m.x + combinations[i, 0], m.y + combinations[i, 1]] == Cell.Black)
                            {
                                for (int j = 2; j < 7; j++)
                                {
                                    if (m.x + j * combinations[i, 0] > 7 || m.y + j * combinations[i, 1] > 7)
                                    { break; }
                                    if (board[m.x + j * combinations[i, 0], m.y + j * combinations[i, 1]] == Cell.Empty)
                                    { break; }
                                    if (board[m.x + j * combinations[i, 0], m.y + j * combinations[i, 1]] == Cell.White)
                                    { for (int k = 1; k < j; k++) { board[m.x + k * combinations[i, 0], m.y + k * combinations[i, 1]] = Cell.White; } break; }
                                }
                            }
                        }
                        catch (IndexOutOfRangeException)
                        { }
                    }
                }
                else
                    throw new ArgumentException();
        }

        public List<Move> GetMoves(Player p)
        {
            List<Move> moves = new List<Move>();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++) {
                    var m = new Move{x=i, y=j};
                    if (IsMoveCorrect(p, m))
                        moves.Add(m);
                }
            return moves;
        }

        public int Price(Player p)
        {
            int price = 0; 
            if (p == Player.White)
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i, j] == Cell.White) price++;
                    }
            else
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        if (board[i, j] == Cell.Black) price++;
                    }
            }
            return price;
            throw new NotImplementedException();
        }
    }
}
