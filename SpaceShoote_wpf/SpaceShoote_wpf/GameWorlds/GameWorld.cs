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
        public List<GameObject> gameObjects { get; }

        public Stopwatch GameTimer { get; }

        public GameWorld()
        {
            gameObjects = new List<GameObject>();
            GameTimer = new Stopwatch();
        }

        public void AddObject(GameObject o)
        {
            gameObjects.Add(o);
        }

        public TimeSpan previousGameTick;

        public float deltatime
        {
            get
            {
                return (float)(GameTimer.Elapsed - previousGameTick).TotalMilliseconds;
            }
        }

        public void StartTimer()
        {
            GameTimer.Start();
        }

        public void GameTick()
        {
            foreach (var o in gameObjects)
                o.Tick(deltatime);

            previousGameTick = GameTimer.Elapsed;
        }
    }
}
