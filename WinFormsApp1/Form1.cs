namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Image card;
        Point position = new Point(400, 400);
        bool dragging;
        Rectangle rect;
        int height = 200, width = 200;


        public Form1()
        {
            InitializeComponent();

            card = Image.FromFile("Triangle.png");
            rect = new Rectangle(position.X, position.Y, width, height);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePosition = new Point(e.X, e.Y);
            if (rect.Contains(mousePosition))
            {
                dragging = true;                
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(dragging)
            {
                position.X = e.X - (width / 2);
                position.Y = e.Y - (height / 2);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if(dragging)
            {
                dragging = false;
                rect.Location = new Point(e.X, e.Y);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /*Pen outline;
            if(dragging)
            {
                outline = new Pen(Color.Yellow, 6);
            }
            else
            {
                outline = new Pen(Color.Plum, 6);
            }

            e.Graphics.DrawRectangle(outline, rect);*/
            e.Graphics.DrawImage(card, position.X, position.Y, width, height);
        }

        private void FormTimer_Tick(object sender, EventArgs e)
        {
            rect.X = position.X;
            rect.Y = position.Y;

            this.Invalidate();
        }
    }
}
