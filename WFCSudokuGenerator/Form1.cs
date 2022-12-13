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
            infoBox.Text += "Generating Board...";
            Tile[,] tiles = new Tile[10, 10];
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
                    ActiveForm.Controls.Add(button);
                    tiles[i2,i] = new Tile(this,new System.Numerics.Vector2(i2, i), button);
                    button.Click += tiles[i2, i].PressButton;
                    infoBox.Text += Environment.NewLine + $"New Tile: ({i2}|{i})";
                }
                temp = new Point(0, temp.Y + 45);
            }

            return new Board(tiles);
        }

        private void newBoard_Click(object sender, EventArgs e)
        {
            board = GenerateBoard();
        }

        private void UpdateStates()
        {
           
        }

        public static void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}