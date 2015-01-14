using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer keyManagerTimer = new System.Windows.Forms.Timer();
        List<PictureBox> list=new List<PictureBox>();
        List<double> xcoord = new List<double>();
        List<double> ycoord = new List<double>();
        List<double> degree = new List<double>();
        List<double> xdistance = new List<double>();
        List<double> ydistance = new List<double>();
        int currentshot=0;
        double range = 250;

        public Form1()
        {
            InitializeComponent();

            this.keyManagerTimer.Tick += (s, e) => Movement();
            this.keyManagerTimer.Interval = 25;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Focus();
            this.KeyPreview = true;
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData & Keys.Right) != 0)
            {
                keyManagerTimer.Enabled = true;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Movement()
        {
            bool isDownKeyPressed = IsKeyPressed(Keys.S);
            bool isRightKeyPressed = IsKeyPressed(Keys.D);
            bool isUpKeyPressed = IsKeyPressed(Keys.W);
            bool isLeftKeyPressed = IsKeyPressed(Keys.A);

            //Case for Down and Right
            if (isRightKeyPressed && !isDownKeyPressed)
            {
                if (pictureBox1.Left < 1080 )  pictureBox1.Left += 8;
            }
            if (!isRightKeyPressed && isDownKeyPressed)
            {
                if (pictureBox1.Top < 720) pictureBox1.Top += 8;
            }
            if (isRightKeyPressed && isDownKeyPressed)
            {
                if (pictureBox1.Top <720) pictureBox1.Top += 8;
                if (pictureBox1.Left < 1080)  pictureBox1.Left += 8;
            }

            //Case for Right and Up
            if (isRightKeyPressed && !isUpKeyPressed)
            {
                if (pictureBox1.Left <1080) pictureBox1.Left += 8;
            }
            if (!isRightKeyPressed && isUpKeyPressed)
            {
                if (pictureBox1.Top > 0) pictureBox1.Top -= 8;
            }
            if (isRightKeyPressed && isUpKeyPressed)
            {
                if (pictureBox1.Top > 0) pictureBox1.Top -= 8;
                if (pictureBox1.Left < 1080) pictureBox1.Left += 8;
            }

            //Case for Left and Up
            if (isLeftKeyPressed && !isUpKeyPressed)
            {
                if (pictureBox1.Left > 0) pictureBox1.Left -= 8;
            }
            if (!isLeftKeyPressed && isUpKeyPressed)
            {
                if (pictureBox1.Top > 0) pictureBox1.Top -= 8;
            }
            if (isLeftKeyPressed && isUpKeyPressed)
            {
                if (pictureBox1.Top > 0) pictureBox1.Top -= 8;
                if (pictureBox1.Left > 0) pictureBox1.Left -= 8;
            }

            //Case for Left and Up
            if (isLeftKeyPressed && !isDownKeyPressed)
            {
                if (pictureBox1.Left > 0) pictureBox1.Left -= 8;
            }
            if (!isLeftKeyPressed && isDownKeyPressed)
            {
                if (pictureBox1.Top < 720) pictureBox1.Top += 8;
            }
            if (isLeftKeyPressed && isDownKeyPressed)
            {
                if (pictureBox1.Top < 720) pictureBox1.Top += 8;
                if (pictureBox1.Left > 0) pictureBox1.Left -= 8;
            }
        }

        //Slow Movement and NO Possiblity for KeyDown if another key is already held
        //void Form1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode==Keys.Right || e.KeyCode==Keys.D)
        //    {
        //        if (pictureBox1.Left < 708)
        //        {
        //            pictureBox1.Left += 8;
        //        }
        //    }
        //    
        //    if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
        //    {
        //        if (pictureBox1.Left > 0)
        //        {
        //            pictureBox1.Left -= 8;
        //        }
        //    }
        //
        //    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
        //    {
        //         if (pictureBox1.Top > 0)
        //         {
        //             pictureBox1.Top -= 8;
        //         }
        //    }
        //
        //    if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
        //    {
        //        if (pictureBox1.Top < 580)
        //        {
        //            pictureBox1.Top += 8;
        //        }
        //    }
        //    
        //    if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S && e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
        //    {
        //        if (pictureBox1.Top < 580)  pictureBox1.Top += 8;
        //        if (pictureBox1.Left > 0)   pictureBox1.Left -= 8;
        //
        //    }
        //}


        private void Shoot()
        {

            

        }
        public static bool IsKeyPressed(Keys key)
        {
            return BitConverter.GetBytes(GetKeyState((int)key))[1] > 0;
        }

        [DllImport("user32")]
        private static extern short GetKeyState(int vKey);

        private void bullets_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox bullet=new PictureBox();
                bullet.Enabled = true;
                bullet.Visible = true;
                bullet.Height = 43;
                bullet.Width = 41;
                bullet.Location = new Point(pictureBox1.Left, pictureBox1.Top);
                bullet.Image = new Bitmap(@"C:\Users\Jason\Pictures\fireball.png");
                bullet.SizeMode = PictureBoxSizeMode.StretchImage;
                Controls.Add(bullet);
                bullet.BringToFront();

                list.Add(bullet);
                xcoord.Add(this.PointToClient(Cursor.Position).X - pictureBox1.Left);
                ycoord.Add(-1*(this.PointToClient(Cursor.Position).Y - pictureBox1.Top));
                degree.Add(Math.Atan2(ycoord[currentshot], xcoord[currentshot]) * 180 / Math.PI);
                if (Cursor.Position.X >= pictureBox1.Left)
                {
                    xdistance.Add(range * Math.Cos(degree[currentshot]));
                }
                else xdistance.Add(-(range * Math.Cos(degree[currentshot])));
                
                ydistance.Add(range * Math.Sin(degree[currentshot]));
                label1.Text = Convert.ToString( degree[currentshot]);
                label2.Text = Convert.ToString(ydistance[currentshot]);
                
                shooting.Enabled = true;
                currentshot++;
            }

        }

        private void shooting_Tick(object sender, EventArgs e)
        {

            //label2.Text = Convert.ToString(range*Math.Cos(degree[currentshot]));  
            shooting.Enabled = false;       
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void moveshot_Tick(object sender, EventArgs e)
        {


            moveshot.Enabled = false;
        }

    }
}
