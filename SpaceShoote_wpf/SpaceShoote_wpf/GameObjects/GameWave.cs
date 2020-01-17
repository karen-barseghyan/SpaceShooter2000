using SpaceShoote_wpf.GameWorlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShoote_wpf.GameObjects
{
    public class GameWave
    {
        private MainWindow mainWindow;
        private GameWorld gameWorld;
        public List<SpawnData> enemies;
        public List<SpawnData> otherObjects;
        public TimeSpan timeLimit;
        public int WaveGroup;
        public int WaveClearScore = 100;
        public bool clearedEnemiesSkip = true;

        public TimeSpan nextEnemy;
        public TimeSpan nextObject;

        public GameWave(MainWindow mainwindow, GameWorld gameworld)
        {
            mainWindow = mainwindow;
            gameWorld = gameworld;
            Random random = new Random();
            GenerateWave(random.Next(2,5));
            WaveGroup = gameWorld.wavesCleared;
        }

        public GameWave(MainWindow mainwindow, GameWorld gameworld, int seed)
        {
            mainWindow = mainwindow;
            gameWorld = gameworld;
            
            GenerateWave(seed);
            WaveGroup = seed;
        }

        public void GenerateWave(int seed)
        {
            mainWindow.DebugWrite("next wave" + seed);
            enemies = new List<SpawnData>();
            otherObjects = new List<SpawnData>();
            nextEnemy = TimeSpan.FromMilliseconds(0);
            nextObject = TimeSpan.FromMilliseconds(0);
            switch (seed) {
                case 0:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 1000);
                        mainWindow.DebugWrite(timeLimit.TotalMilliseconds.ToString());
                        break;
                    }
                default:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        mainWindow.DebugWrite(timeLimit.TotalMilliseconds.ToString());
                        Enemy enemy1 = new Enemy(mainWindow, gameWorld);
                        enemy1.group = WaveGroup;
                        enemy1.Velocity = new Vector2(200, 200);
                        enemy1.Scale = new Vector2(2, 2);
                        enemies.Add(new SpawnData(enemy1, 500));
                        break;
                    }
            } 
            
        }

        public bool IsWaveOver()
        {
            if (gameWorld.GameTime() > timeLimit.TotalMilliseconds)
                return true;
            if (!gameWorld.AnyObjectsFromGroup(WaveGroup) && enemies.Count == 0 && clearedEnemiesSkip)
                return true;
            return false;
        }

        public GameObject GetNextEnemy()
        {
            mainWindow.DebugWrite("add enemy");
            var enemy = enemies[0].gameObject;
            nextEnemy = TimeSpan.FromMilliseconds(enemies[0].delay);
            enemies.Remove(enemies[0]);
            return enemy;
        }
    }

    public struct SpawnData
    {
        public GameObject gameObject;
        public float delay;
        public SpawnData(GameObject o, float d)
        {
            gameObject = o;
            delay = d;
        }
    }
}
