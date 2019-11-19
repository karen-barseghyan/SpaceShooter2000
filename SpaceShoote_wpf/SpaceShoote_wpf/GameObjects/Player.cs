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
    class Player : GameObject
    {
        //main window reference
        // used for debugging
        MainWindow mainWindow;

        // gameworld reference
        GameWorld gameWorld;
        // sprite variables
        WriteableBitmap spriteSheet;
        int spriteSizeX;
        int spriteSizeY;
        int spriteCycle;
        int tiltoffset;
        int transitionDuration;
        int transitionTo;
        TimeSpan transitionTime;
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
        Vector2 mousePreviousPos;
        Vector2 newMousePos;

        float verticalSpeed { get; set; }
        float horizontalSpeed { get; set; }

        float hitboxRadius { get; set; }

        //player constructor with main window as parameter for reference
        public Player(MainWindow mainwindow, GameWorld world)  
        {
            gameWorld = world;
            Position = new System.Numerics.Vector2(100, 100);
            mainWindow = mainwindow;
            spriteSizeX = 64;
            spriteSizeY = 64;
            spriteCycle = 0;

            verticalSpeed = 400;
            horizontalSpeed = 400;

            hitboxRadius = 20;
            transitionDuration = 200;
            inputs = new List<Input>();
            InitializeKeyInputs();
            
            spriteSheet = BitmapFactory.FromResource("graphics/player/ship.png");
        }
        
        private void InitializeKeyInputs()
        {
            goLeft1 = Key.A;
            goLeft2 = Key.Left;
            inputs.Add(new Input("Go Left", goLeft1, goLeft2));

            goRight1 = Key.D;
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
            float x = 0;
            float y = 0;
            newMousePos = new Vector2((float)Mouse.GetPosition(Application.Current.MainWindow).X, 
                (float)Mouse.GetPosition(Application.Current.MainWindow).Y);
            if (!(newMousePos.X < mainWindow.width && newMousePos.Y > 0 && newMousePos.Y < mainWindow.height))
                newMousePos = mousePreviousPos;
            if (newMousePos != mousePreviousPos && mousePreviousPos != null && newMousePos.X > 0)
            {
                useMouse = true;
            }

            if (Keyboard.IsKeyDown(goLeft1))
            {
                x += -horizontalSpeed;
                useMouse = false;
                mainWindow.LightUpInput("Go_Left");
            }
                
            if (Keyboard.IsKeyDown(goRight1))
            {
                x += horizontalSpeed;
                useMouse = false;
                mainWindow.LightUpInput("Go_Right");
            }
               
            if (Keyboard.IsKeyDown(goUp1))
            {
                y += -verticalSpeed;
                useMouse = false;
                mainWindow.LightUpInput("Go_Up");
            }
                
            if (Keyboard.IsKeyDown(goDown1))
            {
                y += verticalSpeed;
                useMouse = false;
                mainWindow.LightUpInput("Go_Down");
            }

            if (useMouse)
            {
                Vector2 target = newMousePos - Position;
                if (target.LengthSquared() > 100)
                    target = Vector2.Normalize(target) * horizontalSpeed;
                else
                    target = Vector2.Zero;

                Velocity = target;
            } else
            {
                Velocity = new Vector2(x, y);
            }
            Position = Position += Velocity * deltatime / 1000f;

            mousePreviousPos = newMousePos;
            mainWindow.DebugPlayerPos(Velocity);
        }

        //draw function of player
        public override void Draw(WriteableBitmap surface)
        {
            // Flames animation
            int spriteoffset = 0;
            if (spriteCycle > 3)
            {
                spriteoffset = 1;
            }
            if (Velocity.Y > 10)
            {
                Rect sourceRect2 = new Rect((2 + spriteoffset) * spriteSizeX, spriteSizeY, spriteSizeX, spriteSizeY);
                Rect destRect2 = new Rect((int)Position.X - spriteSizeX / 2, (int)Position.Y - spriteSizeY / 2, spriteSizeX, spriteSizeY);
                surface.Blit(destRect2, spriteSheet, sourceRect2);
            }
            if (Velocity.Y <= 10)
            {
                Rect sourceRect1 = new Rect(spriteoffset*spriteSizeX, spriteSizeY, spriteSizeX, spriteSizeY);
                Rect destRect1 = new Rect((int)Position.X - spriteSizeX / 2, (int)Position.Y - spriteSizeY / 2, spriteSizeX, spriteSizeY);
                surface.Blit(destRect1, spriteSheet, sourceRect1);
            }

            // Sprite selection from spritesheet logic
            // depending on current state and speed, the correct transition will be called
            // Neutral -> left: 0 -> -1 -> -2 | 0 -> 1 -> 2
            // Neutral -> right: 0 -> 1 -> 2 | 0 -> 3 -> 4
            // Right -> Neutral: 2 -> 1 -> 0 | 4 -> 3 -> 0
            // Left -> Neutral: -2 -> -1 -> 0 | 2 -> 1 -> 0
            // Left -> Right: -2 -> 1 -> 2 | 2 -> 3 -> 4
            // Right -> Left: 2 -> -1 -> -2 | 4 -> 2 -> 1
            if (tiltoffset == 2)
            {
                if (Velocity.X == 0)
                    TransitionTo(1, 0);
                if (Velocity.X > 0)
                    TransitionTo(3, 4);
            }
            else if (tiltoffset == 4)
            {
                if (Velocity.X == 0)
                    TransitionTo(3, 0);
                if (Velocity.X < 0)
                    TransitionTo(1, 2);
            } else if (tiltoffset == 1)
            {
                if (Velocity.X > 0)
                    TransitionTo(3, 4);
            }
            else if (tiltoffset == 3)
            {
                if (Velocity.X < 0)
                    TransitionTo(1, 2);
            }
            else
            {
                if (Velocity.X > 0)
                    TransitionTo(3, 4);
                else if (Velocity.X < 0)
                    TransitionTo(1, 2);
            }
            //checks if transitionTime has elapsed and sets tiltoffset to transitionTo
            if (gameWorld.GameTimer.ElapsedMilliseconds > transitionTime.TotalMilliseconds && tiltoffset != transitionTo)
            {
                tiltoffset = transitionTo;
            }

            Rect sourceRect = new Rect(tiltoffset*spriteSizeX, 0, spriteSizeX, spriteSizeY);
            Rect destRect = new Rect((int)Position.X - spriteSizeX/2, (int)Position.Y-spriteSizeY/2, spriteSizeX, spriteSizeY);
            surface.Blit(destRect, spriteSheet, sourceRect);
            if (newMousePos.X > 0 && newMousePos.X < mainWindow.width && newMousePos.Y > 0 && newMousePos.Y < mainWindow.height)
                surface.DrawEllipseCentered((int)newMousePos.X, (int)newMousePos.Y, 8, 8, Colors.Red);
            spriteCycle++;
            if (spriteCycle > 7)
                spriteCycle = 0;
        }
        //transition function. selects sprite in first parameter when called and sprite in second paramater after transitionTime time has elapsed
        private void TransitionTo(int tilt1, int tilt2)
        {
            tiltoffset = tilt1;
            transitionTo = tilt2;
            transitionTime = TimeSpan.FromMilliseconds((int)gameWorld.GameTimer.ElapsedMilliseconds + transitionDuration);
        }

    }
}
