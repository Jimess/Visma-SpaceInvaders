using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersGame.Classes
{
    class Bullet : GameObject
    {
        public int Direction;
        public Bullet(int X, int Y, Brush Color, int Width, int Height, int Direction) : base(X, Y, Color, Width, Height)
        {
            this.Direction = Direction;
        }

        public override void Update()
        {
            Y += Direction;

            if (Y >= Constants.CANVAS_HEIGHT - Height || Y < 0) // if bullet touches the bounds
            {
                dead = true;
            }
        }

        // checks if tho objects have collided
        public bool Overlap(GameObject other)
        {
            //a way to detect collision. This method is not perfect as it will only count the collision
            //when one object is already inside another. This can raise some collision problems as INTS are used and not doubles
            Rectangle gameObject = new Rectangle(X, Y, Width, Height);
            Rectangle otherObject = new Rectangle(other.X, other.Y, other.Width, other.Height);

            if (gameObject.IntersectsWith(otherObject))
            {
                return true;
            }

            return false;
        }


        public void CollisionHandling(GameObject other)
        {
            if (other.GetType() != GetType() && Overlap(other)) // bullet can't hit another bullet and can't hit the player
            {
                Console.WriteLine("Collided! " + other.GetType());
                other.Hit();
                Hit();
            }
        }

        public override void Hit()
        {
            dead = true;
        }
    }
}
