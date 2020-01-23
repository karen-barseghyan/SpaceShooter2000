using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    /// <summary>
    /// GameWorld class.
    /// Responsible for holding reference to everything in the game world, calling all object's functions
    /// </summary>
    public class GameWorld
    {
        /// list of objects in game world
        public List<GameObject> gameObjects { get; }

        /// game timer
        /// used for controlling timed events
        public Stopwatch GameTimer { get; }
        /// time of last game tick (frame)
        public TimeSpan previousGameTick;
        /// <summary>
        /// Current Wave Reference
        /// </summary>
        public GameWave currentWave;
        /// <summary>
        /// Current Boss reference, to display his remaining health on top of screen
        /// </summary>
        public Boss currentBoss;
        /// <summary>
        /// Explosion Prefab that all objects use when they die
        /// </summary>
        public Explosion explosionPrefab;
        /// <summary>
        /// keeps track of cleared waves. used for wave variations
        /// </summary>
        public int wavesCleared = 0;
        /// <summary>
        /// Keeps track of player score. Game's aim is for player to strive for highest score!
        /// </summary>
        public int score = 0;
        Random random = new Random();
        ///puses the game cycle
        public bool pause;

        int framecounter;
        int previousSecondFrameCount;
        TimeSpan PreviousSecond;
        /// <summary>
        /// how much does the wave time decreases after previous one. % based (1-0.2) more than 1 extends the wave time
        /// </summary>
        public float difficultyIncraese = 0.02f;
        /// <summary>
        /// Saves window size, used by other objects
        /// </summary>
        public Vector2 windowSize;
        /// <summary>
        /// Selects language for various Texts
        /// </summary>
        public string language;
        /// <summary>
        /// Player reference
        /// </summary>
        public Player player;
        /// <summary>
        /// player Healthbar reference
        /// </summary>
        public Bar playerHealth;
        /// <summary>
        /// Wave time reference
        /// </summary>
        public Bar waveTimer;
        /// <summary>
        /// Boss' health bar reference
        /// </summary>
        public Bar bossHealth;
        /// <summary>
        /// Text image responsible for displaying in-game score
        /// </summary>
        public TextImage realTimeScore;
        /// <summary>
        /// text image that says "Points" to show where on screen to look for current score
        /// </summary>
        public TextImage rtScoreText;
        /// <summary>
        /// Text image for pause state
        /// </summary>
        public TextImage pauseText;
        /// <summary>
        /// Text image for game over state
        /// </summary>
        public TextImage gameOverText;
        /// <summary>
        /// Text image telling player to press any key to start
        /// </summary>
        public TextImage pressStart;
        /// <summary>
        /// Text image displaying "Points" text next to score number on Game Over screen
        /// </summary>
        public TextImage scoreText;
        /// <summary>
        /// Text image displaying actual score on game over screen
        /// </summary>
        public TextImage scoreNumber;
        /// <summary>
        /// Keeps track of the delay after player dies and before game over screen comes up
        /// </summary>
        public TimeSpan showGameOver;
        /// <summary>
        /// Keeps track of current game state
        /// 0 - before game launches, "press any key to start"
        /// 1 - active game
        /// 2 - paused game
        /// 3 - game over screen
        /// 4 - transition to game over screen 
        /// </summary>
        public int gameState = 0;

        /// <summary>
        /// GameWorld Constructor. Initializes and prepares various objects necessery for proper gameplay
        /// </summary>
        /// <param name="windowsize"></param>
        /// <param name="lang"></param>
        public GameWorld(Vector2 windowsize, string lang)
        {
            float width = windowsize.X;
            float height = windowsize.Y;
            windowSize = windowsize;
            language = lang;
            gameObjects = new List<GameObject>();
            GameTimer = new Stopwatch();
            explosionPrefab = new Explosion(this);

            pressStart = new TextImage(this, "Press");
            pressStart.Position = new Vector2(width / 2, height / 2 + 64);
            pressStart.name = "Press";
            pressStart.spriteSizeX = 275;
            pressStart.spriteSizeY = 47;
            pressStart.Scale = new Vector2(2, 2);

            pauseText = new TextImage(this, "Pause");
            pauseText.Position = new Vector2(width / 2, height / 2 + 64);
            pauseText.name = "Pause";
            pauseText.spriteSizeX = 65;
            pauseText.Scale = new Vector2(3, 3);
            pauseText.spriteSizeY = 16;

            realTimeScore = new TextImage(this, score, false);
            realTimeScore.name = "font_spreadsheet_x11x16.png";
            realTimeScore.Scale = new Vector2(1, 1);
            realTimeScore.spriteSizeX = 11;
            realTimeScore.spriteSizeY = 16;
            realTimeScore.Position = new Vector2(width - 11, height - 16);


            rtScoreText = new TextImage(this, "Points");
            rtScoreText.name = "Points";
            rtScoreText.Scale = new Vector2(1, 1);
            rtScoreText.spriteSizeX = 71;
            rtScoreText.spriteSizeY = 16;
            rtScoreText.Position = new Vector2(width - 8 * 11 * realTimeScore.Scale.X - rtScoreText.spriteSizeX, height - 16);
        }

        /// <summary>
        /// Function used for adding objects to list of game objects in game world
        /// </summary>
        /// <param name="o">Object to add to gameworld objects lists</param>
        public void AddObject(GameObject o)
        {
            gameObjects.Add(o);
        }

        /// <summary>
        /// Returns time between previous and current frame in miliseconds
        /// </summary>
        public float deltatime
        {
            get
            {
                return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds;
            }
        }
        /// <summary>
        /// Start timer
        /// </summary>
        /// <param name="real"></param>
        public void StartTimer(bool real)
        {
            
            GameTimer.Start();
            pause = false;
            framecounter = 0;
            previousSecondFrameCount = 0;
            PreviousSecond = GameTimer.Elapsed;

            if (real)
            {
                currentWave = new GameWave(this, 0);
            }
        }

        /// <summary>
        /// tick function of game wolrd
        /// runs every frame, unless game is paused (function still is called, but does nothing)
        /// calls tick function of every object in game world
        /// </summary>
        public void GameTick()
        {   
            if (!pause)
            {
                if (gameState==1)
                    realTimeScore.UpdateNumber(score);
                // start if frane
                if ((GameTimer.Elapsed - PreviousSecond).TotalMilliseconds > 1000)
                {
                    previousSecondFrameCount = framecounter;
                    framecounter = 0;
                    PreviousSecond = GameTimer.Elapsed;
                }
                
                 
                // do stuff
                if (currentWave != null)
                {
                    if (waveTimer != null)
                        waveTimer.percent = ((float)(currentWave.timeLimit.TotalMilliseconds - GameTime()) / currentWave.duration / currentWave.difficultyFactor) * 1000;

                    if (currentWave.IsWaveOver())
                    {
                        wavesCleared++;
                        if (currentWave.difficultyFactor > 0.2f)
                            currentWave.difficultyFactor -= difficultyIncraese;

                        
                        if (wavesCleared % 10 == 0 && wavesCleared > 8)
                            currentWave.GenerateWave(10);
                        else
                            currentWave.GenerateWave(random.Next(1, 10));

                    }
                    else
                    {
                        if (GameTime() > currentWave.nextEnemy.TotalMilliseconds && currentWave.enemies.Count > 0)
                        {
                            gameObjects.Add(currentWave.GetNextEnemy());
                        }
                    }
                } else
                {
                    if (waveTimer != null)
                        waveTimer.percent = 0;
                }

                if (bossHealth != null)
                {
                    if (currentBoss != null)
                    {
                        bossHealth.percent = currentBoss.life / 5;
                    }
                    else
                    {
                        bossHealth.percent = 0;
                    }
                }


                // things happen and things move
                //foreach (var o in gameObjects.ToList())
                //    o.Tick();
                //var allTasks = new List<Task>();

                int max = 100;
                if (gameObjects.Count < max)
                    max = gameObjects.Count;
                for (int i = 0; i < max; i++)
                {
                    gameObjects[i].Tick();
                }

                //I tried asynchronous programming of object's movement, but could not get it to work,
                // so game runs synchroniously
                //List<Task> allTasks = new List<Task>();

                foreach (var o in gameObjects.ToList())
                {
                    o.Move();
                    //var t = new Task(o.Move);
                    //allTasks.Add(t);
                    //t.Start();
                    //await Task.Run(new Action(o.Move));
                }
                
                //await Task.WhenAll(allTasks);
                
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].CleanUp();
                }
                // end of frame
                previousGameTick = GameTimer.Elapsed;
                framecounter++;
            }
        }
        /// <summary>
        /// Checks if any objects are present in gameworld from a given Game Wave group
        /// </summary>
        /// <param name="group">group id</param>
        /// <returns>returns true if there is an object present that belongs to given group</returns>
        public bool AnyObjectsFromGroup(int group)
        {
            foreach (GameObject o in gameObjects)
            {
                if (o.group == group)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Gets Number of frames since last second
        /// </summary>
        /// <returns>Number of frames since last second</returns>
        public int GetFPS()
        {
            return previousSecondFrameCount;
        }

        /// <summary>
        /// pauses the game
        /// </summary>
        public void Pause()
        {
            GameTimer.Stop();
            gameState = 2;
            pause = true;
        }
        /// <summary>
        /// resumes the game
        /// </summary>
        public void UnPause()
        {
            GameTimer.Start();
            pause = false;
            gameState = 1;
        }
        /// <summary>
        /// GameTime since start in milliseconds
        /// </summary>
        /// <returns>GameTime since start in milliseconds</returns>
        public int GameTime()
        {
            return (int)GameTimer.Elapsed.TotalMilliseconds;
        }
    }
}
