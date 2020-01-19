using SpaceShoote_wpf.GameWorlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
    class BackgroundLayer1 : Ship
    {
        //player constructor with main window as parameter for reference
        public BackgroundLayer1(MainWindow mainwindow, GameWorld world)
        {
            gameWorld = world;
            Position = new System.Numerics.Vector2(300, -1500);
            mainWindow = mainwindow;
            spriteSizeX = 500;
            spriteSizeY = 2000;
            Scale.X = 1.5f;
            Scale.Y = 1f;
            Speed = new Vector2(0, -100);
            boundToWindow = true;
            spriteSheet = BitmapFactory.FromResource("graphics/ui/background_stars_layer1.png");

        }

 


        //player constructor with main window for reference and starting position
        public BackgroundLayer1(MainWindow mainwindow, float posx, float posy)
        {
            Position = new System.Numerics.Vector2(300, -1500);
            mainWindow = mainwindow;
        }

        // tick function of player, runs every frame
        public override void Tick()
        {
            float x = 0;
            float y = 0;
            Position = Position -= Speed * 20 / 1000f;
            
            if (Position.Y > 646)
            {
                //  Velocity = new Vector2(0, 3000);
                Position = new System.Numerics.Vector2(300, -1500);
            } 
            
            base.Tick();
  
        }


        public override void Draw(WriteableBitmap surface)
        {
            //base.Draw(surface);
            // rectangle to crop from the sprite sheet

            Rect sourceRect = new Rect(animoffset * spriteSizeX, 0, spriteSizeX, spriteSizeY);
            // destination where to apply cropped image from the sprite sheet
            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
            // apply cropped image to writablebitmap
            surface.Blit(destRect, spriteSheet, sourceRect);
        }
    }
    
    class BackgroundLayer2 : Ship
    {
        //player constructor with main window as parameter for reference
        public BackgroundLayer2(MainWindow mainwindow, GameWorld world)
        {
            gameWorld = world;
            Position = new System.Numerics.Vector2(300, -1500);
            mainWindow = mainwindow;
            spriteSizeX = 500;
            spriteSizeY = 2000;
            Scale.X = 1.5f;
            Scale.Y = 1f;
            Speed = new Vector2(0, -150);
            boundToWindow = true;
            spriteSheet = BitmapFactory.FromResource("graphics/ui/background_stars_layer2.png");

        }




        //player constructor with main window for reference and starting position
        public BackgroundLayer2(MainWindow mainwindow, float posx, float posy)
        {
            Position = new System.Numerics.Vector2(posx, posy);
            mainWindow = mainwindow;
        }

        // tick function of player, runs every frame
        public override void Tick()
        {
            float x = 0;
            float y = 0;
            Position = Position -= Speed * 20 / 1000f;

            if (Position.Y > 646)
            {
                //  Velocity = new Vector2(0, 3000);
                Position = new System.Numerics.Vector2(300, -1500);
            }

            base.Tick();
        }


        public override void Draw(WriteableBitmap surface)
        {
            //base.Draw(surface);
            // rectangle to crop from the sprite sheet
            Rect sourceRect = new Rect(animoffset * spriteSizeX, 0, spriteSizeX, spriteSizeY);
            // destination where to apply cropped image from the sprite sheet
            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
            // apply cropped image to writablebitmap
            surface.Blit(destRect, spriteSheet, sourceRect);
        }
    }

    class Health : Ship
    {
        //player constructor with main window as parameter for reference
        public Health(MainWindow mainwindow, GameWorld world)
        {
            gameWorld = world;
            Position = new System.Numerics.Vector2(30, 750);
            mainWindow = mainwindow;
            spriteSizeX = 10;
            spriteSizeY = 9;
            Scale.X = 5f;
            Scale.Y = 5f;
            //Speed = new Vector2(0, -150);
            boundToWindow = true;
            spriteSheet = BitmapFactory.FromResource("graphics/powerups/life_spreadsheet_x10x9.png");

        }




        //player constructor with main window for reference and starting position
        public Health(MainWindow mainwindow, float posx, float posy)
        {
            Position = new System.Numerics.Vector2(posx, posy);
            mainWindow = mainwindow;
        }

        // tick function of player, runs every frame

        bool increase = false;
        bool decrease = true;
        public override void Tick()
        {
            if (Scale.X <= 1f)
            {
                increase = true;
                decrease = false;
            }

            if (Scale.X >= 5f)
            {
                increase = false;
                decrease = true;
            }

            if (increase == true)
            {
                Scale.X = Scale.X + 0.1f;
                Scale.Y = Scale.Y + 0.1f;
            }

            if (decrease == true)
            {
                Scale.X = Scale.X - 0.1f;
                Scale.Y = Scale.Y - 0.1f;
            }

            base.Tick();
        }


        public override void Draw(WriteableBitmap surface)
        {
            //base.Draw(surface);
            // rectangle to crop from the sprite sheet
            Rect sourceRect = new Rect(animoffset * spriteSizeX, 0, spriteSizeX, spriteSizeY);
            // destination where to apply cropped image from the sprite sheet
            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
            // apply cropped image to writablebitmap
            surface.Blit(destRect, spriteSheet, sourceRect);
        }
    }
}
