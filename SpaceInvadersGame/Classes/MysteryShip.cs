using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersGame.Classes
{
    class MysteryShip : GameObject
    {
        public int points;
        public int Direction;

        public MysteryShip(int X, int Y, Brush Color, int Width, int Height, int points) : base(X, Y, Color, Width, Height)
        {
            this.points = points;
        }

        public override void Hit()
        {
            //when hit, add points to the score
            Stats.IncreasePoints(points);
            //die
            dead = true;
        }

        public override void Update()
        {
            X += Direction;

            //if ship leaves bounds just die
            if (X >= Constants.CANVAS_WIDTH)
            {
                //die
                dead = true;
            } else if (X < 0)
            {
                dead = true;
            }
        }
    }
}
