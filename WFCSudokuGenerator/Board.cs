using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace WFCSudokuGenerator
{
    public class Board
    {
        public Form1 form { get; private set; }
        public Tile[,] tiles;
        public List<string> log = new List<string>();
        public bool waveFormRunning = false;
        public bool stop = true;
        public Thread waveForm;
        public Thread waveForm2;

        public Board(Tile[,] tiles, Form1 form)
        {
            this.tiles = tiles;
            this.form = form;
            waveForm = new Thread(WaveformProcess);
            waveForm2 = new Thread(WaveformProcess);
            waveForm.Start();
            waveForm2.Start();
        }

        public async void WaveformProcess()
        {
            while(true)
            {
                if (!waveFormRunning)
                    continue;

                if(stop)
                {
                    await Task.Delay(250);
                    continue;
                }

                Tile[] temp = new Tile[tiles.Length];
                temp = tiles.Cast<Tile>().ToArray();

                if(temp.All(x => x.state == Tile.State.Fixed))
                {
                    WaveFormCollapsed();
                    return;
                }
                
                Collapse();

                if(temp.Any(x => x.entropy == 0))
                {
                    stop = true;
                    Debug.WriteLine(log.Count);
                    //log.RemoveRange(log.Count - 11, 10);
                    await Task.Run(() => Load(log.Count - 5));
                    stop = false;
                }

                await Task.Delay(80);
            }
        }

        public void StartStop(bool wfr = true)
        {
            waveFormRunning = wfr;

            if (waveFormRunning)
                stop = !stop;
        }

        public void Collapse()
        {
            log.Add(Save());
            foreach (Tile tile in tiles)
                tile.calculateEntropyReduction(tiles);

            Tile[] temp = new Tile[tiles.Length];
            temp = tiles.Cast<Tile>().ToArray();

            Tile[] test = temp.Where(x => x.state == Tile.State.Superposition && x.entropy == 1).ToArray();
            
            if(test.Length > 0)
            {
                test[new Random().Next(0, test.Length)].Collapse(tiles);
                return;
            }

            temp = temp.Where(x => x.state == Tile.State.Superposition && x.entropy > 0).ToArray();
            temp = temp.OrderByDescending(x => x.entropy).ToArray();
            temp = temp.TakeLast(20).OrderByDescending(x => x.entropyReduction).ToArray();
            temp = temp.TakeLast(new Random().Next(1, 15)).OrderByDescending(x => x.entropyReduction).ToArray();
            temp = temp.Concat(temp.TakeLast(5).ToArray()).ToArray();

            if (new Random().Next(0, 100) > 90)
                temp[new Random().Next(0, temp.Length)].Collapse(tiles);
            else
                temp[new Random().Next(0, temp.Length)].Collapse(tiles);
        }

        public async void Load(int index)
        {
            foreach (Tile tile in this.tiles)
                form.Invoke(new Action(() => form.Controls.Remove(tile.button)));

            string main = log[index];
            string[] temp = main.Split(Environment.NewLine);
            Tile[,] tiles = new Tile[9, 9];

            foreach(string s in temp)
            {
                string[] stemp = s.Split('-');
                Tile tile = new Tile(form, new System.Numerics.Vector2(), new Button());
                form.Invoke(new Action(() => tile.Load(stemp)));
                tiles[(int)tile.position.X, (int)tile.position.Y] = tile;
                form.Invoke(new Action(() => form.Controls.Add(tile.button)));
                //form.Invoke(new Action(() => tile.button.Click += tile.PressButton));
            }
            this.tiles = tiles;
        }

        public string Save()
        {
            //State:1-Entropy:9-PossibleStates:{}-Value:0-Position:()-ButtonPosition:()-ButtonSize()
            string main = "";
            foreach(var tile in tiles)
            {
                string state = $"State:{tile.state}";
                string entropy = $"Entropy:{tile.entropy}";
                string PossibleStates = $"PossibleStates:{String.Join(',', tile.possibleStates.Select(x => x.ToString()))}";
                string Value = $"Value:{tile.value.ToString()}";
                string Position = $"Position:{tile.position.X}|{tile.position.Y}";
                string ButtonPosition = $"ButtonPosition:{tile.button.Location.X}|{tile.button.Location.Y}";
                string ButtonSize = $"ButtonSize:{tile.button.Size.Width}|{tile.button.Size.Height}";

                string solution = $"{state}-{entropy}-{PossibleStates}-{Value}-{Position}-{ButtonPosition}-{ButtonSize}" + Environment.NewLine;
                main += solution;
            }
            main = main.TrimEnd('\n');
            return main;
        }

        public Tile[] GetDiagonal(int diagonal)
        {
            List<Tile> result = new List<Tile>();

            switch(diagonal)
            {
                case 0:
                    for (int i = 0; i < 9; i++)
                        result.Add(tiles[i, i]);

                    for (int i = 0; i < 9; i++)
                        result.Add(tiles[8 - i, i]);
                    break;

                case 1:
                    for (int i = 0; i < 9; i++)
                        result.Add(tiles[i,i]);
                    break;
                case 2:
                    for (int i = 0; i < 9; i++)
                        result.Add(tiles[8 - i, i]);
                    break;

                case 3: return new Tile[] { };
            }
            return result.ToArray();
        }

        public void WaveFormCollapsed()
        {
            waveFormRunning = false;
            foreach(var tile in tiles) { tile.button.BackColor = Color.Azure; tile.button.ForeColor = Color.Blue; }
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
            int count = 0;
            for (int i = 0; i < 3; i++)
                for (int i2 = 0; i2 < 3; i2++)
                {
                    temp[count] = tiles[x - i, y - i2];
                    count++;
                }
            return temp;
        }
    }
}
