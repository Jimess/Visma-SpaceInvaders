using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvadersGame.Classes
{
    class Stats
    {
        public static int Score { get; set; }
        public static bool GameOver { get; set; }
        public static int Lives { get; set; }
        public static bool Reset { get; set; }
        public enum Direction { LEFT, RIGHT };
        public static Direction dir { get; set; }
        public static bool isDown { get; set; }
        public static bool isEnd { get; set; }
        public static int EnemySpeed { get; set; }

        private static int[] lifeBonus = new int[] { 1000, 1500 };
        private static int currentBonus;
        public static bool Won { get; set; }

        public Stats()
        {
            Score = 0;
            GameOver = false;
            Lives = 3;
            Reset = false;
            dir = Direction.RIGHT;
            isDown = false;
            isEnd = false;
            EnemySpeed = Constants.ENEMY_TIMER_SPEED;
            currentBonus = 0;
            Won = false;

        }

        public static void IncreaseSpeed()
        {
            EnemySpeed -= Constants.ENEMY_TIMER_SPEED_STEP;
            Console.WriteLine(EnemySpeed);
        }

        public static void IncreasePoints(int points)
        {
            Score += points;
            
            //add life bonus when goals are reached
            if (currentBonus < lifeBonus.Length)
            {
                if (Score >= lifeBonus[currentBonus])
                {
                    Lives++;
                    currentBonus++;
                }
            }
        }

        public static void PlayerDied()
        {
            Lives -= 1;
            if (Lives >= 0)
            {
                Reset = true;
            } else
            {
                GameOver = true;
            }
        }
    }
}
