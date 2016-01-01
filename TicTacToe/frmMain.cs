/*
 *  Tic Tac Toe
 *  (c) Afaan Bilal, AMX Infinity!
 */


using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        char[,] v;
        char who;
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            v = new char[3, 3];
            Reset();
            EnableButtons(false);
        }

        private void EnableButtons(bool e = true)
        {
            b11.Enabled = b12.Enabled = b13.Enabled = b21.Enabled = b22.Enabled = b23.Enabled = b31.Enabled = b32.Enabled = b33.Enabled = e;
        }

        private void bClick(object sender, EventArgs e)
        {
            Button b = sender as Button;
            
            if (b.Text != string.Empty)
                return;

            b.Text = who.ToString();

            switch(b.Name)
            {
                case "b11":
                    v[0, 0] = who;
                    break;

                case "b12":
                    v[0, 1] = who;
                    break;

                case "b13":
                    v[0, 2] = who;
                    break;

                case "b21":
                    v[1, 0] = who;
                    break;

                case "b22":
                    v[1, 1] = who;
                    break;

                case "b23":
                    v[1, 2] = who;
                    break;

                case "b31":
                    v[2, 0] = who;
                    break;

                case "b32":
                    v[2, 1] = who;
                    break;

                case "b33":
                    v[2, 2] = who;
                    break;
            }

            if (CheckIfWon(who))
            {
                hadWon = true;
                this.Paint += FrmMain_Paint;
                this.Refresh();
                               
                string Player = (who == 'X') ? "1" : "2";
                MessageBox.Show("Congratulations, Player " + Player + " (" + who.ToString() + ") has won!", "TicTacToe");
                EnableButtons(false);
                //Reset();
                return;
            }

            if (CheckIfDraw())
            {
                MessageBox.Show("Sorry, the game is over as a draw!", "TicTacToe");
                Reset();
                return;
            }

            who = (who == 'X') ? 'O' : 'X';
            lblp1.Enabled = (who == 'X');
            lblp2.Enabled = (who == 'O');
        }

        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.Green);
            p.Width = 10f;
            p.ScaleTransform(2, 2);
            Point[] pts = new Point[3];
            byte[] ptTypes = new byte[3]
                {   (byte)System.Drawing.Drawing2D.PathPointType.Line,
                        (byte)System.Drawing.Drawing2D.PathPointType.Line,
                        (byte)System.Drawing.Drawing2D.PathPointType.Line };

            for (int i = 0; i < 3; i++)
            {
                pts[i].X = (int)(winBtns[i].Location.X + (winBtns[i].Size.Width * 0.5));
                pts[i].Y = (int)(winBtns[i].Location.Y + (winBtns[i].Size.Height * 0.5));

                winBtns[i].BackColor = Color.Green;
            }

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath(pts, ptTypes);
            e.Graphics.DrawPath(p, path);

            this.Paint -= FrmMain_Paint;
        }

        private bool CheckIfDraw()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (v[i, j] == ' ')
                        return false;

            return true;
        }
        
        Button[] winBtns;
        bool hadWon;

        private bool CheckIfWon(char who)
        {
            winBtns = new Button[3];
            bool win = false;

            if (v[0, 0] == who && v[0, 1] == who && v[0, 2] == who)
            {
                winBtns[0] = b11;
                winBtns[1] = b12;
                winBtns[2] = b13;
                win = true;
            }

            if (v[1,0] == who && v[1,1] == who && v[1,2] == who)
            {
                winBtns[0] = b21;
                winBtns[1] = b22;
                winBtns[2] = b23;
                win = true;
            }

            if (v[2,0] == who && v[2,1] == who && v[2,2] == who)
            {
                winBtns[0] = b31;
                winBtns[1] = b32;
                winBtns[2] = b33;
                win = true;
            }

            if (v[0, 0] == who && v[1, 0] == who && v[2, 0] == who)
            {
                winBtns[0] = b11;
                winBtns[1] = b21;
                winBtns[2] = b31;
                win = true;
            }

            if (v[0, 1] == who && v[1, 1] == who && v[2, 1] == who)
            {
                winBtns[0] = b12;
                winBtns[1] = b22;
                winBtns[2] = b32;
                win = true;
            }

            if (v[0, 2] == who && v[1, 2] == who && v[2, 2] == who)
            {
                winBtns[0] = b13;
                winBtns[1] = b23;
                winBtns[2] = b33;
                win = true;
            }

            if (v[0, 0] == who && v[1, 1] == who && v[2, 2] == who)
            {
                winBtns[0] = b11;
                winBtns[1] = b22;
                winBtns[2] = b33;
                win = true;
            }

            if (v[0,2] == who && v[1,1] == who && v[2,0] == who)
            {
                winBtns[0] = b13;
                winBtns[1] = b22;
                winBtns[2] = b31;
                win = true;
            }

            return win;
        }

        private void Reset()
        {
            who = 'X';

            b11.Text = b12.Text = b13.Text = b21.Text = b22.Text = b23.Text = b31.Text = b32.Text = b33.Text = string.Empty;
            EnableButtons();

            if (hadWon)
            {
                this.Paint += FrmMain_Paint1;
                this.Refresh();
            }

            lblp1.Enabled = true;
            lblp2.Enabled = false;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    v[i,j] = ' ';
        }

        private void FrmMain_Paint1(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.FromKnownColor(KnownColor.Control));
            p.Width = 10f;
            p.ScaleTransform(2, 2);
            Point[] pts = new Point[3];
            byte[] ptTypes = new byte[3]
                {   (byte)System.Drawing.Drawing2D.PathPointType.Line,
                        (byte)System.Drawing.Drawing2D.PathPointType.Line,
                        (byte)System.Drawing.Drawing2D.PathPointType.Line };

            for (int i = 0; i < 3; i++)
            {
                pts[i].X = (int)(winBtns[i].Location.X + (winBtns[i].Size.Width * 0.5));
                pts[i].Y = (int)(winBtns[i].Location.Y + (winBtns[i].Size.Height * 0.5));

                winBtns[i].BackColor = Color.FromKnownColor(KnownColor.ControlLight);
            }

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath(pts, ptTypes);
            e.Graphics.DrawPath(p, path);

            this.Paint -= FrmMain_Paint1;
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
