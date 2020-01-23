﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameWorld
    {
        public MainWindow mainWindow;
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

        // game world Constructor
        public GameWorld(MainWindow window)
        {
            mainWindow = window;
            gameObjects = new List<GameObject>();
            GameTimer = new Stopwatch();
            explosionPrefab = new Explosion(mainWindow, this);
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
                currentWave = new GameWave(mainWindow, this, 0);
            }
        }

        //tick function of game wolrd
        // runs every tick (frame
        // calls tick function of every object in game world
        public void GameTick()
        {   
            if (!pause)
            {
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
                    if (mainWindow.waveTimer != null)
                        mainWindow.waveTimer.percent = ((float)(currentWave.timeLimit.TotalMilliseconds - GameTime()) / currentWave.duration / currentWave.difficultyFactor) * 1000;

                    if (currentWave.IsWaveOver())
                    {
                        wavesCleared++;
                        if (currentWave.difficultyFactor > 0.2f)
                            currentWave.difficultyFactor -= 0.01f;

                        
                        if (wavesCleared % 10 == 0 && wavesCleared > 8)
                            currentWave.GenerateWave(10);
                        else
                            currentWave.GenerateWave(random.Next(1, 10));
                        
                        //currentWave.GenerateWave(8);
                        mainWindow.DebugWrite(currentWave.difficultyFactor.ToString());
                        //

                        //set thing
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
                    if (mainWindow.waveTimer != null)
                        mainWindow.waveTimer.percent = 0;
                }

                if (mainWindow.bossHealth != null)
                {
                    if (currentBoss != null)
                    {
                        mainWindow.bossHealth.percent = currentBoss.life / 5;
                    }
                    else
                    {
                        mainWindow.bossHealth.percent = 0;
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
            mainWindow.gameState = 2;
            pause = true;
        }
        //unpauses the game
        public void UnPause()
        {
            GameTimer.Start();
            pause = false;
            mainWindow.gameState = 1;
        }

        public int GameTime()
        {
            return (int)GameTimer.Elapsed.TotalMilliseconds;
        }
    }
}