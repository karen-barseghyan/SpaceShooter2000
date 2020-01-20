using SpaceShoote_wpf.GameWorlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
           
    public class Enemy1 : Ship
    {
 

        public Enemy1(MainWindow mainwindow, GameWorld world)
        {
 
            mainWindow = mainwindow;
            gameWorld = world;
            points = 30;
            var rand = new Random();
            Position = new System.Numerics.Vector2(rand.Next(500), -100);
            spriteSizeX = 32;
            spriteSizeY = 32;           
            spriteCycle = 1;  
            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/megabeetle_spreadsheet_x32x32.png");           
        }
        bool goleft = false;
        bool goright = true;
        public override void Tick()
        {
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


    public class Enemy2 : Ship
    {  
        public Enemy2(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            points = 20;
            var rand2 = new Random();
            float offset = 3.5f;
            Position = new System.Numerics.Vector2(rand2.Next(100)*offset, -100);
            spriteSizeX = 32;
            spriteSizeY = 32;
            spriteCycle = 1;
            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/wasp_spreadsheet_x32x32.png");
        }
        bool goleft1 = false;
        bool goright1 = true;
        bool stop = false;  
        public override void Tick()
        {
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
                    Velocity.X = -300;
                }

                if (goleft1 == false && goright1 == true)
                {
                    Velocity.X = 300;
                }

            }

     
        }
    }

    public class Enemy3 : Ship
    {
        public Enemy3(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            points = 10;
            Position = new System.Numerics.Vector2(300, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;
            spriteCycle = 1;
            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 200;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/beetle_spreadsheet_x16x16.png");
        }

        bool goleft2 = false;
        bool goright2 = true;
        public override void Tick()
        {
            Velocity.Y = 50;
            if (Position.Y >= 100)
            {
                if (Position.X > 30 && Position.X < 60)
                {
                    goright2 = true;
                    goleft2 = false;
                }

                if (Position.X < 500 && Position.X > 450)
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

    public class Enemy4 : Ship
    {

        public Enemy4(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            points = 10;
            Position = new System.Numerics.Vector2(100, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;
            spriteCycle = 1;
            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/skitter_spreadsheet_x16x16.png");
        }
        bool goright3 = true;
        bool goleft3 = false;
        bool godown = true;
        bool goup = false;
        public override void Tick()
        {
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
            }

            if (Position.Y >= 501)
            {
                godown = false;
                goup = true;
                
            }

            if (Position.Y >= 100 && Position.Y <=500)
            {
                if (Position.X > 30 && Position.X < 60)
                {
                    goright3 = true;
                    goleft3 = false;
                }

                if (Position.X < 500 && Position.X > 450)
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

    public class Enemy5 : Ship
    {
        public Enemy5(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            points = 5;
            float offset = 3.5f;
            var rand2 = new Random();
            Position = new System.Numerics.Vector2(rand2.Next(100) * offset, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;
            spriteCycle = 1;
            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 200;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/disc_spreadsheet_x16x16.png");
        }

        bool goleft = false;
        bool goright = true;

        public override void Tick()
        {
            Velocity.Y = 10;

            if (Position.Y >= 200)
            {
                if (Position.X > 30 && Position.X < 60)
                {
                    goright = true;
                    goleft = false;
                }

                if (Position.X < 500 && Position.X > 450)
                {
                    goright = false;
                    goleft = true;
                }


                if (goleft == true && goright == false)
                {
                    Velocity.X = -1000;
                }

                if (goleft == false && goright == true)
                {
                    Velocity.X = 1000;
                }

            }

        }
    }

    public class Enemy6 : Ship
    {
        public Enemy6(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            points = 5;
            var rand = new Random();
            Position = new System.Numerics.Vector2(rand.Next(100), -100);
            spriteSizeX = 12;
            spriteSizeY = 11;
            spriteCycle = 1;
            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 200;
            showHitbox = false;
            Speed = new System.Numerics.Vector2(300, 300);
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/eye_spreadsheet_x16x16.png");
        }

        bool goleft = false;
        bool goright = true;

        public override void Tick()
        {
            Velocity.Y = 400;

            if (Position.Y >= 200)
            {
                if (Position.X > 30 && Position.X < 60)
                {
                    goright = true;
                    goleft = false;
                }

                if (Position.X < 500 && Position.X > 450)
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

    public class Enemy7 : Ship
    {

        public Enemy7(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            points = 15;
            Position = new System.Numerics.Vector2(300, -100);
            spriteSizeX = 12;
            spriteSizeY = 10;
            spriteCycle = 1;
            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 200;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/drone2_spreadsheet_x12x10.png");
        }

        bool goleft2 = false;
        bool goright2 = true;
        public override void Tick()
        {
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

    public class Enemy8 : Ship
    {
        public Enemy8(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            points = 10;
            Position = new System.Numerics.Vector2(100, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;
            spriteCycle = 1;
            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;
            Speed = new System.Numerics.Vector2(300, 300);
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/fish_spreadsheet_x16x16.png");
        }
        bool goright3 = true;
        bool goleft3 = false;
        bool godown = true;
        bool goup = false;
        public override void Tick()
        {
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
                if (Position.X > 30 && Position.X < 60)
                {
                    goright3 = true;
                    goleft3 = false;
                }

                if (Position.X < 500 && Position.X > 450)
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

    public class Enemy9 : Ship
    {
        public Enemy9(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            points = 40;
            var rand2 = new Random();
            float offset = 3.5f;
            Position = new System.Numerics.Vector2(rand2.Next(100) * offset, -100);
            spriteSizeX = 26;
            spriteSizeY = 30;
            spriteCycle = 1;
            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/brain_spreadsheet_x26x30.png");
        }


        bool goleft1 = false;
        bool goright1 = true;
        bool stop = false;

        public override void Tick()
        {
            if (stop == false)
            {
                Velocity.Y = 1000;
            }

            if (stop == true)
            {
                Velocity.Y = 0;
            }

            if (Position.Y >= 300)
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
                    Velocity.X = -50;
                }

                if (goleft1 == false && goright1 == true)
                {
                    Velocity.X = 50;
                }

            }


        }
    }

    public class Boss : Ship
    {
        public Boss(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            points = 500;
            Position = new System.Numerics.Vector2(250, -100);
            spriteSizeX = 92;
            spriteSizeY = 114;
            spriteCycle = 1;
            slowFactor = 0.5f;
            hitboxRadius = 4;
            transitionDuration = 200;
            showHitbox = false;
            spriteSheet = BitmapFactory.FromResource("graphics/aliens/boss_snail_spreadsheet_x92x114.png");
        }

        bool goleft1 = false;
        bool goright1 = true;
        bool stop = false;

        public override void Tick()
        {

            if (stop == false)
            {
                Velocity.Y = 10;
            }

            if (stop == true)
            {
                Velocity.Y = 0;
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
