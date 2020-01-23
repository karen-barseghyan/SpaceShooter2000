using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameObjects
{
    /// <summary>
    /// Generating a wave of enemies.
    /// </summary>
    public class GameWave
    {
        private GameWorld gameWorld;
        /// <summary>
        /// List of enemies.
        /// </summary>
        public List<SpawnData> enemies;
        /// <summary>
        /// List of objects other than enemies
        /// </summary>
        public List<SpawnData> otherObjects;
        /// <summary>
        /// Time limit of the wave.
        /// </summary>
        public TimeSpan timeLimit;
        /// <summary>
        /// Duration of the wave.
        /// </summary>
        public float duration = 5000;
        /// <summary>
        /// Difficulty of the wave.
        /// </summary>
        public float difficultyFactor = 1;
        /// <summary>
        /// Group of the wave.
        /// </summary>
        public int WaveGroup;
        /// <summary>
        /// Bonus for clearing the wave.
        /// </summary>
        public int WaveClearScore = 100;
        /// <summary>
        /// Ends wave if all enemies defeated.
        /// </summary>
        public bool clearedEnemiesSkip = true; //ends wave if all enemies are defeated
        /// <summary>
        /// Time until next enemy.
        /// </summary>
        public TimeSpan nextEnemy;
        /// <summary>
        /// Time until next object
        /// </summary>
        public TimeSpan nextObject;
        /// <summary>
        /// Making the waves of enemies.
        /// </summary>
        /// <param name="world">World that the object is added to. </param>
        public GameWave(GameWorld gameworld)
        {
            gameWorld = gameworld;
            Random random = new Random();
            GenerateWave(random.Next(2,5));
            WaveGroup = gameWorld.wavesCleared;
        }
        /// <summary>
        /// Making the waves of enemies according to seed.
        /// </summary>
        /// <param name="world">World that the object is added to. </param>
        /// <param name="seed">Seed used to generate enemies </param>
        public GameWave(GameWorld gameworld, int seed)
        {
            gameWorld = gameworld;
            
            GenerateWave(seed);
            WaveGroup = seed;
        }
        /// <summary>
        /// Generating the waves of enemies according to seed.
        /// </summary>
        /// <param name="seed">Seed used to generate enemies </param>
        public void GenerateWave(int seed)
        {
            enemies = new List<SpawnData>();
            otherObjects = new List<SpawnData>();
            nextEnemy = TimeSpan.FromMilliseconds(0);
            nextObject = TimeSpan.FromMilliseconds(0);
            WaveGroup = gameWorld.GameTime();
            /// <summary>
            /// Next wave will have seed (Random 2-5) each time
            /// </summary>
            switch (seed) {
                case 0:
                    {
                        duration = 3000;
                        WaveClearScore = 200;
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);
                        /// <summary>
                        /// All enemies, but not all objects have to be removed to clear wave
                        /// </summary>
                        break;
                    }
                case 1:
                    {

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
                            /// <summary>
                            /// No wave group means killing the boss is enough to skip the wave, but they still have to at least spawn
                            /// </summary>
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
                        timeLimit = TimeSpan.FromMilliseconds(gameWorld.GameTime() + duration * difficultyFactor);       
                        break;
                    }
            } 
            
        }
        /// <summary>
        /// Checking if the wave is over.
        /// </summary>
        public bool IsWaveOver()
        {
            if (gameWorld.GameTime() > timeLimit.TotalMilliseconds)
                return true;
            if (!gameWorld.AnyObjectsFromGroup(WaveGroup) && enemies.Count == 0 && clearedEnemiesSkip)
            {
                /// <summary>
                /// Gives clear bonus only here.
                /// </summary>
                int bonus = (int)(WaveClearScore + (int)(timeLimit.TotalMilliseconds - gameWorld.GameTime())/1000/difficultyFactor/difficultyFactor);
                gameWorld.score += bonus;
                return true;
            }  
            return false;
        }
        /// <summary>
        /// Add next enemy.
        /// </summary>
        public GameObject GetNextEnemy()
        {
            var enemy = enemies[0].gameObject;
            nextEnemy = TimeSpan.FromMilliseconds(enemies[0].delay + gameWorld.GameTime());
            enemies.Remove(enemies[0]);
            return enemy;
        }
    }

    /// <summary>
    /// Structure responsible for how the enemy spawns.
    /// </summary>
    public struct SpawnData
    {
        /// <summary>
        /// What GameObject is the enemy.
        /// </summary>
        public GameObject gameObject;
        /// <summary>
        /// When they spawn.
        /// </summary>
        public float delay;
        /// <summary>
        /// Constructing the spawn.
        /// </summary>
        /// <param name="o">What enemy is.</param>
        /// <param name="d">When they spawn. </param>

        public SpawnData(GameObject o, float d)
        {
            gameObject = o;
            delay = d;
        }
    }
}
