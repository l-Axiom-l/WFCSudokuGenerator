using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            return new Tile[1];
        }

        /// <summary>
        /// Gets the Square by the bottom right corner tile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile[] GetSquare(int x, int y)
        {
            Tile[] temp = new Tile[9];
            tiles[x, y].button.BackColor = Color.Blue;
            for (int i = 0; i < 3; i++)
                for (int i2 = 0; i2 < 3; i2++)
                {
                    temp[i + i2] = tiles[x + i, y - i2];
                }
            return temp;
        }
    }
}
