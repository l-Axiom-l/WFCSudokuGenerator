using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFCSudokuGenerator
{
    public class Board
    {
        public Tile[,] tiles;
        public Board(Tile[,] tiles)
        {
            this.tiles = tiles;
        }

        public Tile[] GetDiagonal()
        {

        }

        public Tile[] GetSquare(int x, int y)
        {

        }
    }
}
