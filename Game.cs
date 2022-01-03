using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Snake
{
    internal class Game
    {

        const int PartSize = 25;

        Graphics device;
        Directions currenDirection = Directions.Right;

        List<Point> snake = new List<Point>();

        Point food = new Point(0, 0);

        int width, height;


        //Game is constructor
        public Game(Graphics device, int width, int height)
        {
            this.device = device;
            this.width = width;
            this.height = height;

        }
        public bool Running = false;
        public enum Directions
        {
            Left,
            Right,
            Up,
            Down,
        }
        public void Draw()
        {
            device.Clear(Color.White);
            foreach (Point part in snake)
            {
                DrawPart(Brushes.Green, part);
            }
            DrawPart(Brushes.Red, food);

        }

        private void DrawPart(Brush brush, Point p)
        {
            device.FillEllipse(brush, p.X * PartSize, p.Y * PartSize, PartSize, PartSize);
        }
        public void ChangeDirection(Directions directions)
        {
            currenDirection = directions;

        }
        private void ChangeFood()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            food.X = rand.Next(0, width / PartSize - 1);
            food.Y = rand.Next(0, height / PartSize - 1);
            DrawPart(Brushes.Red, food);

        }
        public void UpdateSnake()
        {

            Point nextHead = snake[0];
            if (currenDirection == Directions.Right)
            {
                nextHead.X += 1;
            }
            else if (currenDirection == Directions.Left)
            {
                nextHead.X -= 1;
            }
            else if (currenDirection == Directions.Up)
            {
                nextHead.Y -= 1;
            }
            else if (currenDirection == Directions.Down)
            {
                nextHead.Y += 1;
            }

            if (nextHead.Y == height / PartSize)
            {
                nextHead.Y = 0;
            }
            else if (nextHead.Y == -1)
            {
                nextHead.Y = height / PartSize - 1;
            }

            if (nextHead.X == width / PartSize)
            {
                nextHead.X = 0;
            }
            else if (nextHead.X == -1)
            {
                nextHead.X = width / PartSize - 1;
            }

            foreach (Point part in snake)
            {
                if (nextHead == part)
                {
                    //gameovert 
                    Running = false;
                }

            }

            if (nextHead == food)
            {
                snake.Insert(0, food);
                DrawPart(Brushes.Green, nextHead);
                ChangeFood();
            }
            else
            { 
                snake.Insert(0, nextHead);
                DrawPart(Brushes.Green, nextHead);
                DrawPart(Brushes.White, snake[snake.Count -1]);
                snake.RemoveAt(snake.Count - 1);
            }

        }
        public void StopGameLoop()
        {
            Running = false;
        }
        public void StartGame()
        {
            snake.Clear();
            for (int i = 70; i > 0; i--)
            {
                snake.Add(new Point(i, 0));
            }
            Running = true;
            ChangeFood();
            currenDirection = Directions.Right;
            GameLoop();
        }

        private void GameLoop()
        {
            Draw();
            while (Running)
            {       
                UpdateSnake();
                Thread.Sleep(60);
                Application.DoEvents();
            }
            DrawPart(Brushes.Black, snake[0]);
            string gameOverText = $"Du hast {snake.Count} Punkte";
            Font f = new Font("Arial", 25);
            SizeF stringSize = device.MeasureString(gameOverText, f);
            device.DrawString(gameOverText, f, Brushes.Black, (width - stringSize.Width) / 2, (height - stringSize.Height) / 2);
        }
    }
}
