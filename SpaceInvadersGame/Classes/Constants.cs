using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SpaceInvadersGame.Classes
{
    class Constants
    {
        //main canvas size
        public static readonly int CANVAS_WIDTH = 900;
        public static readonly int CANVAS_HEIGHT = 500;

        //fps
        public static readonly int SPEED = 60;

        //player settings
        public static readonly int PLAYER_SIZE = 30;
        public static readonly int PLAYER_SPEED = 10;
        public static readonly int PLAYER_LIVES = 3;
        public static readonly int PLAYER_CD = 500;
        public static readonly int PLAYER_BULLET_DIR = -5; //negative value, means the bullet will go up

        //bullet settings
        public static readonly int BULLET_WIDTH = 5;
        public static readonly int BULLET_HEIGHT = 16;


        //enemy settings
        public static readonly int ENEMY_LINE_COUNT = 5;
        public static readonly int ENEMY_COUNT = 15; //per line
        public static readonly int ENEMY_SIZE = 30;
        public static readonly int ENEMY_SCORE_MULTIPLIER = 10; //point decrement step from top to bottom
        public static readonly int ENEMY_TOP_SCORE = ENEMY_LINE_COUNT * ENEMY_SCORE_MULTIPLIER;
        public static readonly int ENEMY_TIMER_SPEED = 1000;
        public static readonly int ENEMY_MAX_TIMER_SPEED = 30;
        //enemy move speed will no exceed the max speed
        public static readonly int ENEMY_TIMER_SPEED_STEP = (ENEMY_TIMER_SPEED - ENEMY_MAX_TIMER_SPEED) / (ENEMY_LINE_COUNT * ENEMY_COUNT);
        public static readonly int ENEMY_INITIAL_MOVE_SPEED = 15;
        public static readonly int ENEMY_Y_SPEED = 30;
        public static readonly int ENEMY_X_OFFSET = 100;
        public static readonly int ENEMY_Y_OFFSET = 150;
        // must be equal to the number of enemy lines
        public static readonly List<Brush> ENEMEY_COLORS = new List<Brush> { Brushes.Red, Brushes.Yellow, Brushes.GreenYellow,
            Brushes.RosyBrown, Brushes.Goldenrod };
        public static readonly int ENEMY_BULLET_DIR = 5;

        //player blockade settings
        public static readonly int BLOCKADE_COUNT = 4;
        public static readonly int BLOCKAGE_SIZE = 150;
        public static readonly int BLOCKADE_LIFE = 20;

        //mystery ship settings
        public static readonly int M_SHIP_WIDTH = 50;
        public static readonly int M_SHIP_HEIGHT = 10;
        public static readonly int M_MIN_TIMER = 5000;
        public static readonly int M_MAX_TIMER = 10000;
        public static readonly int M_SHIP_SPEED = 5;



    }
}
