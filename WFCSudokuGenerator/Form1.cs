namespace WFCSudokuGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Tile[,] GenerateBoard()
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
                    Form1.ActiveForm.Controls.Add(button);
                    tiles[i2,i] = new Tile(new System.Numerics.Vector2(i2, i), button);
                    infoBox.Text += Environment.NewLine + $"New Tile: ({i2}|{i})";
                }
                temp = new Point(0, temp.Y + 45);
            }

            return tiles;
        }

        private void newBoard_Click(object sender, EventArgs e)
        {
            GenerateBoard();
        }
    }
}