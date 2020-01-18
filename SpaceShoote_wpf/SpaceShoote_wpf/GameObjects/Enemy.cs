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

    //MegaBeetle
    public class Enemy : Ship
    {
        public int points = 10;

        public Enemy(MainWindow mainwindow, GameWorld world)
        {
 
            mainWindow = mainwindow;
            gameWorld = world;
            Position = new System.Numerics.Vector2(100, -100);
            spriteSizeX = 32;
            spriteSizeY = 32;
            
            spriteCycle = 1;
          
            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;

           // Speed = new System.Numerics.Vector2(0, -1000);

            spriteSheet = BitmapFactory.FromResource("graphics/aliens/megabeetle_spreadsheet_x32x32.png");
            
        }

        bool goleft = false;
        bool goright = true;

        public override void Tick()
        {
            //   Position = Position -= Speed * 2 / 1000f;

            Position.Y = Position.Y + 3f;

            if (Position.Y >= 200)
            {
                //Speed = new System.Numerics.Vector2(1000, -1000);
                //   showHitbox = true;
               // Position.Y = Position.Y + 5f;
                if (Position.X > 30 && Position.X < 60)
                {
                    //  Position.X = Position.X + 5f;
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
                    Position.X = Position.X - 5f;
                }

                if (goleft == false && goright == true)
                {
                    Position.X = Position.X + 5f;
                }

            }

            base.Tick();
        }

    }


    public class Enemy2 : Ship
    {
        public int points = 10;

        public Enemy2(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            Position = new System.Numerics.Vector2(200, -100);
            spriteSizeX = 32;
            spriteSizeY = 32;

            spriteCycle = 1;

            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;

            Speed = new System.Numerics.Vector2(300, 300);

            spriteSheet = BitmapFactory.FromResource("graphics/aliens/wasp_spreadsheet_x32x32.png");
        }
    }

    public class Enemy3 : Ship
    {
        public int points = 10;

        public Enemy3(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            Position = new System.Numerics.Vector2(300, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;

            spriteCycle = 1;

            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 200;
            showHitbox = false;

            Speed = new System.Numerics.Vector2(300, 300);

            spriteSheet = BitmapFactory.FromResource("graphics/aliens/disc_spreadsheet_x16x16.png");
        }
    }

    public class Enemy4 : Ship
    {
        public int points = 10;

        public Enemy4(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            Position = new System.Numerics.Vector2(400, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;

            spriteCycle = 1;

            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;

            Speed = new System.Numerics.Vector2(300, 300);

            spriteSheet = BitmapFactory.FromResource("graphics/aliens/beetle_spreadsheet_x16x16.png");
        }
    }

    public class Enemy5 : Ship
    {
        public int points = 10;

        public Enemy5(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            Position = new System.Numerics.Vector2(500, -100);
            spriteSizeX = 16;
            spriteSizeY = 16;

            spriteCycle = 1;

            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 200;
            showHitbox = false;

            Speed = new System.Numerics.Vector2(300, 300);

            spriteSheet = BitmapFactory.FromResource("graphics/aliens/skitter_spreadsheet_x16x16.png");
        }
    }

    public class Enemy6 : Ship
    {
        public int points = 10;

        public Enemy6(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            Position = new System.Numerics.Vector2(600, -100);
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
    }

    public class Enemy7 : Ship
    {
        public int points = 10;

        public Enemy7(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            Position = new System.Numerics.Vector2(150, -100);
            spriteSizeX = 12;
            spriteSizeY = 11;

            spriteCycle = 1;

            slowFactor = 0.5f;
            hitboxRadius = 4;
            transitionDuration = 200;
            showHitbox = false;

            Speed = new System.Numerics.Vector2(300, 300);

            spriteSheet = BitmapFactory.FromResource("graphics/aliens/drone1_spreadsheet_x12x11.png");
        }
    }

}
