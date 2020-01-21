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
            checkCollisions = false;
            spriteSheet = BitmapFactory.FromResource("graphics/ui/background_stars_layer1.png");

        }

 


        //player constructor with main window for reference and starting position
        public BackgroundLayer1(MainWindow mainwindow, float posx, float posy)
        {
            Position = new System.Numerics.Vector2(300, -1500);
            mainWindow = mainwindow;
            checkCollisions = false;
        }

        // tick function of player, runs every frame
        public override void Tick()
        {
            float x = 0;
            float y = 0;
            Position = Position -= Speed * 20 / 1000f;
            
            if (Position.Y > 643)
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
            checkCollisions = false;

        }




        //player constructor with main window for reference and starting position
        public BackgroundLayer2(MainWindow mainwindow, float posx, float posy)
        {
            Position = new System.Numerics.Vector2(posx, posy);
            mainWindow = mainwindow;
            checkCollisions = false;

        }

        // tick function of player, runs every frame
        public override void Tick()
        {
            float x = 0;
            float y = 0;
            Position = Position -= Speed * 20 / 1000f;

            if (Position.Y > 643)
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
        private Color color = Color.FromRgb(255, 0, 0);
        private Vector3 endColor = new Vector3(100, 255, 100);
        private Vector3 startColor = new Vector3(100, 0, 0);
        //player constructor with main window as parameter for reference
        public Health(MainWindow mainwindow, GameWorld world)
        {
            gameWorld = world;
            mainWindow = mainwindow;
            spriteSizeX = mainWindow.width;
            spriteSizeY = 8;
            Position = new System.Numerics.Vector2(0, mainWindow.height - spriteSizeY);
            Scale.X = 1f;
            Scale.Y = 1f;
            //Speed = new Vector2(0, -150);
            boundToWindow = false;
            //spriteSheet = BitmapFactory.FromResource("graphics/powerups/life_spreadsheet_x10x9.png");
            checkCollisions = false;

        }

        // tick function of player, runs every frame
        public override void Tick()
        {
            if (mainWindow.player.life > 0)
            {
                spriteSizeX = (int)(mainWindow.width * mainWindow.player.life / 1000);
                Vector3 c = Vector3.Lerp(startColor, endColor, mainWindow.player.life / 1000);
                color = Color.FromRgb((byte)c.X, (byte)c.Y, (byte)c.Z);
            }
                
            else
            {
                Vector3 c = Vector3.Lerp(startColor, endColor, mainWindow.player.life / 1000);
                color = Color.FromRgb(0, 0, 0);
            }

            
            /*
            
                Scale = Vector2.One * 6 * (mainWindow.player.life) / 1000;
            else
                Scale = new Vector2(0.1f, 0.1f);
            base.Tick();
            */
        }


        public override void Draw(WriteableBitmap surface)
        {
            surface.FillRectangle((int)Position.X, (int)Position.Y, (int)Position.X + (int)spriteSizeX, (int)Position.Y + spriteSizeY, color);
            /*
            Rect sourceRect = new Rect(animoffset * spriteSizeX, 0, spriteSizeX, spriteSizeY);

            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);

            surface.Blit(destRect, spriteSheet, sourceRect);
            */
        }
    }
}
