using SpaceShoote_wpf.GameObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShoote_wpf.GameWorlds
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

        //puses the game cycle
        public bool pause;

        int framecounter;
        int previousSecondFrameCount;
        TimeSpan PreviousSecond;

        // game world Constructor
        public GameWorld()
        {
            gameObjects = new List<GameObject>();
            GameTimer = new Stopwatch();
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
        public void StartTimer()
        {
            GameTimer.Start();
            pause = false;
            framecounter = 0;
            previousSecondFrameCount = 0;
            PreviousSecond = GameTimer.Elapsed;
        }

        //tick function of game wolrd
        // runs every tick (frame
        // calls tick function of every object in game world
        public void GameTick()
        {   
            if (!pause)
            {
                if ((GameTimer.Elapsed - PreviousSecond).TotalMilliseconds > 1000)
                {
                    previousSecondFrameCount = framecounter;
                    framecounter = 0;
                    PreviousSecond = GameTimer.Elapsed;
                }
                foreach (var o in gameObjects)
                    o.Tick(deltatime);

                previousGameTick = GameTimer.Elapsed;
                framecounter++;
            }
        }

        public int GetFPS()
        {
            return previousSecondFrameCount;
        }

        //pauses the game
        public void Pause()
        {
            GameTimer.Stop();
            pause = true;
        }
        //unpauses the game
        public void UnPause()
        {
            GameTimer.Start();
            pause = false;
        }

        public int GameTime()
        {
            return (int)GameTimer.Elapsed.TotalMilliseconds;
        }
    }
}
