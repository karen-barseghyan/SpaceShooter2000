using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameObjects
{
    /// <summary>
    /// First layer of a two-layer background.
    /// </summary>
    public class BackgroundLayer1 : GameObject
    {
        /// <summary>
        /// Constructor with game World as parameter for reference.
        /// </summary>
        /// <param name="world">World that the object is added to. </param>
        public BackgroundLayer1(GameWorld world)
        {
            gameWorld = world;
            Position = new Vector2(300, -1500);
            spriteSizeX = 500;
            spriteSizeY = 2000;
            Scale.X = 1.5f;
            Scale.Y = 1f;
            Speed = new Vector2(0, -100);
            boundToWindow = true;
            checkCollisions = false;
            spriteSheet = BitmapFactory.FromResource("graphics/ui/background_stars_layer1.png");
        }

        /// <summary>
        /// Simplified Tick function of GameObject
        /// </summary>
        public override void Tick()
        {
            /// <summary>
            /// Speed at which sprite goes in default.
            /// </summary>
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
            Rect sourceRect = new Rect(animoffset * spriteSizeX, 0, spriteSizeX, spriteSizeY);
            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
            surface.Blit(destRect, spriteSheet, sourceRect);
        }
    }
    /// <summary>
    /// Second layer of a two-layer background.
    /// </summary>
    public class BackgroundLayer2 : GameObject
    {

        public BackgroundLayer2(GameWorld world)
        {
            gameWorld = world;
            Position = new System.Numerics.Vector2(300, -1500);
            spriteSizeX = 500;
            spriteSizeY = 2000;
            Scale.X = 1.5f;
            Scale.Y = 1f;
            Speed = new Vector2(0, -150);
            boundToWindow = true;
            spriteSheet = BitmapFactory.FromResource("graphics/ui/background_stars_layer2.png");
            checkCollisions = false;
        }

        public override void Tick()
        {
            Position = Position -= Speed * 20 / 1000f;

            if (Position.Y > 643)
            {
                Position = new System.Numerics.Vector2(300, -1500);
            }

            base.Tick();
        }


        public override void Draw(WriteableBitmap surface)
        {
            Rect sourceRect = new Rect(animoffset * spriteSizeX, 0, spriteSizeX, spriteSizeY);
            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
            surface.Blit(destRect, spriteSheet, sourceRect);
        }
    }

    /// <summary>
    /// Bar - ui element for displaying a number in a visualy pleasing way. en. health bars, timer.
    /// it is a thin (8px high) and full-window wide. Positions relative to it's top-left corner and retracts to the left 
    /// </summary>
    public class Bar : GameObject
    {
        /// <summary>
        /// Current color of the bar. Interpolates between startColor and endColor.
        /// </summary>
        private Color color;
        /// <summary>
        /// Starting color of the bar. Present when percent is equal to 1000
        /// </summary>
        public Vector3 startColor = new Vector3(255, 255, 255);
        /// <summary>
        /// Starting color of the bar. Present when percent is equal to 0
        /// </summary>
        public Vector3 endColor = new Vector3(50, 0, 0);
        /// <summary>
        /// Factor for interpolation between colors. Number from 0-1000. Above 1000 becomes wider than window
        /// </summary>
        public float percent;

        /// <summary>
        /// Constructor with gameWorld as parameter for reference.
        /// </summary>
        /// <param name="world">World that the object is added to. </param>
        public Bar(GameWorld world)
        {
            gameWorld = world;
            checkCollisions = false;
            spriteSizeX = (int)gameWorld.windowSize.X;
            spriteSizeY = 8;
            Position = new Vector2(0, gameWorld.windowSize.Y - spriteSizeY);
            
            startColor = new Vector3(100, 255, 100);
            endColor = new Vector3(100, 0, 0);

            color = Color.FromRgb(100, 255, 100);
        }

        /// <summary>
        /// Draws the bar, positionned relative to the top-left corner
        /// </summary>
        /// <param name="surface"></param>
        public override void Draw(WriteableBitmap surface)
        {
            if (percent > 0)
            {
                spriteSizeX = (int)(gameWorld.windowSize.X * percent / 1000);
                Vector3 c = Vector3.Lerp(endColor, startColor, percent / 1000);
                color = Color.FromRgb((byte)c.X, (byte)c.Y, (byte)c.Z);
                surface.FillRectangle((int)Position.X, (int)Position.Y, (int)Position.X + spriteSizeX, (int)Position.Y + spriteSizeY, color);
            }
        }
    }

    /// <summary>
    /// Image Text class, used for displaying images that represent text on screen.
    /// Also includes generating numbers (use the TextImage(GameWorld world, float score, bool _simple) constructor)
    /// </summary>
    public class TextImage : GameObject
    {
        /// <summary>
        /// name of the image. Adds this to path string to find correct recourse. Make sure it makes a correct path
        /// </summary>
        public string name = "Press";
        /// <summary>
        /// List of digits for generating numbers. Empty if it is just an imageText
        /// </summary>
        public List<GameObject> numbers;
        
        bool simple = true;
        /// <summary>
        /// Simple constructor for TextImage
        /// </summary>
        /// <param name="text"> Add name part in text param for path string</param>
        public TextImage(GameWorld world, string text)
        {
            gameWorld = world;
            showHitbox = false;
            transitionDuration = 0;

            name = text;
            try
            {
                spriteSheet = BitmapFactory.FromResource("graphics/ui/" + name + gameWorld.language + ".png");
            }
            catch
            {
                spriteSheet = BitmapFactory.FromResource("graphics/ui/font_spreadsheet_x11x16.png");
            }
        }
        /// <summary>
        /// Constructor for number generation
        /// </summary>
        /// <param name="world">world reference</param>
        /// <param name="score">number you want to generate</param>
        /// <param name="_simple">bool to turn of simple mode</param>
        public TextImage(GameWorld world, float score, bool _simple)
        {
            simple = false;
            gameWorld = world;
            showHitbox = false;
            transitionDuration = 0;
            spriteCount = 10;
            spriteSheet = BitmapFactory.FromResource("graphics/ui/num_spreadsheet_x11x16.png");
            numbers = new List<GameObject>();

            int t = (int)score;
            if (t == 0)
            {
                numbers.Add(Copy(0));
            } else
            {
                while (t > 0)
                {
                    int div = t / 10;
                    int num = t - (10 * div);
                    numbers.Add(Copy(num));
                    t /= 10;
                }
            }
        }
        /// <summary>
        /// Simple constructor for digits
        /// </summary>
        /// <param name="world">World reference</param>
        /// <param name="num">which digit you want to represent (0-10)</param>
        public TextImage(GameWorld world, int num)
        {
            gameWorld = world;
            transitionDuration = 0;
            spriteCount = 10;
            showHitbox = false;
            spriteSizeX = 11;
            spriteSizeY = 16;
            spriteCycle = num;
        }
        
        /// <summary>
        /// Copies TextImage to avoid Creating a new bitmap to save processing time
        /// </summary>
        /// <param name="num">Cuts out Desired Digit</param>
        /// <returns>returns new Copy object reference</returns>
        public TextImage Copy(int num)
        {
            TextImage p = new TextImage(gameWorld, num);
            p.spriteSheet = spriteSheet;
            p.Scale = Scale;
            return p;
        }
        /// <summary>
        /// updates the number for generated number display
        /// </summary>
        /// <param name="score">new number to display</param>
        public void UpdateNumber(float score)
        {
            numbers = new List<GameObject>();

            int t = (int)score;
            if (t == 0)
            {
                numbers.Add(Copy(0));
            }
            else
            {
                while (t > 0)
                {
                    int div = t / 10;
                    int num = t - (10 * div);
                    numbers.Add(Copy(num));
                    t /= 10;
                }
            }
            Position = new Vector2(gameWorld.windowSize.X - numbers.Count * spriteSizeX * Scale.X, gameWorld.windowSize.Y - spriteSizeY*Scale.Y);
        }

        /// <summary>
        /// Draw function of TextImage. Same as GameOjbect in simple mode, Draws all digits in advanced mode
        /// </summary>
        /// <param name="surface">writablebitmap referecne to draw on</param>
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
                    numbers[i].Scale = Scale;
                    float offset = (numbers.Count - 1 - i) * spriteSizeX * Scale.X;
                    numbers[i].Position = new Vector2(Position.X + offset, Position.Y);
                    numbers[i].Draw(surface);
                }
            }
        }
    }
}
