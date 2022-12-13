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
        public Form1 form { get; private set; }
        //public Board board { get; private set; }
        public Vector2 position { get; private set; }
        public Button button { get; private set; }
        public State state { get; private set; } = State.Superposition;
        public List<int> possibleStates = new List<int> {1,2,3,4,5,6,7,8,9};
        public int entropy = 9;
        public int value;

        public Tile(Form1 form, Vector2 position, Button button)
        {
            this.form = form;
            this.position = position;
            this.button = button;
        }

        public void Collapse(Tile[,] tiles, int State = 0)
        {
            state = Tile.State.Superposition;
            value = possibleStates[new Random().Next(0, possibleStates.Count)];
            button.Text = value.ToString();
            Propagate(tiles);
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

            Vector2 temp = GetSquare();
            Square = form.board.GetSquare((int)temp.X, (int)temp.Y);

            //if (isDiagonal())
            //    Diagonal = board.GetDiagonal();

            foreach (Tile t in LineX)
            {
                t.possibleStates.Remove(value);
                t.entropy = t.possibleStates.Count;
            }


            foreach (Tile t in LineY)
            {
                t.possibleStates.Remove(value);
                t.entropy = t.possibleStates.Count;
            }

            foreach (Tile t in Square)
            {
                if (t != null)
                {
                    t.possibleStates.Remove(value);
                    t.entropy = t.possibleStates.Count;
                    t.button.BackColor = Color.Orange;
                }
            }



            //foreach (Tile t in Diagonal)
            //    t.possibleStates.Remove(value);
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
            Collapse(form.board.tiles);
        }

        Vector2 GetSquare()
        {
            float x = position.X / 3;
            float y = position.Y / 3;

            x = int.Parse(x.ToString()[0].ToString());
            y = int.Parse(y.ToString()[0].ToString());

            x += position.X % 3;
            y += position.Y % 3;

            return new Vector2(x, y);
        }

        public enum State
        {
            Superposition,
            Fixed
        }
    }
}
