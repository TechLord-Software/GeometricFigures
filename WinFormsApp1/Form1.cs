using Microsoft.VisualBasic.ApplicationServices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Image currentGeo;
        Point currentPosition = new Point(400, 400);
        bool dragging;
        Rectangle currentRect;
        int height = 100, width = 100;


        public Form1()
        {
            InitializeComponent();    
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePosition = new Point(e.X, e.Y);
            if (currentRect.Contains(mousePosition))
            { 
                    currentRect = new Rectangle(currentPosition.X, currentPosition.Y, width, height);
                    dragging = true;
            }
            
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                currentPosition.X = e.X - (width / 2);
                currentPosition.Y = e.Y - (height / 2);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                dragging = false;
                currentRect.Location = new Point(e.X, e.Y);                
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (currentGeo != null)
            {
                e.Graphics.DrawImage(currentGeo, currentPosition.X, currentPosition.Y, width, height);

            }
        }

        private void FormTimer_Tick(object sender, EventArgs e)
        {
            currentRect.X = currentPosition.X;
            currentRect.Y = currentPosition.Y;

            this.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentPosition = new Point(400, 400);
            currentGeo = pictureBox1.BackgroundImage;
            currentRect = new Rectangle(currentPosition.X, currentPosition.Y, width, height);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentPosition = new Point(400, 400);
            currentGeo = pictureBox2.BackgroundImage;
            currentRect = new Rectangle(currentPosition.X, currentPosition.Y, width, height);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentPosition = new Point(400, 400);
            currentGeo = pictureBox3.BackgroundImage;
            currentRect = new Rectangle(currentPosition.X, currentPosition.Y, width, height);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentPosition = new Point(400, 400);
            currentGeo = pictureBox4.BackgroundImage;
            currentRect = new Rectangle(currentPosition.X, currentPosition.Y, width, height);
        }
    }
}
