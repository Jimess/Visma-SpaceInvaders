using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersGame.Classes
{
    class Enemy : GameObject
    {

        public int points;
        public int row, col;
        public Bullet bullet;

        public Enemy(int X, int Y, Brush Color, int Width, int Height, int points, int row, int col) : base(X, Y, Color, Width, Height)
        {
            this.points = points;
            this.row = row;
            this.col = col;
        }

        public override void Update()
        {
        }

        public void Move()
        {
            switch (Stats.dir) {
                case Stats.Direction.RIGHT:
                    X += Constants.ENEMY_INITIAL_MOVE_SPEED;
                    break;
                case Stats.Direction.LEFT:
                    X -= Constants.ENEMY_INITIAL_MOVE_SPEED;
                    break;
            }
        }

        public bool CanMove()
        {
            switch (Stats.dir)
            {
                case Stats.Direction.RIGHT:
                    if (X + Constants.ENEMY_INITIAL_MOVE_SPEED >= Constants.CANVAS_WIDTH - Constants.ENEMY_SIZE)
                    {
                        return false;
                    }
                    return true;
                case Stats.Direction.LEFT:
                    if (X - Constants.ENEMY_INITIAL_MOVE_SPEED <= 0)
                    {
                        return false;
                    }
                    return true;
                default:
                    return false;
            }
        }

        public void MoveDown()
        {
            Y += Constants.ENEMY_Y_SPEED;
        }

        public bool CanMoveDown()
        {
            return !(Y + Constants.ENEMY_Y_SPEED >= Constants.CANVAS_HEIGHT - Constants.ENEMY_SIZE);
        }

        public override void Hit()
        {
            //when hit, add points to the score
            Stats.IncreasePoints(points);
            //increment game speed
            Stats.IncreaseSpeed();
            //die
            dead = true;
        }

        public void Shoot()
        {
            bullet = new Bullet(X + Width / 2 - Constants.BULLET_WIDTH / 2, Y + Height,
                Brushes.Green, Constants.BULLET_WIDTH, Constants.BULLET_HEIGHT, Constants.ENEMY_BULLET_DIR);
        }
    }
}
