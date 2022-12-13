using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WFCSudokuGenerator
{
    public class Tile
    {
        public Form1 form { get; private set; }
        public Vector2 position { get; private set; }
        public Button button { get; private set; }
        public State state { get; private set; } = State.Superposition;
        public List<int> possibleStates = new List<int> {1,2,3,4,5,6,7,8,9};
        public int entropy = 9;
        public int value = 0;
        public int entropyReduction;

        public Tile(Form1 form, Vector2 position, Button button)
        {
            this.form = form;
            this.position = position;
            this.button = button;
        }

        public void Collapse(Tile[,] tiles, int State = 0)
        {
            state = Tile.State.Fixed;
            try
            {
                value = possibleStates[new Random().Next(0, possibleStates.Count)];
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex + ":" + value);
            }

            form.Invoke(() => button.Text = value.ToString());
            form.Invoke(() => button.BackColor = Color.Aquamarine);
            form.Invoke(() => button.Enabled = false);
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
                LineY[i] = tiles[(int)(position.X), i];
            }

            Vector2 temp = GetSquare();
            Square = form.board.GetSquare((int)(temp.X * 3 - 1), (int)(temp.Y * 3 - 1));

            Diagonal = form.board.GetDiagonal(isDiagonal());

            foreach (Tile t in LineX)
            {
                t.possibleStates.Remove(value);
                t.Update();
            }


            foreach (Tile t in LineY)
            {
                t.possibleStates.Remove(value);
                t.Update();
            }

            foreach (Tile t in Square)
            {
                if (t != null)
                {
                    t.possibleStates.Remove(value);
                    t.Update();
                }
            }

            if(Diagonal.Length > 0)
                foreach (Tile t in Diagonal)
                {
                    t.possibleStates.Remove(value);
                    Update();
                }

        }

        int isDiagonal()
        {
            if (position.X + position.Y == 8 && position.X / position.Y == 1)
                return 0;
            else if (position.X / position.Y == 1)
                return 1;
            else if (position.X + position.Y == 8)
                return 2;
            else return 3;

        }

        public void PressButton(object sender, EventArgs e)
        {
            Collapse(form.board.tiles);
        }

        Vector2 GetSquare()
        {
            float x = (position.X + 1) / 3;
            float y = (position.Y + 1) / 3;

            x = int.Parse(x.ToString()[0].ToString());
            y = int.Parse(y.ToString()[0].ToString());

            x += (position.X + 1) % 3 == 0 ? 0 : 1;
            y += (position.Y + 1) % 3 == 0 ? 0 : 1;

            return new Vector2(x, y);
        }

        public void Update()
        {
            if (state == State.Fixed)
                return;

            entropy = possibleStates.Count;

            if (entropy == 0)
            {
                form.Invoke(() => button.BackColor = Color.DarkRed);
                //form.Invoke(() => button.Enabled = false);
            }
            else
            {
                form.Invoke(() => button.BackColor = Color.Gray);
                //form.Invoke(() => button.Enabled = true);
            }
        }

        public void Load(string[] temp)
        {
            foreach (string st in temp)
            {
                string[] notagain = st.Split(":");
                switch (notagain[0])
                {
                    case "State":
                        state = notagain[1] == "Fixed" ? Tile.State.Fixed : Tile.State.Superposition;
                        break;
                    case "Entropy":
                        entropy = int.Parse(notagain[1]);
                        break;
                    case "PossibleStates":
                        try
                        {
                            if (notagain[1].Length < 1)
                            {
                                break;
                            }

                            string[] list = notagain[1].Split(',');
                            list = list.Select(x => x.Trim()).ToArray();
                            possibleStates = list.Select(x => int.Parse(x)).ToList();
                        }
                        catch(Exception ex)
                        {
                            Debug.WriteLine(notagain[1]);
                        }

                        break;
                    case "Value":
                        value = int.Parse(notagain[1]);
                        break;
                    case "Position":
                        string[] vector = notagain[1].Split('|');
                        vector = vector.Select(x => x.Trim()).ToArray();
                        position = new Vector2(float.Parse(vector[0]), float.Parse(vector[1]));
                        break;
                    case "ButtonPosition":
                        string[] vector2 = notagain[1].Split('|');
                        vector2 = vector2.Select(x => x.Trim()).ToArray();
                        form.Invoke(() => button.Location = new Point(int.Parse(vector2[0]), int.Parse(vector2[1])));
                        break;
                    case "ButtonSize":
                        string[] size = notagain[1].Split('|');
                        size = size.Select(x => x.Trim()).ToArray();
                        form.Invoke(() => button.Size = new Size(int.Parse(size[0]), int.Parse(size[1])));
                        break;
                }
            }

            button.Text = value.ToString();

            if (entropy == 0)
            {
                button.BackColor = Color.DarkRed;
                //button.Enabled = false;
            }
            else
            {
                button.BackColor = Color.Gray;
                //button.Enabled = true;
            }

            if(state == State.Fixed)
            {
                button.BackColor = Color.Aquamarine;
                //button.Enabled = false;
            }
        }

        public void calculateEntropyReduction(Tile[,] tiles)
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
            Square = form.board.GetSquare((int)(temp.X * 3 - 1), (int)(temp.Y * 3 - 1));
            Diagonal = form.board.GetDiagonal(isDiagonal());

            Tile[] simArray = LineX.Concat(LineY).Concat(Diagonal).Concat(Square).Distinct().ToArray();
            simArray = simArray.Where(x => x.state != State.Fixed).ToArray();
            entropyReduction = simArray.Length;
        }

        public enum State
        {
            Superposition,
            Fixed
        }
    }
}
