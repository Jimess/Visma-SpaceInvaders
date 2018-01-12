using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SpaceInvadersGame.Classes
{
    public abstract class GameObject
    {
        //positions vars
        public int X { get; set; }
        public int Y { get; set; }
        Brush Color; // set up different colour for different GameObjects
        public int Width { get; set; }
        public int Height { get; set; }
        public bool dead = false;


        public GameObject(int X, int Y, Brush Color, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Color = Color;
            this.Width = Width;
            this.Height = Height;
        }

        //to be implemented do do different effects when hit
        public abstract void Hit();

        //draw
        public virtual void Draw(Graphics g)
        {
            g.FillRectangle(Color, new Rectangle(X, Y, Width, Height));
        }

        // update methods is responsible for moving objects. in this case it will be abstract even though the bricks will not be moving;
        public abstract void Update();
    }
}
