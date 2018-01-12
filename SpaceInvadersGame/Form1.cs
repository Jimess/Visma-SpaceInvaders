using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpaceInvadersGame.Classes;

namespace SpaceInvadersGame
{
    public partial class Form1 : Form
    {
        List<GameObject> objects = new List<GameObject>();
        List<GameObject> alive = new List<GameObject>();

        //support list for the coherent enemy movement
        List<Enemy> enemies = new List<Enemy>();

        //bullets list
        List<Bullet> bullets = new List<Bullet>();

        Player player;

        MysteryShip mysteryShip;

        int enemyMoveSpeed = Constants.ENEMY_TIMER_SPEED;

        public Form1()
        {
            InitializeComponent();

            //add borders around picturebox/main canvas
            gameCanvas.BorderStyle = BorderStyle.FixedSingle;

            //reset the stats
            new Stats();

            //start the game timer. the rate at which the game will refresh
            gameTimer.Interval = 1000 / Constants.SPEED;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //enemy timer
            enemy_move_timer.Interval = Constants.ENEMY_TIMER_SPEED;
            enemy_move_timer.Tick += UpdateEnemy;

            //start game
            StartGame();
        }

        private void StartGame()
        {
            //clear lists
            objects.Clear();
            alive.Clear();
            enemies.Clear();
            bullets.Clear();

            //new stats
            new Stats();
            
            //hide gameover labels
            restartLabel.Visible = false;
            gameOverLabel.Visible = false;
            winLabel.Visible = false;

            //add player
            player = new Player(Constants.CANVAS_WIDTH / 2 - Constants.PLAYER_SIZE, Constants.CANVAS_HEIGHT - Constants.PLAYER_SIZE,
                Brushes.Blue, Constants.PLAYER_SIZE, Constants.PLAYER_SIZE);
            objects.Add(player);

            //add enemies
            int spawnStep = (Constants.CANVAS_WIDTH - (2 * Constants.ENEMY_X_OFFSET)) / (Constants.ENEMY_COUNT-1);
            for (int i = 0; i < Constants.ENEMY_LINE_COUNT; i++)
            {
                for (int j = 0; j < Constants.ENEMY_COUNT; j++)
                {
                    objects.Add(new Enemy(Constants.ENEMY_X_OFFSET - Constants.ENEMY_SIZE/2 + (spawnStep * j), 50 + (50 * i), Constants.ENEMEY_COLORS[i],
                        Constants.ENEMY_SIZE, Constants.ENEMY_SIZE, Constants.ENEMY_TOP_SCORE - (i * Constants.ENEMY_SCORE_MULTIPLIER), i , j)); //score starts at 50 then decrements by 10
                }

            }

            //add mystery ship timer
            Random mr = new Random();
            mysteryShipTimer.Interval = mr.Next(Constants.M_MIN_TIMER, Constants.M_MAX_TIMER);
            mysteryShipTimer.Tick += ShipSpawn;
            mysteryShipTimer.Start();

            enemy_move_timer.Start();
        }

        private void ResetGame() //only resets player and position
        {
            player = new Player(Constants.CANVAS_WIDTH / 2 - Constants.PLAYER_SIZE, Constants.CANVAS_HEIGHT - Constants.PLAYER_SIZE,
                Brushes.Blue, Constants.PLAYER_SIZE, Constants.PLAYER_SIZE);
            objects.Add(player);
            Stats.Reset = false;
        }

        private void ShipSpawn(object sender, EventArgs e)
        {
            Random mr = new Random();
            mysteryShip = new MysteryShip(0, 0, Brushes.Violet, Constants.M_SHIP_WIDTH, Constants.M_SHIP_HEIGHT, 150);
            mysteryShip.Direction = Constants.M_SHIP_SPEED;
            objects.Add(mysteryShip);

            //update mystery ship timer
            mysteryShipTimer.Interval = mr.Next(Constants.M_MIN_TIMER, Constants.M_MAX_TIMER);
        }

        private void UpdateEnemy(object sender, EventArgs e)
        {
            if (!Stats.isEnd)
            {
                // get the list of the bottom row enemies
                List<Enemy> bottomEnemies = new List<Enemy>();


                // if it is not the end and enemies are depleted
                if (enemies.Count != 0)
                {
                    bottomEnemies.Add(enemies[0]);
                } else
                {
                    Stats.GameOver = true;
                    Stats.Won = true;
                }

                //need to store the bottom enemies into the list
                foreach (Enemy en in enemies)
                {
                    bool found = true;
                    for (int i = 0; i < bottomEnemies.Count; i++)
                    {
                        if (bottomEnemies[i].col == en.col) //if enemy in same column
                        {
                            if (bottomEnemies[i].row < en.row) //if enemy is lower than the previous in the column, break and dont add
                            {
                                bottomEnemies[i] = en;
                                found = true;
                                break;
                            } else //if enemy is not lower than the previous in the column, break and dont add
                            {
                                found = true;
                                break;
                            }
                        } else // if enemy not in same column, add it after loop
                        {
                            found = false;
                        }
                    }
                    if (!found)
                    {
                        bottomEnemies.Add(en);
                    }
                }


                if (Stats.isDown)
                {
                    foreach (Enemy en in enemies) // moving enemies down and checking if the next move will be end
                    {
                        en.MoveDown();
                        if (!en.CanMoveDown())
                            Stats.isEnd = true;
                    }
                    Stats.isDown = false; // moved down, switching directions
                    if (Stats.dir == Stats.Direction.RIGHT)
                        Stats.dir = Stats.Direction.LEFT;
                    else
                    {
                        Stats.dir = Stats.Direction.RIGHT;
                    }
                }
                else
                {
                    foreach (Enemy en in enemies)
                    {
                        en.Move();
                        if (!en.CanMove()) // checking if the next move will be down or not
                            Stats.isDown = true;
                    }

                    //RANDOM enemy shoots out a bullet, or none shoots
                    Random r = new Random();
                    int rng = r.Next(bottomEnemies.Count+1);
                    if (rng < bottomEnemies.Count)
                        bottomEnemies[rng].Shoot();
                }
            } else // if the bottom last space has been reached by enemies
            {
                //game over
                Stats.GameOver = true;
            }
            
            //update enemymovespeed
            enemy_move_timer.Interval = Stats.EnemySpeed; //timer updates itself when interval is changed. no need to restart

        }


        private void UpdateScreen(object sender, EventArgs e)
        {
            if (Stats.Reset)
            {
                ResetGame();
            } else if (Stats.GameOver)
            {
                //stop enemy movements
                enemy_move_timer.Stop();
                mysteryShipTimer.Stop();

                //show gameover labels
                restartLabel.Visible = true;
                gameOverLabel.Visible = true;

                //if you won the game show win label
                if (Stats.Won)
                    winLabel.Visible = true;

                //input for new game
                if (Input.GetKeyPressed(Keys.Enter))
                {
                    StartGame();
                }

            } else
            {                
                //input
                if (Input.GetKeyPressed(Keys.D))
                {
                    player.Direction = Constants.PLAYER_SPEED;
                } else if (Input.GetKeyPressed(Keys.A))
                {
                    player.Direction = -Constants.PLAYER_SPEED;
                } else
                {
                    player.Direction = 0;
                }

                //seperate if so shooting can be done while other keys are pressed
                if (Input.GetKeyPressed(Keys.Space))
                {
                    player.Shoot();
                }

                //new alive list
                alive = new List<GameObject>();

                //collision handling
                foreach (GameObject obj in objects)
                {
                    if (obj is Bullet)
                    {
                        //checks for enemy collision
                        foreach(Enemy en in enemies)
                        {
                            ((Bullet)obj).CollisionHandling(en);
                        }
                        //bullets check for collision with the player
                        ((Bullet)obj).CollisionHandling(player);
                        if (mysteryShip != null && !mysteryShip.dead)
                            ((Bullet)obj).CollisionHandling(mysteryShip);
                    }
                }

                //clearing the enemies list after collision as it needs updating first. dead enemies will not be added to the enemies list
                enemies = new List<Enemy>();

                foreach (GameObject obj in objects)
                {
                    //if (!obj.dead && obj.GetType() != typeof(Enemy))
                    if (!obj.dead)
                    {
                        if (obj is Enemy)
                        {
                            enemies.Add((Enemy)obj);
                            if (((Enemy)obj).bullet != null)
                            {
                                alive.Add(((Enemy)obj).bullet);
                                ((Enemy)obj).bullet = null;
                            }
                            //Console.WriteLine("Adding enemy");
                        } else if (obj is Player) // if player, check for bullet
                        {
                            alive.Add(obj);
                            if (player.bullet != null) {
                                alive.Add(player.bullet);
                                player.bullet = null;
                            }
                            obj.Update();
                        } else
                        {
                            alive.Add(obj);
                            obj.Update();
                        }
                    }
                }

                objects.Clear();
                objects.AddRange(alive);
                objects.AddRange(enemies);
            }

            //update score
            scoreCount.Text = Stats.Score.ToString();
            //update lives
            livesCount.Text = Stats.Lives.ToString();

            //update GUI
            gameCanvas.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void gameCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (GameObject obj in alive)
            {
                obj.Draw(g);
            }

            foreach (Enemy en in enemies)
            {
                en.Draw(g);
            }
        }
    }
}
