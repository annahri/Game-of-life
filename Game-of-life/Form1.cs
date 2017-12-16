using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_of_life
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static Random GetRandom = new Random();
        static int res = 10;
        static int cols = 460 / res;
        static int rows = 320 / res;
        static int off = 0;
        static int generation = 0;
        static int[] moreZero = { 0, 0, 1 };

        public static int[,] grid = make2Darray(cols, rows);


        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 50;
            numericUpDown1.Value = timer1.Interval / 10;
            geneBox.Text = $"{generation}";
            
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            //Graphics graphics = mainPanel.CreateGraphics();
            //Pen pBlack = new Pen(Color.Black, 1);
            //for (int i = 0; i < cols; i++)
            //{
            //    for (int j = 0; j < rows; j++)
            //    {
            //        int x = i * res;
            //        int y = j * res;
                    
            //        graphics.DrawRectangle(pBlack, new Rectangle(x, y, res - off, res - off));
            //    }
            //}
        }
        
        // Functions
        static int[,] make2Darray(int cols, int rows)
        {
            int[,] array = new int[cols,rows];
            return array;
        }
        static void updateDraw(Control obj)
        {
            Graphics graphics = obj.CreateGraphics();
            //Pen pBlack = new Pen(Color.Black, 1);
            SolidBrush bBlack = new SolidBrush(Color.Black),
                       bWhite = new SolidBrush(Color.White); ;
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int x = i * res;
                    int y = j * res;

                    if (grid[i, j] == 1)
                        graphics.FillRectangle(bBlack, new Rectangle(x, y, res - off, res - off));
                    else
                        graphics.FillRectangle(bWhite, new Rectangle(x, y, res - off, res - off));

                    //graphics.DrawRectangle(pBlack, new Rectangle(x, y, res - off, res - off));
                }
            }
            graphics.Dispose();
            //pBlack.Dispose();
            bBlack.Dispose();
            bWhite.Dispose();
        }

        static void beginGrowth(Control obj)
        {
            Graphics graphics = obj.CreateGraphics();
            //Pen pBlack = new Pen(Color.Gray, 1);
            SolidBrush bBlack = new SolidBrush(Color.Black),
                       bWhite = new SolidBrush(Color.White);
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int x = i * res;
                    int y = j * res;

                    if (grid[i, j] == 1)
                        graphics.FillRectangle(bBlack, new Rectangle(x, y, res - off, res - off));
                    else
                        graphics.FillRectangle(bWhite, new Rectangle(x, y, res - off, res - off));

                    //graphics.DrawRectangle(pBlack, new Rectangle(x, y, res - off, res - off));
                }
            }

            var newGrid = make2Darray(cols, rows);
            // Compute newGrid based on grid
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    int state = grid[i, j];

                    // Calculate neighbors
                    var neighbors = countNeighs(grid, i, j, cols, rows);
                    if (state == 0 && neighbors == 3)
                        newGrid[i, j] = 1;
                    else if (state == 1 && (neighbors < 2 || neighbors > 3))
                        newGrid[i, j] = 0;
                    else
                        newGrid[i, j] = state;

                }
            }

            grid = newGrid;

            bBlack.Dispose();
            bWhite.Dispose();
            graphics.Dispose();
        }

        static int[,] populate(int[,] _2Darray)
        {
            
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    grid[i, j] = moreZero[GetRandom.Next(moreZero.Length)];
                }
            }
            return _2Darray;
        }

        static int[,] zeroFill(int[,] _2Darray)
        {
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    grid[i, j] = 0;
                }
            }
            return _2Darray;
        }

        static int countNeighs(int[,] grid, int x, int y, int cols, int rows)
        {
            int sum = 0;
            if (x == 0)
            {
                if (y == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            sum += grid[x + i, y + j];
                        }
                    }
                }
                else if (y == rows -1)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = -1; j < 1; j++)
                        {
                            sum += grid[x + i, y + j];
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            sum += grid[x + i, y + j];
                        }
                    }
                }
            }
            else if (x == cols-1)
            {
                if (y == 0)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            sum += grid[x + i, y + j];
                        }
                    }
                }
                else if (y == rows -1)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        for (int j = -1; j < 1; j++)
                        {
                            sum += grid[x + i, y + j];
                        }
                    }
                }
                else
                {
                    for (int i = -1; i < 1; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            sum += grid[x + i, y + j];
                        }
                    }
                }
            }
            else if (y == 0)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        sum += grid[x + i, y + j];
                    }
                }
            }
            else if (y == rows -1)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 1; j++)
                    {
                        sum += grid[x + i, y + j];
                    }
                }
            }
            else
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        sum += grid[x + i, y + j];
                    }
                }
            }

            sum -= grid[x, y];
            return sum;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            geneBox.Text = $"{generation++}";
            beginGrowth(mainPanel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            zeroFill(grid);
            populate(grid);
            updateDraw(mainPanel);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            generation = 0;
            startButton.Enabled = false;
            button1.Enabled = false;
            restartButton.Enabled = true;
            timer1.Enabled = true;
        }

        private void geneBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                generation = 0;
                zeroFill(grid);
                timer1.Stop();
                timer1.Enabled = false;
                startButton.Enabled = true;
                button1.Enabled = true;
                restartButton.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = (int) numericUpDown1.Value * 10;
        }

        
    }
}
