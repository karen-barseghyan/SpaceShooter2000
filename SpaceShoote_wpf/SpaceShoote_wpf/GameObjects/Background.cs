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

    public class Bar : GameObject
    {
        private Color color;
        public Vector3 startColor = new Vector3(255, 255, 255);
        public Vector3 endColor = new Vector3(50, 0, 0);
        public float percent;

        public Bar(MainWindow mainwindow, GameWorld world)
        {
            gameWorld = world;
            mainWindow = mainwindow;
            checkCollisions = false;
            spriteSizeX = mainwindow.width;
            spriteSizeY = 8;
            Position = new Vector2(0, mainwindow.height - spriteSizeY);
            
            startColor = new Vector3(100, 255, 100);
            endColor = new Vector3(100, 0, 0);

            color = Color.FromRgb(100, 255, 100);
        }

        /// <summary>
        /// Draws health bar, positionned relative to the top-left corner
        /// </summary>
        /// <param name="surface"></param>
        public override void Draw(WriteableBitmap surface)
        {
            if (percent > 0)
            {
                spriteSizeX = (int)(mainWindow.width * percent / 1000);
                Vector3 c = Vector3.Lerp(endColor, startColor, percent / 1000);
                color = Color.FromRgb((byte)c.X, (byte)c.Y, (byte)c.Z);
                surface.FillRectangle((int)Position.X, (int)Position.Y, (int)Position.X + spriteSizeX, (int)Position.Y + spriteSizeY, color);
            }
        }
    }

    public class TextImage : GameObject
    {
        public string name = "Press";
        List<GameObject> numbers;
        bool simple = true;
        public TextImage(MainWindow mainwindow, GameWorld world, string text)
        {
            gameWorld = world;
            mainWindow = mainwindow;
            showHitbox = false;
            transitionDuration = 0;

            name = text;
            try
            {
                spriteSheet = BitmapFactory.FromResource("graphics/ui/" + name + mainWindow.language + ".png");
            }
            catch
            {
                spriteSheet = BitmapFactory.FromResource("graphics/ui/font_spreadsheet_x11x16.png");
            }
        }
        public TextImage(MainWindow mainwindow, GameWorld world, float score, bool _simple)
        {
            simple = false;
            gameWorld = world;
            mainWindow = mainwindow;
            showHitbox = false;
            transitionDuration = 0;
            spriteCount = 10;
            spriteSheet = BitmapFactory.FromResource("graphics/ui/num_spreadsheet_x11x16.png");
            numbers = new List<GameObject>();

            int t = (int)score;
            while (t > 0)
            {
                int div = t / 10;
                int num = t - (10*div);
                numbers.Add(Copy(num));
                t /= 10;
            }
        }

        public TextImage(MainWindow mainwindow, GameWorld world, int num)
        {
            gameWorld = world;
            mainWindow = mainwindow;
            transitionDuration = 0;
            spriteCount = 10;
            showHitbox = false;
            spriteSizeX = 11;
            spriteSizeY = 16;
            spriteCycle = num;
        }

        public TextImage Copy(int num)
        {
            TextImage p = new TextImage(mainWindow, gameWorld, num);
            p.spriteSheet = spriteSheet;
            p.Scale = new Vector2(2, 2);
            return p;
        }

        public override void Draw(WriteableBitmap surface)
        {
            
            if (simple)
            {
                base.Draw(surface);
            } else
            {
                //MessageBox.Show(simple.ToString());
                for (int i = numbers.Count - 1; i >= 0; i--)
                {
                    float offset = (numbers.Count - 1 - i) * spriteSizeX * Scale.X;
                    numbers[i].Position = new Vector2(Position.X + offset, Position.Y);
                    numbers[i].Draw(surface);
                }
            }
        }
    }
}
