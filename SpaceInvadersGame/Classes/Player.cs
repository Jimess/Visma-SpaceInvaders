using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvadersGame.Classes
{
    class Player : GameObject
    {
        public int Direction;
        public Bullet bullet;
        Timer CDTimer;
        bool shot = false;
        public Player(int X, int Y, Brush Color, int Width, int Height) : base(X, Y, Color, Width, Height)
        {
            CDTimer = new Timer();
        }

        public override void Update()
        {
            X += Direction;
            X = Clamp(X, 0, Constants.CANVAS_WIDTH - Constants.PLAYER_SIZE);
        }

        //clamping
        public int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }

        public void Shoot()
        {
            if (!shot)
            {
                //shoots the bullet
                //some signs are inverted because the direction the bullet will be shot is negative (will go up)
                bullet = new Bullet(X + Constants.PLAYER_SIZE/2 - Constants.BULLET_WIDTH/2, Y - Constants.PLAYER_SIZE + Constants.BULLET_HEIGHT - 10,
                    Brushes.Green, Constants.BULLET_WIDTH, Constants.BULLET_HEIGHT, Constants.PLAYER_BULLET_DIR);
                shot = true;

                CDTimer.Interval = Constants.PLAYER_CD;
                CDTimer.Tick += BulletCD;
                CDTimer.Start();
            }
            
        }

        // when CD is done, let the player shoot again
        private void BulletCD (object sender, EventArgs e) 
        {
            CDTimer.Stop();
            shot = false;
        }

        public override void Hit()
        {
            dead = true;
            Stats.PlayerDied();
        }
    }
}
