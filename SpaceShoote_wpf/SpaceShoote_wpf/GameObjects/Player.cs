using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
    class Player : GameObject
    {
        //main window reference
        // used for debugging
        MainWindow mainWindow;

        // sprite variables
        WriteableBitmap spriteSheet;
        int spriteSizeX;
        int spriteSizeY;

        //input variables
        public List<Input> inputs;
        
        Key goLeft1;
        Key goLeft2;
        Key goRight1;
        Key goRight2;
        Key goUp1;
        Key goUp2;
        Key goDown1;
        Key goDown2;
        Key shoot1;
        Key shoot2;
        Key bomb;
        Key pause;
        Key slow1;
        MouseAction shoot1mouse;
        MouseAction shoot2mouse;
        bool disableMouse;
        
        bool useMouse;
        

        float speed;

        //player constructor with main window as parameter for reference
        public Player(MainWindow mainwindow)  
        {
            Position = new System.Numerics.Vector2(100, 100);
            mainWindow = mainwindow;
            spriteSizeX = 64;
            spriteSizeY = 64;

            speed = 100;
            inputs = new List<Input>();
            InitializeKeyInputs();

            spriteSheet = BitmapFactory.FromResource("graphics/player/ship.png");
        }
        
        private void InitializeKeyInputs()
        {
            goLeft1 = Key.A;
            goLeft2 = Key.Left;
            inputs.Add(new Input("Go Left", goLeft1, goLeft2));

            goRight1 = Key.A;
            goRight2 = Key.Right;
            inputs.Add(new Input("Go Right", goRight1, goRight2));

            goUp1 = Key.W;
            goUp2 = Key.Up;
            inputs.Add(new Input("Go Up", goUp1, goUp2));

            goDown1 = Key.S;
            goDown2 = Key.Down;
            inputs.Add(new Input("Go Down", goDown1, goDown2));

            shoot1 = Key.Space;
            shoot2 = Key.E;
            shoot1mouse = MouseAction.LeftClick;
            inputs.Add(new Input("Shoot", shoot1, shoot2, shoot1mouse));

            shoot2mouse = MouseAction.RightClick;
            inputs.Add(new Input("Alt Shoot", shoot2mouse));
            

            bomb = Key.B;
            inputs.Add(new Input("Bomb", bomb));

            pause = Key.Escape;
            inputs.Add(new Input("Pause", pause));

            slow1 = Key.LeftShift;
            inputs.Add(new Input("Slow", slow1));

            disableMouse = false;
        }
        

        //player constructor with main window for reference and starting position
        public Player(MainWindow mainwindow, float posx, float posy)
        {
            Position = new System.Numerics.Vector2(posx,posy);
            mainWindow = mainwindow;
        }

        // tick function of player, runs every frame
        public override void Tick(float deltatime)
        {
            Position = Position += Velocity * deltatime / 1000f;
            //Position = 
            //mainWindow.PointToScreen(Mouse.GetPosition(mainWindow.Viewport));
            Position = new System.Numerics.Vector2((float)Mouse.GetPosition(Application.Current.MainWindow).X, (float)Mouse.GetPosition(Application.Current.MainWindow).Y) ;


            mainWindow.DebugPlayerPos(Position);
        }

        //draw function of player
        public override void Draw(WriteableBitmap surface)
        {
            Rect sourceRect = new Rect(0, 0, spriteSizeX, spriteSizeY);
            Rect destRect = new Rect((int)Position.X - spriteSizeX/2, (int)Position.Y-spriteSizeY/2, spriteSizeX, spriteSizeY);
            surface.Blit(destRect, spriteSheet, sourceRect);
        }
    }
}
