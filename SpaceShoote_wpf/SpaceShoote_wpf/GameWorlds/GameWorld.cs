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
        }

        //tick function of game wolrd
        // runs every tick (frame
        // calls tick function of every object in game world
        public void GameTick()
        {
            foreach (var o in gameObjects)
                o.Tick(deltatime);

            previousGameTick = GameTimer.Elapsed;
        }
    }
}
