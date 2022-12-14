using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WFCSudokuGenerator
{
    public partial class Form1 : Form
    {
        public Board board;

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
            Point temp = new Point(0, 40);
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
                    //button.MouseDoubleClick += tiles[i2, i].PressButton;
                    button.Click += (x, y) => TileInfo(button);
                    infoBox.Text += Environment.NewLine + $"New Tile: ({i2}|{i})";
                }
                temp = new Point(0, temp.Y + 45);
            }

            return new Board(tiles, this);
        }

        private void newBoard_Click(object sender, EventArgs e)
        {
            board = GenerateBoard();
            collapseButton.Click += (x,y) => board.Collapse();
            backButton.Click += (x,y) => board.Load(board.log.Count - 5);
            startstopButton.Click += (x, y) => board.StartStop();
        }

        private void UpdateStates()
        {
           
        }

        void DeleteBoard()
        {
            board.waveFormRunning = false;

            try
            {
                board.waveForm.Interrupt();
                board.waveForm2.Interrupt();
            }
            catch(Exception ex)
            {

            }

            foreach(Tile t in board.tiles)
            {
                this.Controls.Remove(t.button);
            }

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