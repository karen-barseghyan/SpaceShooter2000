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
        public bool clearedEnemiesSkip = true; //ends wave if all enemies are defeated

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
            //add more cases (enemy spawn patterns) (leave 0 as empty wave)
            // next wave will have seed (Random 2-5) each time
            switch (seed) {
                case 0:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        mainWindow.DebugWrite(timeLimit.TotalMilliseconds.ToString());
                        //prepare enemies
                        mainWindow.DebugWrite(timeLimit.TotalMilliseconds.ToString());

                        //add enemies to the list (enemy, miliseconds until spawning NEXT enemy (if not last enemy in the list)
                        //  enemies.Add(new SpawnData(boss, 0));

                        // works same with "otherObjects". Use that for Background / Asteroid etc. objects they use a seperate list
                        // all enemies, but not all objects have to be removed to clear wave
                        break;
                    }
                case 1:
                    {
                        //30 discs 
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        var rand = new Random();
                        for (int i = 0; i < 30; i++)
                        {
                            Enemy5 newEnemy = new Enemy5(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);

                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };


                        break;
                    }

                case 2:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        // 10 beetles, 10 wasps
                        var rand = new Random();
                        for (int i = 0; i < 5; i++)
                        {
                            Enemy1 newEnemy = new Enemy1(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 400));
                        };

                        for (int i = 0; i < 5; i++)
                        {
                            Enemy2 newEnemy = new Enemy2(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 400));
                        };

                        for (int i = 0; i < 5; i++)
                        {
                            Enemy1 newEnemy = new Enemy1(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 400));
                        };

                        for (int i = 0; i < 5; i++)
                        {
                            Enemy2 newEnemy = new Enemy2(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 400));
                        };


                        break;
                    }

                case 4:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        //little beetles+drones
                        var rand = new Random();
                        for (int i = 0; i < 10; i++)
                        {
                            Enemy3 newEnemy = new Enemy3(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);

                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));

                            Enemy7 newEnemy1 = new Enemy7(mainWindow, gameWorld);
                            newEnemy1.group = WaveGroup;
                            newEnemy1.Velocity = new Vector2(0, 100);
                            newEnemy1.Scale = new Vector2(2, 2);

                            newEnemy1.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy1, 300));
                        };
                        break;
                    }

                case 5:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        //line of mega beetles
                        for (int i = 0; i < 40; i++)
                        {
                            Enemy1 newEnemy = new Enemy1(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            var rand = new Random();
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };
                        break;
                    }

                case 6:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        //bouncing skitters
                        var rand = new Random();
                        for (int i = 0; i < 30; i++)
                        {
                            Enemy4 newEnemy = new Enemy4(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);

                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 600));
                        };
                        break;
                    }


                case 7:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        //two of each
                        var rand = new Random();

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy1 newEnemy = new Enemy1(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy2 newEnemy = new Enemy2(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy3 newEnemy = new Enemy3(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy4 newEnemy = new Enemy4(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy5 newEnemy = new Enemy5(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy6 newEnemy = new Enemy6(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy7 newEnemy = new Enemy7(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy8 newEnemy = new Enemy8(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };


                        for (int i = 0; i < 2; i++)
                        {
                            Enemy9 newEnemy = new Enemy9(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };
                        break;
                    }

                case 8:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        var rand = new Random();
                        for (int i = 0; i < 10; i++)
                        {
                            Enemy9 newEnemy = new Enemy9(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 10; i++)
                        {
                            Enemy2 newEnemy = new Enemy2(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 300));
                        };
                        break;
                    }


                case 9:
                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        //lots of drones
                        var rand = new Random();
                        for (int i = 0; i < 50; i++)
                        {
                            Enemy7 newEnemy = new Enemy7(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);

                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 100));
                        };
                        break;
                    }
                case 10:

                    {
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 30000);
                        //boss + some little fishes
                        var rand = new Random();
                        for (int i = 0; i < 1; i++)
                        {
                            Boss newEnemy = new Boss(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(4, 4);

                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 20; i++)
                        {
                            Enemy8 newEnemy = new Enemy8(mainWindow, gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);

                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 1000));
                        };
                        break;
                    }
                default:
                    {
                        // add limit to how long will the wave last until it moves on to the next one
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 3000);
                        //if boss wave, add a long time limit and clearedEnemiesSkip = true;
                        
         
                        break;
                    }
            } 
            
        }
        //
        public bool IsWaveOver()
        {
            if (gameWorld.GameTime() > timeLimit.TotalMilliseconds)
                return true;
            if (!gameWorld.AnyObjectsFromGroup(WaveGroup) && enemies.Count == 0 && clearedEnemiesSkip)
                return true;// give clear bonus only here
            return false;
        }

        public GameObject GetNextEnemy()
        {
            //mainWindow.DebugWrite("add enemy");
            var enemy = enemies[0].gameObject;
            nextEnemy = TimeSpan.FromMilliseconds(enemies[0].delay + gameWorld.GameTime());
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
