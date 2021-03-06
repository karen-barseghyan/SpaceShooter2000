﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameObjects
{
    /// <summary>
    /// First enemy, so called Mega Beetle, it goes down while bouncing of the walls at steady speed, does not shoot, only deals contact damage.
    /// </summary>
    public class Enemy1 : Ship
    {
        public Enemy1(GameWorld world)
        {
            gameWorld = world;
            points = 30;
            life = 100;
            var rand = new Random();
            Position = new System.Numerics.Vector2(rand.Next(500), -100);
            spriteSizeX = 32;
            spriteSizeY = 32;           
            spriteCycle = 0;
            spriteCount = 3;
            transitionDuration = 100;
            slowFactor = 0.5f;
            hitboxRadius = 10;
           
            showHitbox = false;
           
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/megabeetle_spreadsheet_x32x32.png");           
        }
        /// <summary>
        /// Whether the object is supposed to go left or not.
        /// </summary>
        bool goleft = false;
        /// <summary>
        /// Whether the object is supposed to go right or not.
        /// </summary>
        bool goright = true;
        /// <summary>
        /// override Tick function of enemy. Adds behaviour patterns on top of base Tick() function of GameObject
        /// </summary>
        public override void Tick()
        {
            base.Tick();
            Velocity.Y = 200;
            if (Position.Y >= 200)
            {
                if (Position.X > 30 && Position.X < 60)
                {
                     goright = true;
                     goleft = false;
                }

                if (Position.X < 500 && Position.X>450)
                {
                    goright = false;
                    goleft = true;
                }

                if (goleft == true && goright == false)
                {
                    Velocity.X = -200;
                }

                if (goleft == false && goright == true)
                {
                    Velocity.X = 200;
                }

            }

        }

    }

    /// <summary>
    /// First enemy, so called Wasp, it goes down while and stops at certain height, afterwards it stops and shoots bullets while going left and right.
    /// </summary>
    public class Enemy2 : Ship
    {  
        public Enemy2(GameWorld world)
        {
            gameWorld = world;
            points = 20;
            life = 100;
            var rand2 = new Random();
            float offset = 3.5f;
            Position = new System.Numerics.Vector2(rand2.Next(100)*offset, -100);
            spriteSizeX = 32;
            spriteSizeY = 32;
            spriteCycle = 0;
            spriteCount = 2;
            transitionDuration = 100;
            slowFactor = 0.5f;
            hitboxRadius = 10;
     
            
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/wasp_spreadsheet_x32x32.png");

            // PROJECTILE 1
            fireRate1 = 1.2f;
            projectile = new Projectile(gameWorld);

            projectile.Velocity = new Vector2(0, 300);
            projectile.spriteSheet = BitmapFactory.FromResource("graphics/projectiles/projectile5_spreadsheet_x11x11.png");
            projectile.spriteSizeX = 11;
            projectile.spriteSizeY = 11;
            projectile.Scale = new Vector2(2, 2);
            /// <summary>
            /// Setting the collission mask of the projectile. Enemy projectiles only collide with Player, so mask masks everything else
            /// </summary>
            projectile.collisionMask = new string[] { "enemy", "enemy projectile", "player projectile" };
            projectile.collisionDamage = 10;
            projectile.tag = "enemy projectile";
        }
        bool goleft1 = false;
        bool goright1 = true;
        bool stop = false;
        /// <summary>
        /// override Tick function of enemy. Adds behaviour patterns on top of base Tick() function of GameObject
        /// </summary>
        public override void Tick()
        {
            base.Tick();
            Shoot(0);
            if (stop == false)
            {
                Velocity.Y = 150;
            }

            if (stop == true)
            {
                Velocity.Y = 0;
            }

            if (Position.Y >= 100)
            {
                stop = true;
                if (Position.X > 0 && Position.X < 30)
                {
                    goright1 = true;
                    goleft1 = false;
                }

                if (Position.X < 600 && Position.X > 550)
                {
                    goright1 = false;
                    goleft1 = true;
                }

                if (goleft1 == true && goright1 == false)
                {
                    Velocity.X = -300;
                }

                if (goleft1 == false && goright1 == true)
                {
                    Velocity.X = 300;
                }

            }

     
        }
    }
    /// <summary>
    /// Third enemy, so called Beetle, it goes down while bouncing of the walls at fast speed, does not shoot, only deals contact damage.
    /// </summary>
    public class Enemy3 : Ship
    {
        public Enemy3(GameWorld world)
        {
            gameWorld = world;
            points = 10;
            life = 50;
            Position = new System.Numerics.Vector2(300, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;
            spriteCycle = 0;
            spriteCount = 2;
            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 100;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/beetle_spreadsheet_x16x16.png");
        }

        bool goleft2 = false;
        bool goright2 = true;
        /// <summary>
        /// override Tick function of enemy. Adds behaviour patterns on top of base Tick() function of GameObject
        /// </summary>
        public override void Tick()
        {
            base.Tick();
            Velocity.Y = 50;
            if (Position.Y >= 100)
            {
                if (Position.X > 0 && Position.X < 30)
                {
                    goright2 = true;
                    goleft2 = false;
                }

                if (Position.X < 600 && Position.X > 550)
                {
                    goright2 = false;
                    goleft2 = true;
                }

                if (goleft2 == true && goright2 == false)
                {
                    Velocity.X = -400;
                }

                if (goleft2 == false && goright2 == true)
                {
                    Velocity.X = 400;
                }

            }

        }
    }
    /// <summary>
    /// Fourth enemy, so called Skitter, it bounces up, down, left, right and shoots while bouncing from top wall.
    /// </summary>
    public class Enemy4 : Ship
    {

        public Enemy4(GameWorld world)
        {
            gameWorld = world;
            points = 10;
            life = 50;
            Position = new System.Numerics.Vector2(100, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;
            spriteCycle = 0;
            spriteCount = 3;
            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 100;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/skitter_spreadsheet_x16x16.png");

            fireRate1 = 1f;

            projectile = new Projectile(gameWorld);

            projectile.Velocity = new Vector2(0, 400);
            projectile.spriteSheet = BitmapFactory.FromResource("graphics/projectiles/projectile4_spreadsheet_x11x11.png");
            projectile.spriteSizeX = 11;
            projectile.spriteSizeY = 11;
            projectile.Scale = new Vector2(1.5f, 1.5f);
            projectile.collisionMask = new string[] { "enemy", "enemy projectile", "player projectile" };
            projectile.collisionDamage = 20;
            projectile.tag = "enemy projectile";
        }
        bool goright3 = true;
        bool goleft3 = false;
        bool godown = true;
        bool goup = false;
        public override void Tick()
        {
            base.Tick();
            if (godown == true)
            {
                Velocity.Y = 300;
            }

            if (goup == true)
            {
                Velocity.Y = -300;
            }

            if (Position.Y>= 100 && Position.Y <= 105)
            {
                godown = true;
                goup = false;
                Shoot(0);
            }

            if (Position.Y >= 501)
            {
                godown = false;
                goup = true;
                
            }

            if (Position.Y >= 100 && Position.Y <=500)
            {
                if (Position.X > 0 && Position.X < 30)
                {
                    goright3 = true;
                    goleft3 = false;
                }

                if (Position.X < 600 && Position.X > 550)
                {
                    goright3 = false;
                    goleft3 = true;
                }


                if (goleft3 == true && goright3 == false)
                {
                    Velocity.X = -200;
                }

                if (goleft3 == false && goright3 == true)
                {
                    Velocity.X = 200;
                }

            }

        }
    }
    /// <summary>
    /// Fifth enemy, so called Disc, it goes down very slowly but bounces very fast, does not shoot.
    /// </summary>
    public class Enemy5 : Ship
    {
        public Enemy5(GameWorld world)
        {
            gameWorld = world;
            points = 5;
            life = 50;
            float offset = 3.5f;
            var rand2 = new Random();
            Position = new System.Numerics.Vector2(rand2.Next(100) * offset, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;
            spriteCycle = 0;
            spriteCount = 8;
            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 100;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/disc_spreadsheet_x16x16.png");
        }

        bool goleft = false;
        bool goright = true;
        /// <summary>
        /// override Tick function of enemy. Adds behaviour patterns on top of base Tick() function of GameObject
        /// </summary>
        public override void Tick()
        {
            base.Tick();
            Velocity.Y = 40;

            if (Position.Y >= 30)
            {
                if (Position.X > 0 && Position.X < 30)
                {
                    goright = true;
                    goleft = false;
                }

                if (Position.X < 600 && Position.X > 550)
                {
                    goright = false;
                    goleft = true;
                }


                if (goleft == true && goright == false)
                {
                    Velocity.X = -500;
                }

                if (goleft == false && goright == true)
                {
                    Velocity.X = 500;
                }

            }

        }
    }
    /// <summary>
    /// Sixth enemy, so called Eye, it goes down rapidly, can bounce but usually does not, does not shoot.
    /// </summary>
    public class Enemy6 : Ship
    {
        public Enemy6(GameWorld world)
        {
            gameWorld = world;
            points = 5;
            life = 10;
            var rand = new Random();
            Position = new System.Numerics.Vector2(rand.Next(100), -100);
            spriteSizeX = 12;
            spriteSizeY = 11;
            spriteCycle = 0;
            spriteCount = 5;
            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 100;
            showHitbox = false;
            Speed = new System.Numerics.Vector2(300, 300);
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/eye_spreadsheet_x16x16.png");
        }

        bool goleft = false;
        bool goright = true;
        /// <summary>
        /// override Tick function of enemy. Adds behaviour patterns on top of base Tick() function of GameObject
        /// </summary>
        public override void Tick()
        {
            base.Tick();
            Velocity.Y = 400;

            if (Position.Y >= 200)
            {
                if (Position.X > 0 && Position.X < 30)
                {
                    goright = true;
                    goleft = false;
                }

                if (Position.X < 600 && Position.X > 550)
                {
                    goright = false;
                    goleft = true;
                }


                if (goleft == true && goright == false)
                {
                    Velocity.X = -100;
                }

                if (goleft == false && goright == true)
                {
                    Velocity.X = 100;
                }

            }

        }
    }

    /// <summary>
    /// Seventh enemy, so called Drone, bounces from one wall to another but bounces faster on one side, shoots sparks down, spawns in swarms.
    /// </summary>
    public class Enemy7 : Ship
    {

        public Enemy7(GameWorld world)
        {
            gameWorld = world;
            points = 15;
            life = 10;
            Position = new System.Numerics.Vector2(300, -100);
            spriteSizeX = 12;
            spriteSizeY = 10;
            spriteCycle = 0;
            spriteCount = 5;
            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 100;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/drone2_spreadsheet_x12x10.png");

            fireRate1 = 5f;

            projectile = new Projectile(gameWorld);

            projectile.Velocity = new Vector2(0, 300);
            projectile.spriteSheet = BitmapFactory.FromResource("graphics/projectiles/projectile6_spreadsheet_x11x13.png");
            projectile.spriteSizeX = 11;
            projectile.spriteSizeY = 13;
            projectile.Scale = new Vector2(1, 1);
            projectile.collisionMask = new string[] { "enemy", "enemy projectile", "player projectile" };
            projectile.collisionDamage = 10;
            projectile.tag = "enemy projectile";

        }

        bool goleft2 = false;
        bool goright2 = true;
        /// <summary>
        /// override Tick function of enemy. Adds behaviour patterns on top of base Tick() function of GameObject
        /// </summary>
        public override void Tick()
        {
            base.Tick();
            Shoot(0);
            Velocity.Y = 100;
            if (Position.Y >= 0)
            {
                if (Position.X > 0 && Position.X < 20)
                {
                    goright2 = true;
                    goleft2 = false;
                }

                if (Position.X < 600 && Position.X > 550)
                {
                    goright2 = false;
                    goleft2 = true;
                }


                if (goleft2 == true && goright2 == false)
                {
                    Velocity.X = -400;
                }

                if (goleft2 == false && goright2 == true)
                {
                    Velocity.X = 100;
                }

            }

        }
    }

    /// <summary>
    /// Eighth enemy, so called Fish, acts like a skitter but doesn't shoot, usually announces the boss.
    /// </summary>
    public class Enemy8 : Ship
    {
        public Enemy8(GameWorld world)
        {
            gameWorld = world;
            points = 10;
            life = 50;
            Position = new System.Numerics.Vector2(100, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;
            spriteCycle = 0;
            spriteCount = 3;
            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 100;
            showHitbox = false;
            Speed = new System.Numerics.Vector2(300, 300);
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/fish_spreadsheet_x16x16.png");
        }
        bool goright3 = true;
        bool goleft3 = false;
        bool godown = true;
        bool goup = false;
        /// <summary>
        /// override Tick function of enemy. Adds behaviour patterns on top of base Tick() function of GameObject
        /// </summary>
        public override void Tick()
        {
            base.Tick();
            if (godown == true)
            {
                Velocity.Y = 350;
            }

            if (goup == true)
            {
                Velocity.Y = -350;
            }

            if (Position.Y >= 100 && Position.Y <= 105)
            {
                godown = true;
                goup = false;
            }

            if (Position.Y >= 501)
            {
                godown = false;
                goup = true;

            }

            if (Position.Y >= 100 && Position.Y <= 600)
            {
                if (Position.X > 0 && Position.X < 30)
                {
                    goright3 = true;
                    goleft3 = false;
                }

                if (Position.X < 600 && Position.X > 550)
                {
                    goright3 = false;
                    goleft3 = true;
                }


                if (goleft3 == true && goright3 == false)
                {
                    Velocity.X = -350;
                }

                if (goleft3 == false && goright3 == true)
                {
                    Velocity.X = 350;
                }

            }

        }
    }

    /// <summary>
    /// Ninth enemy, so called Brain, goes down rapidly but stops to form a line, fires in burst, very high damage and toughness.
    /// </summary>
    public class Enemy9 : Ship
    {
        public Enemy9(GameWorld world)
        {
            gameWorld = world;
            points = 40;
            life = 1000;
            var rand2 = new Random();
            float offset = 3.5f;
            Position = new System.Numerics.Vector2(rand2.Next(100) * offset, -100);
            spriteSizeX = 26;
            spriteSizeY = 30;
            spriteCycle = 0;
            spriteCount = 3;
            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/brain_spreadsheet_x26x30.png");

            explosionSize = 6;
            fireRate1 = 4f;

            projectile = new Projectile(gameWorld);

            projectile.Velocity = new Vector2(0, 200);

            projectile.spriteSheet = BitmapFactory.FromResource("graphics/projectiles/projectile6_spreadsheet_x11x13.png");
            projectile.spriteSizeX = 11;
            projectile.spriteSizeY = 13;
            projectile.Scale = new Vector2(2, 2);
            projectile.collisionMask = new string[] { "enemy", "enemy projectile", "player projectile" };
            projectile.collisionDamage = 30;
            projectile.tag = "enemy projectile";
        }

        int projectileBurst = 4;
        int burstI = 0;

        bool goleft1 = false;
        bool goright1 = true;
        bool stop = false;

        /// <summary>
        /// override Tick function of enemy. Adds behaviour patterns on top of base Tick() function of GameObject
        /// </summary>
        public override void Tick()
        {
            base.Tick();
            
            if (stop == false)
            {
                Velocity.Y = 1000;
            }

            if (stop == true)
            {
                Velocity.Y = 0;
                Shoot(burstI);
            }

            if (Position.Y >= 300)
            {
                stop = true;
                if (Position.X > 0 && Position.X < 30)
                {
                    goright1 = true;
                    goleft1 = false;
                }

                if (Position.X < 600 && Position.X > 550)
                {
                    goright1 = false;
                    goleft1 = true;
                }


                if (goleft1 == true && goright1 == false)
                {
                    Velocity.X = -50;
                }

                if (goleft1 == false && goright1 == true)
                {
                    Velocity.X = 50;
                }

            }
        }
        /// <summary>
        /// Overrides shoot to fire in burst.
        /// </summary>
        public override void Shoot(int type)
        {
            if (canShoot1)
            {
                canShoot1 = false;
                if (burstI < projectileBurst)
                {
                    burstI++;
                    canShootNext1 = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 100);
                } else
                {
                    burstI = 0;
                    canShootNext1 = TimeSpan.FromMilliseconds(gameWorld.GameTime() + fireRate1 * 1000);
                }
                projectile.Velocity = Vector2.Normalize(gameWorld.player.Position - Position) * 200;
                Vector2 posA = new Vector2(Position.X - spriteSizeX, Position.Y);
                Vector2 posB = new Vector2(Position.X + spriteSizeX, Position.Y);
                Projectile copyA = projectile.Copy(posA);
                gameWorld.gameObjects.Add(copyA);

                Projectile copyB = projectile.Copy(posB);
                gameWorld.gameObjects.Add(copyB);
            }
        }
    }

    /// <summary>
    /// Boss, so called Boss Snail, very slow but extremely high hp and difficult pattern of shooting to avoid.
    /// </summary>
    public class Boss : Ship
    {
        public Boss(GameWorld world)
        {
            gameWorld = world;
            points = 500;
            life = 5000;
            Position = new System.Numerics.Vector2(250, -250);
            spriteSizeX = 92;
            spriteSizeY = 114;
            spriteCycle = 0;
            slowFactor = 0.5f;
            hitboxRadius = 40;
            spriteCount = 3;
            transitionDuration = 200;
            collisionDamage = 1000;
            boundToWindow = false;
            showHitbox = false;
            deleteOffscreen = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/boss_snail_spreadsheet_x92x114.png");


            fireRate1 = 0.01f;

            explosionSize = 10;

            projectile = new Projectile(gameWorld);
            var rand = new Random();
            projectile.Velocity = new Vector2(rand.Next(100)-50, 200);
            projectile.spriteSheet = BitmapFactory.FromResource("graphics/projectiles/projectile3_spreadsheet_x11x11.png");
            projectile.spriteSizeX = 11;
            projectile.spriteSizeY = 11;
            projectile.Scale = new Vector2(2, 2);
            projectile.collisionMask = new string[] { "enemy", "enemy projectile", "player projectile" };
            projectile.collisionDamage = 30;
            projectile.tag = "enemy projectile";
        }

        bool goleft1 = false;
        bool goright1 = true;
        bool stop = false;

        /// <summary>
        /// override Tick function of enemy. Adds behaviour patterns on top of base Tick() function of GameObject
        /// </summary>
        public override void Tick()
        {
            var rand = new Random();
            projectile.Velocity = new Vector2(rand.Next(200) - 100, 200);
            base.Tick();
            if (stop == false)
            {
                Velocity.Y = 50;
            }

            if (stop == true)
            {
                Velocity.Y = 0;
            }

            if ((int) Position.X % 10 == 0 && stop == true)
            {
                Shoot(0);
            }

            if (Position.Y >= 100)
            {
                stop = true;
                if (Position.X > 30 && Position.X < 60)
                {
                    goright1 = true;
                    goleft1 = false;
                }

                if (Position.X < 500 && Position.X > 450)
                {
                    goright1 = false;
                    goleft1 = true;
                }


                if (goleft1 == true && goright1 == false)
                {
                    Velocity.X = -10;
                }

                if (goleft1 == false && goright1 == true)
                {
                    Velocity.X = 10;
                }
            }
        }
    }
}
