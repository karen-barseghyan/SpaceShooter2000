using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class GameWorld
    {
        // list of objects in game world
        public List<GameObject> gameObjects { get; }

        // game timer
        // used for controlling timed events
        public Stopwatch GameTimer { get; }
        // time of last game tick (frame)
        public TimeSpan previousGameTick;

        public GameWave currentWave;
        public Boss currentBoss;

        public Explosion explosionPrefab;

        public int wavesCleared = 0;
        public int score = 0;
        Random random = new Random();
        //puses the game cycle
        public bool pause;

        int framecounter;
        int previousSecondFrameCount;
        TimeSpan PreviousSecond;

        public float difficultyIncraese = 0.02f;

        public Vector2 windowSize;
        public string language;
        public Player player;
        public Bar playerHealth;
        public Bar waveTimer;
        public Bar bossHealth;

        public TextImage realTimeScore;
        public TextImage rtScoreText;

        public TextImage pauseText;
        public TextImage gameOverText;
        public TextImage pressStart;
        public TextImage scoreText;
        public TextImage scoreNumber;

        public TimeSpan showGameOver;
        public int gameState = 0;

        // game world Constructor
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

        // Function used for adding objects to list of game objects in game world
        public void AddObject(GameObject o)
        {
            gameObjects.Add(o);
        }

        //returns time since last tick (frame) in miliseconds
        public float deltatime
        {
            get
            {
                return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds;
            }
        }
        //initialize game timer
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

        //tick function of game wolrd
        // runs every tick (frame
        // calls tick function of every object in game world
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

                //tu crashuje
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

        public bool AnyObjectsFromGroup(int group)
        {
            foreach (GameObject o in gameObjects)
            {
                if (o.group == group)
                    return true;
            }
            return false;
        }

        public int GetFPS()
        {
            return previousSecondFrameCount;
        }

        //pauses the game
        public void Pause()
        {
            GameTimer.Stop();
            gameState = 2;
            pause = true;
        }
        //unpauses the game
        public void UnPause()
        {
            GameTimer.Start();
            pause = false;
            gameState = 1;
        }

        public int GameTime()
        {
            return (int)GameTimer.Elapsed.TotalMilliseconds;
        }
    }
}
