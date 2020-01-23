using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    public class GameWave
    {
        private GameWorld gameWorld;
        public List<SpawnData> enemies;
        public List<SpawnData> otherObjects;
        public TimeSpan timeLimit;
        public float duration = 5000;
        public float difficultyFactor = 1;
        public int WaveGroup;
        public int WaveClearScore = 100;
        public bool clearedEnemiesSkip = true; //ends wave if all enemies are defeated

        public TimeSpan nextEnemy;
        public TimeSpan nextObject;

        public GameWave(GameWorld gameworld)
        {
            gameWorld = gameworld;
            Random random = new Random();
            GenerateWave(random.Next(2,5));
            WaveGroup = gameWorld.wavesCleared;
        }

        public GameWave(GameWorld gameworld, int seed)
        {
            gameWorld = gameworld;
            
            GenerateWave(seed);
            WaveGroup = seed;
        }

        public void GenerateWave(int seed)
        {
            enemies = new List<SpawnData>();
            otherObjects = new List<SpawnData>();
            nextEnemy = TimeSpan.FromMilliseconds(0);
            nextObject = TimeSpan.FromMilliseconds(0);
            WaveGroup = gameWorld.GameTime();
            //add more cases (enemy spawn patterns) (leave 0 as empty wave)
            // next wave will have seed (Random 2-5) each time
            switch (seed) {
                case 0:
                    {
                        duration = 3000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);

                        //add enemies to the list (enemy, miliseconds until spawning NEXT enemy (if not last enemy in the list)
                        //  enemies.Add(new SpawnData(boss, 0));

                        // works same with "otherObjects". Use that for Background / Asteroid etc. objects they use a seperate list
                        // all enemies, but not all objects have to be removed to clear wave
                        break;
                    }
                case 1:
                    {
                        //30 discs 
                        duration = 10000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        var rand = new Random();
                        for (int i = 0; i < 30; i++)
                        {
                            Enemy5 newEnemy = new Enemy5(gameWorld);
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
                        duration = 5000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        // 10 beetles, 10 wasps
                        var rand = new Random();
                        for (int i = 0; i < 5; i++)
                        {
                            Enemy1 newEnemy = new Enemy1(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 400));
                        };

                        for (int i = 0; i < 5; i++)
                        {
                            Enemy2 newEnemy = new Enemy2(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 400));
                        };

                        for (int i = 0; i < 5; i++)
                        {
                            Enemy1 newEnemy = new Enemy1(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 400));
                        };

                        for (int i = 0; i < 5; i++)
                        {
                            Enemy2 newEnemy = new Enemy2(gameWorld);
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
                        duration = 7000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        //little beetles+drones
                        var rand = new Random();
                        for (int i = 0; i < 10; i++)
                        {
                            Enemy3 newEnemy = new Enemy3(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);

                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));

                            Enemy7 newEnemy1 = new Enemy7(gameWorld);
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
                        duration = 10000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        //line of mega beetles
                        for (int i = 0; i < 40; i++)
                        {
                            Enemy1 newEnemy = new Enemy1(gameWorld);
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
                        duration = 10000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        //bouncing skitters
                        var rand = new Random();
                        for (int i = 0; i < 30; i++)
                        {
                            Enemy4 newEnemy = new Enemy4(gameWorld);
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
                        duration = 10000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        //two of each
                        var rand = new Random();

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy1 newEnemy = new Enemy1(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy2 newEnemy = new Enemy2(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy3 newEnemy = new Enemy3(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy4 newEnemy = new Enemy4(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy5 newEnemy = new Enemy5(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy6 newEnemy = new Enemy6(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy7 newEnemy = new Enemy7(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 2; i++)
                        {
                            Enemy8 newEnemy = new Enemy8(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };


                        for (int i = 0; i < 2; i++)
                        {
                            Enemy9 newEnemy = new Enemy9(gameWorld);
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
                        duration = 20000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        var rand = new Random();
                        for (int i = 0; i < 3; i++)
                        {
                            Enemy9 newEnemy = new Enemy9(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);
                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                        };

                        for (int i = 0; i < 10; i++)
                        {
                            Enemy2 newEnemy = new Enemy2(gameWorld);
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
                        duration = 5000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        //lots of drones
                        var rand = new Random();
                        for (int i = 0; i < 50; i++)
                        {
                            Enemy7 newEnemy = new Enemy7(gameWorld);
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
                        duration = 30000;
                        WaveClearScore = 5000;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        clearedEnemiesSkip = true;
                        //boss + some little fishes
                        var rand = new Random();
                        for (int i = 0; i < 1; i++)
                        {
                            Boss newEnemy = new Boss(gameWorld);
                            newEnemy.group = WaveGroup;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(4, 4);

                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 200));
                            gameWorld.currentBoss = newEnemy;
                        };

                        for (int i = 0; i < 20; i++)
                        {
                            Enemy8 newEnemy = new Enemy8(gameWorld);
                            //no wave group means killing the boss is enough to skip the wave, but they still have to at least spawn
                            newEnemy.group = 0;
                            newEnemy.Velocity = new Vector2(0, 100);
                            newEnemy.Scale = new Vector2(2, 2);

                            newEnemy.Position.X = rand.Next(500);
                            enemies.Add(new SpawnData(newEnemy, 1000));
                        };
                        break;
                    }
                default:
                    {
                        duration = 5000;
                        // add limit to how long will the wave last until it moves on to the next one
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
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
            {
                // give clear bonus only here
                int bonus = (int)(WaveClearScore + (int)(timeLimit.TotalMilliseconds - gameWorld.GameTime())/1000/difficultyFactor/difficultyFactor);
                gameWorld.score += bonus;
                return true;
            }  
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
