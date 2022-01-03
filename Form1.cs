namespace Snake
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Game snake;
        const int wwo = 16;
        const int who = 39;

        private void Form1_Shown(object sender, EventArgs e)
        {
            Graphics device = this.CreateGraphics();
            snake = new Game(device, this.Width - wwo, this.Height - who);
            snake.StartGame();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (snake.Running == false)
            {
                snake.StartGame();
            }
            else
            {

                if (e.KeyCode == Keys.Right)
                {
                    snake.ChangeDirection(Game.Directions.Right);
                }
                else if (e.KeyCode == Keys.Left)
                {
                    snake.ChangeDirection(Game.Directions.Left);
                }
                else if (e.KeyCode == Keys.Up)
                {
                    snake.ChangeDirection(Game.Directions.Up);

                }
                else if (e.KeyCode == Keys.Down)
                {
                    snake.ChangeDirection(Game.Directions.Down);
                }
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            snake.StopGameLoop();
        }
    }
}