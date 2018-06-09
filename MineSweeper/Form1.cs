using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        int ROWS = 16;
        int COLUMNS = 16;
        int mines = 0;

        public Form1()
        {
            InitializeComponent();

            // Create the minesweeper board
            CreateBoard();
        }

        private void setMines()
        {
            Random rand = new Random();

            foreach (MineButton currentSquare in flowLayoutPanel1.Controls)
            {
                if (rand.Next(0, 7) == 4)
                {
                    currentSquare.mine = true;

                    mines++;
                }
            }
        }

        private void calculateSquareMineCount()
        {
            foreach (MineButton currentButton in flowLayoutPanel1.Controls)
            {
                if (currentButton.mine)
                {
                    foreach (MineButton checkButton in flowLayoutPanel1.Controls)
                    {
                        if (!checkButton.mine)
                        {
                            if ((checkButton.x + 1 == currentButton.x) && (checkButton.y == currentButton.y))
                            {
                                checkButton.mineCount++;
                            }

                            if ((checkButton.x - 1 == currentButton.x) && (checkButton.y == currentButton.y))
                            {
                                checkButton.mineCount++;
                            }

                            if ((checkButton.x == currentButton.x) && (checkButton.y + 1 == currentButton.y))
                            {
                                checkButton.mineCount++;
                            }

                            if ((checkButton.x == currentButton.x) && (checkButton.y - 1 == currentButton.y))
                            {
                                checkButton.mineCount++;
                            }

                            if ((checkButton.x + 1 == currentButton.x) && (checkButton.y + 1 == currentButton.y))
                            {
                                checkButton.mineCount++;
                            }

                            if ((checkButton.x - 1 == currentButton.x) && (checkButton.y - 1 == currentButton.y))
                            {
                                checkButton.mineCount++;
                            }

                            if ((checkButton.x - 1 == currentButton.x) && (checkButton.y + 1 == currentButton.y))
                            {
                                checkButton.mineCount++;
                            }

                            if ((checkButton.x + 1 == currentButton.x) && (checkButton.y - 1 == currentButton.y))
                            {
                                checkButton.mineCount++;
                            }
                        }

                        if (checkButton.mineCount > 0)
                        {
                            if (checkButton.mineCount == 1)
                            {
                                checkButton.ForeColor = Color.Blue;
                            }
                            else if (checkButton.mineCount == 2)
                            {
                                checkButton.ForeColor = Color.Green;
                            }
                            else if (checkButton.mineCount == 3)
                            {
                                checkButton.ForeColor = Color.Red;
                            }
                            else if (checkButton.mineCount == 4)
                            {
                                checkButton.ForeColor = Color.DarkBlue;
                            }
                            else if (checkButton.mineCount == 5)
                            {
                                checkButton.ForeColor = Color.Brown;
                            }
                            else if (checkButton.mineCount == 6)
                            {
                                checkButton.ForeColor = Color.Teal;
                            }
                            else if (checkButton.mineCount == 8)
                            {
                                checkButton.ForeColor = Color.Gray;
                            }

                            checkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        }
                    }
                }
            }
        }

        private void CreateBoard()
        {
            for(int currentRow = 0; currentRow + 1 < ROWS; currentRow++)
            {
                for (int currentColumn = 0; currentColumn + 1 < COLUMNS; currentColumn++)
                {
                    this.mineButton = new System.Windows.Forms.MineButton(currentRow, currentColumn);

                    this.mineButton.Name = currentRow.ToString() + currentColumn.ToString();
                    this.mineButton.Size = new System.Drawing.Size(20, 20);
                    this.mineButton.UseVisualStyleBackColor = true;
                    this.mineButton.Click += new System.EventHandler(this.ButtonClick);
                    this.mineButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.flagSquare);

                    this.flowLayoutPanel1.Controls.Add(this.mineButton);
                }
            }

            setMines();
            calculateSquareMineCount();
        }

        private bool gameComplete()
        {
            bool gameComplete = true;

            foreach (MineButton currentSquare in flowLayoutPanel1.Controls)
            {
                if(!currentSquare.mine && currentSquare.Enabled)
                {
                    gameComplete = false;

                    break;
                }
            }

            return gameComplete;
        }

        private void flagSquare(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MineButton clickedButton = sender as MineButton;

                if(clickedButton.flagged)
                {
                    clickedButton.BackColor = default(Color);

                    clickedButton.flagged = false;
                }
                else
                {
                    clickedButton.BackColor = Color.Red;

                    clickedButton.flagged = true;
                }

                if (gameComplete())
                {
                    MessageBox.Show("You Win!");
                }
            }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            MineButton clickedButton = sender as MineButton;

            if (!clickedButton.flagged)
            {
                int clickedButtonX = clickedButton.x;
                int clickedButtonY = clickedButton.y;

                if (clickedButton.mine)
                {
                    MessageBox.Show("Game over!");

                    flowLayoutPanel1.Visible = false;
                }
                else if (clickedButton.mineCount > 0)
                {
                    clickedButton.Enabled = false;

                    clickedButton.Text = clickedButton.mineCount.ToString();
                }
                else
                {
                    clickedButton.Enabled = false;

                    foreach (MineButton c in flowLayoutPanel1.Controls)
                    {
                        MineButton currentButton = c;

                        // UP CLICK
                        if ((currentButton.x + 1 == clickedButton.x) && (currentButton.y == clickedButton.y) && !currentButton.mine)
                        {
                            if (c.mineCount == 0)
                            {
                                c.PerformClick();

                                c.Enabled = false;
                            }
                            else
                            {
                                c.Enabled = false;

                                currentButton.Text = currentButton.mineCount.ToString();
                            }
                        }

                        // DOWN CLICK
                        if ((currentButton.x - 1 == clickedButton.x) && (currentButton.y == clickedButton.y) && !currentButton.mine)
                        {
                            if (c.mineCount == 0)
                            {
                                c.PerformClick();

                                c.Enabled = false;
                            }
                            else
                            {
                                c.Enabled = false;

                                currentButton.Text = currentButton.mineCount.ToString();
                            }
                        }

                        // RIGHT CLICK
                        if ((currentButton.x == clickedButton.x) && (currentButton.y - 1 == clickedButton.y) && !currentButton.mine)
                        {
                            if (c.mineCount == 0)
                            {
                                c.PerformClick();

                                c.Enabled = false;
                            }
                            else
                            {
                                c.Enabled = false;

                                currentButton.Text = currentButton.mineCount.ToString();
                            }
                        }

                        // LEFT CLICK
                        if ((currentButton.x == clickedButton.x) && (currentButton.y + 1 == clickedButton.y) && !currentButton.mine)
                        {
                            if (c.mineCount == 0)
                            {
                                c.PerformClick();

                                c.Enabled = false;
                            }
                            else
                            {
                                c.Enabled = false;

                                currentButton.Text = currentButton.mineCount.ToString();
                            }
                        }

                        if ((currentButton.x + 1 == clickedButton.x) && (currentButton.y + 1 == clickedButton.y) && !currentButton.mine)
                        {
                            if (c.mineCount == 0)
                            {
                                c.PerformClick();

                                c.Enabled = false;
                            }
                            else
                            {
                                c.Enabled = false;

                                currentButton.Text = currentButton.mineCount.ToString();
                            }
                        }

                        if ((currentButton.x - 1 == clickedButton.x) && (currentButton.y - 1 == clickedButton.y) && !currentButton.mine)
                        {
                            if (c.mineCount == 0)
                            {
                                c.PerformClick();

                                c.Enabled = false;
                            }
                            else
                            {
                                c.Enabled = false;

                                currentButton.Text = currentButton.mineCount.ToString();
                            }
                        }

                        if ((currentButton.x - 1 == clickedButton.x) && (currentButton.y + 1 == clickedButton.y) && !currentButton.mine)
                        {
                            if (c.mineCount == 0)
                            {
                                c.PerformClick();

                                c.Enabled = false;
                            }
                            else
                            {
                                c.Enabled = false;

                                currentButton.Text = currentButton.mineCount.ToString();
                            }

                        }

                        if ((currentButton.x + 1 == clickedButton.x) && (currentButton.y - 1 == clickedButton.y) && !currentButton.mine)
                        {
                            if (c.mineCount == 0)
                            {
                                c.PerformClick();

                                c.Enabled = false;
                            }
                            else
                            {
                                c.Enabled = false;

                                currentButton.Text = currentButton.mineCount.ToString();
                            }
                        }
                    }
                }
            }

            if (gameComplete())
            {
                MessageBox.Show("You Win!");
            }
        }
    }
}