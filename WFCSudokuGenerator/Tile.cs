using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WFCSudokuGenerator
{
    public class Tile
    {
        public Board board { get; private set; }
        public Vector2 position { get; private set; }
        public Button button { get; private set; }
        public State state { get; private set; } = State.Superposition;
        public List<int> possibleStates = new List<int> {1,2,3,4,5,6,7,8,9};

        public Tile(Board board, Vector2 position, Button button)
        {
            this.board = board;
            this.position = position;
            this.button = button;
        }

        public void Collapse(Tile[,] tiles, int State)
        {

        }

        public void Propagate(Tile[,] tiles)
        {
            Tile[] LineX = new Tile[9];
            Tile[] LineY = new Tile[9];
            Tile[] Diagonal = new Tile[9];
            Tile[] Square = new Tile[9];

            for (int i = 0; i < 9; i++)
            {
                LineX[i] = tiles[i, (int)(position.Y)];
                LineY[i] = tiles[(int)(position.Y), i];
            }

            if (isDiagonal())
                Diagonal = board.GetDiagonal();
        }

        bool isDiagonal()
        {
            if (position.X / position.Y < 9 && position.X + position.Y == 10)
                return true;
            else
                return false;
        }

        public void PressButton(object sender, EventArgs e)
        {

        }

        public enum State
        {
            Superposition,
            Fixed
        }
    }
}
