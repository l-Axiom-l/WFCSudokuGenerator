using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WFCSudokuGenerator
{
    public partial class Form1 : Form
    {
        public Board board;

        EventHandler collapseEvent;
        EventHandler backEvent;
        EventHandler stopEvent;
        EventHandler exportEvent;

        public Form1()
        {
            InitializeComponent();
        }

        public Board GenerateBoard()
        {
            if (board != null)
                DeleteBoard();

            infoBox.Text += "Generating Board...";
            Tile[,] tiles = new Tile[9, 9];
            Point temp = new Point(0, 18);
            for (int i = 0; i < 9; i++)
            {
                for (int i2 = 0; i2 < 9; i2++)
                {
                    Button button = new Button();
                    temp = new Point(temp.X + 45, temp.Y);
                    button.Location = temp;
                    button.Text = "0";
                    button.Size = new Size(40,40);
                    button.BackColor= Color.Gray;
                    ActiveForm.Controls.Add(button);
                    tiles[i2,i] = new Tile(this,new System.Numerics.Vector2(i2, i), button);
                    tiles[i2, i].e = (x, y) => TileInfo(button);
                    button.Click += tiles[i2, i].e;
                    infoBox.Text += Environment.NewLine + $"New Tile: ({i2}|{i})";
                }
                temp = new Point(0, temp.Y + 45);
            }

            return new Board(tiles, this);
        }

        private void newBoard_Click(object sender, EventArgs e)
        {
            board = GenerateBoard();

            collapseEvent = (x,y) => board.Collapse();
            backEvent = (x,y) => board.Load(board.log.Count - 5);
            stopEvent = (x, y) => board.StartStop();
            exportEvent = (x, y) => board.Export(saveFileDialog);

            collapseButton.Click += collapseEvent;
            backButton.Click += backEvent;
            startstopButton.Click += stopEvent;
            exportButton.Click += exportEvent;
        }

        void DeleteBoard()
        {
            board.waveFormRunning = false;

            try
            {
                board.canceled = true;
                Debug.WriteLine(board.waveForm.IsAlive);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            foreach (Tile t in board.tiles)
            {
                t.button.Click -= t.e;
                this.Controls.Remove(t.button);
            }

            collapseButton.Click -= collapseEvent;
            backButton.Click -= backEvent;
            startstopButton.Click -= stopEvent;
            exportButton.Click -= exportEvent;
            board = null;
        }

        void TileInfo(Button b)
        {
            Tile[] temp = new Tile[board.tiles.Length];
            temp = board.tiles.Cast<Tile>().ToArray();
            Tile t = temp.Where(x => x.button == b).ElementAt(0);

            string Info = Environment.NewLine + $"Position:{t.position}\r\nPossible States:{string.Join('-', t.possibleStates)}\r\nEntropy:{t.entropy}\r\nEntropyReduction:{t.entropyReduction}";
            infoBox.Text += Info;
        }
    }
}