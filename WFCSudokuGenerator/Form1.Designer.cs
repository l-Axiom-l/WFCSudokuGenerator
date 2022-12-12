namespace WFCSudokuGenerator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backButton = new System.Windows.Forms.Button();
            this.startstopButton = new System.Windows.Forms.Button();
            this.collapseButton = new System.Windows.Forms.Button();
            this.infoBox = new System.Windows.Forms.TextBox();
            this.newBoard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(475, 90);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(77, 29);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            // 
            // startstopButton
            // 
            this.startstopButton.Location = new System.Drawing.Point(658, 90);
            this.startstopButton.Name = "startstopButton";
            this.startstopButton.Size = new System.Drawing.Size(94, 29);
            this.startstopButton.TabIndex = 1;
            this.startstopButton.Text = "Start/Stop";
            this.startstopButton.UseVisualStyleBackColor = true;
            // 
            // collapseButton
            // 
            this.collapseButton.Location = new System.Drawing.Point(558, 90);
            this.collapseButton.Name = "collapseButton";
            this.collapseButton.Size = new System.Drawing.Size(94, 29);
            this.collapseButton.TabIndex = 2;
            this.collapseButton.Text = "Collapse";
            this.collapseButton.UseVisualStyleBackColor = true;
            // 
            // infoBox
            // 
            this.infoBox.Location = new System.Drawing.Point(475, 125);
            this.infoBox.Multiline = true;
            this.infoBox.Name = "infoBox";
            this.infoBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.infoBox.Size = new System.Drawing.Size(277, 292);
            this.infoBox.TabIndex = 3;
            // 
            // newBoard
            // 
            this.newBoard.Location = new System.Drawing.Point(475, 55);
            this.newBoard.Name = "newBoard";
            this.newBoard.Size = new System.Drawing.Size(277, 29);
            this.newBoard.TabIndex = 4;
            this.newBoard.Text = "New Board";
            this.newBoard.UseVisualStyleBackColor = true;
            this.newBoard.Click += new System.EventHandler(this.newBoard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 474);
            this.Controls.Add(this.newBoard);
            this.Controls.Add(this.infoBox);
            this.Controls.Add(this.collapseButton);
            this.Controls.Add(this.startstopButton);
            this.Controls.Add(this.backButton);
            this.Name = "Form1";
            this.Text = "WaveFormCollapse Sudoku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button backButton;
        private Button startstopButton;
        private Button collapseButton;
        private TextBox infoBox;
        private Button newBoard;
    }
}