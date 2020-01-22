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
    public class Player : Ship
    {
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
        
        bool useMouse;
        Vector2 mousePreviousPos;
        Vector2 newMousePos;
        int mouse_mode; // 0 - no mouse, 1 - mouse mode 1, 2 - mouse mode 2

        int flameSpriteCount = 2;
        float flameTransitionDuration = 100;
        TimeSpan flameTransitionTime = TimeSpan.FromMilliseconds(0);

        //player constructor with main window as parameter for reference
        public Player(MainWindow mainwindow, GameWorld world)  
        {
            gameWorld = world;
            Position = new System.Numerics.Vector2(100, 100);
            mainWindow = mainwindow;
            spriteSizeX = 32;
            spriteSizeY = 32;
            Scale.X = 1.5f;
            Scale.Y = 1.5f;
            spriteCycle = 0;

            tag = "player";
            collisionMask = new string[] { "player projectile", "player"};
            life = 1000;

            Speed = new Vector2(800, 800);
            slowFactor = 0.5f;
            mouse_mode = 1;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = false;
            boundToWindow = true;
            
            spriteSheet = BitmapFactory.FromResource("graphics/player/ship_spreadsheet_x32x32.png");

            // PROJECTILE 1
            fireRate1 = 0.1f;

            projectile = new Projectile(mainWindow, gameWorld);
            
            projectile.Velocity = new Vector2(0, -800);
            projectile.spriteSheet = BitmapFactory.FromResource("graphics/projectiles/projectile1_spreadsheet_x12x12.png");
            projectile.spriteSizeX = 12;
            projectile.spriteSizeY = 12;
            projectile.Scale = new Vector2(2, 2);
            projectile.spriteCycle = 2;
            projectile.transitionDuration = 0;
            projectile.collisionMask = new string[] { "player", "player projectile", "enemy projectile" };
            projectile.collisionDamage = 20;
            projectile.tag = "player projectile";

            // PROJECTILE 2
            fireRate2 = 1f;
            
            projectile1 = new Projectile(mainWindow, gameWorld);
            projectile1.Velocity = new Vector2(0, -600);
            projectile1.spriteSheet = BitmapFactory.FromResource("graphics/projectiles/projectile2_spreadsheet_x10x14.png");
            projectile1.spriteSizeX = 10;
            projectile1.spriteSizeY = 14;
            projectile1.Scale = new Vector2(5, 5);
            projectile1.hitboxRadius = 12;
            projectile1.spriteCount = 4;
            projectile1.collisionMask = new string[] { "player", "player projectile" }; // collides with enemy projectiles
            projectile1.tag = "player projectile";
            projectile1.collisionDamage = 500;
            projectile1.life = 100;
            projectile1.oneHit = false; // it deals damage over time

            InitializeKeyInputs();
        }
        
        private void InitializeKeyInputs()
        {
            goLeft1 = Key.A;
            goLeft2 = Key.Left;

            goRight1 = Key.D;
            goRight2 = Key.Right;

            goUp1 = Key.W;
            goUp2 = Key.Up;

            goDown1 = Key.S;
            goDown2 = Key.Down;

            shoot1 = Key.E;
            shoot2 = Key.F;
            shoot1mouse = MouseAction.LeftClick;
            shoot2mouse = MouseAction.RightClick;
            
            bomb = Key.Space;
            pause = Key.Escape;

            slow1 = Key.LeftShift;
        }
        

        //player constructor with main window for reference and starting position
        public Player(MainWindow mainwindow, float posx, float posy)
        {
            Position = new System.Numerics.Vector2(posx,posy);
            mainWindow = mainwindow;
        }

        // tick function of player, runs every frame
        public override void Tick()
        {
            float x = 0;
            float y = 0;

            //newMousePos = mainWindow.mousePos;
            newMousePos = new Vector2((float)Mouse.GetPosition(Application.Current.MainWindow).X, (float)Mouse.GetPosition(Application.Current.MainWindow).Y);

            // doesn't update cursor position if it would be outside of game window
            if (!(newMousePos.X < mainWindow.width && newMousePos.Y > 0 && newMousePos.Y < mainWindow.height) && mouse_mode == 1)
                newMousePos = mousePreviousPos;
            
            // makes game use mouse logic for movement rather than keybord if mouse movement was detected
            if (newMousePos != mousePreviousPos && mousePreviousPos != null && newMousePos.X > 0)
            {
                useMouse = true;
            }

            // movement actions
            if (Keyboard.IsKeyDown(goLeft1) || Keyboard.IsKeyDown(goLeft2))
            {
                x += -Speed.X;
                useMouse = false;
                mainWindow.LightUpInput("Go_Left");
            }
                
            if (Keyboard.IsKeyDown(goRight1) || Keyboard.IsKeyDown(goRight2))
            {
                x += Speed.X;
                useMouse = false;
                mainWindow.LightUpInput("Go_Right");
            }
               
            if (Keyboard.IsKeyDown(goUp1) || Keyboard.IsKeyDown(goUp2))
            {
                y += -Speed.Y;
                useMouse = false;
                mainWindow.LightUpInput("Go_Up");
            }
                
            if (Keyboard.IsKeyDown(goDown1) || Keyboard.IsKeyDown(goDown2))
            {
                y += Speed.Y;
                useMouse = false;
                mainWindow.LightUpInput("Go_Down");
            }

            // shoot actions

            if (Keyboard.IsKeyDown(shoot1) || Mouse.LeftButton == MouseButtonState.Pressed)
            {
                mainWindow.LightUpInput("Shoot");
                Shoot(0);
            }

            if (Keyboard.IsKeyDown(shoot2) || Mouse.RightButton == MouseButtonState.Pressed)
            {
                mainWindow.LightUpInput("Shoot2");
                Shoot(1);
            }

            // misc actions

            if (Keyboard.IsKeyDown(slow1))
            {
                mainWindow.LightUpInput("Slow");
            }
            if (Keyboard.IsKeyDown(bomb))
            {
                mainWindow.LightUpInput("Bomb");
            }

            if (Keyboard.IsKeyDown(pause))
            {
                gameWorld.Pause();
                mainWindow.PauseBt.Content = "Unpause";
            }

            // determines velocity for player depending on cursor position
            // ship will fly towards cursor even if mouse is not being moved as long it is in a diffirent position
            if (useMouse && newMousePos != null)
            {
                Vector2 target = newMousePos - Position;
                if (target.LengthSquared() > 40)
                    target = Vector2.Normalize(target) * Speed.X;
                else
                    target = Vector2.Zero;
                Velocity = target;
            } else
            {
                Velocity = new Vector2(x, y);
            }
            //saves current mouse position for use next frame
            mousePreviousPos = newMousePos;
            //debugging info
            mainWindow.DebugPlayerPos(Position);
            mainWindow.DebugPlayerV(Velocity);
            mainWindow.DebugPlayerLife((int)life);
            mainWindow.DebugPlayerScore(gameWorld.score);

            mainWindow.playerHealth.percent = life;

            base.Tick();
        }

        public override void Draw(WriteableBitmap surface)
        {
            // draws cursor
            if (newMousePos.X > 0 && newMousePos.X < mainWindow.width && newMousePos.Y > 0 && newMousePos.Y < mainWindow.height && useMouse)
                surface.FillEllipseCentered((int)newMousePos.X, (int)newMousePos.Y, 2, 2, Colors.White);

            // Flames animation

            if (gameWorld.GameTimer.ElapsedMilliseconds > flameTransitionTime.TotalMilliseconds)
            {
                flameTransitionTime = TimeSpan.FromMilliseconds(gameWorld.GameTime() + flameTransitionDuration);
                spriteCycle += 1;
                if (spriteCycle > spriteCount)
                {
                    spriteCycle = 0;
                }
            }

            if (Velocity.Y > 0)
            {
                //description below
                Rect sourceRect2 = new Rect((2 + spriteCycle) * spriteSizeX, spriteSizeY, spriteSizeX, spriteSizeY);
                Rect destRect2 = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
                surface.Blit(destRect2, spriteSheet, sourceRect2);
            }
            if (Velocity.Y <= 0)
            {
                //description below
                Rect sourceRect1 = new Rect(spriteCycle * spriteSizeX, spriteSizeY, spriteSizeX, spriteSizeY);
                Rect destRect1 = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
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
            if (animoffset == 2)
            {
                if (Velocity.X == 0)
                    TransitionTo(1, 0);
                if (Velocity.X > 0)
                    TransitionTo(3, 4);
            }
            else if (animoffset == 4)
            {
                if (Velocity.X == 0)
                    TransitionTo(3, 0);
                if (Velocity.X < 0)
                    TransitionTo(1, 2);
            }
            else if (animoffset == 1)
            {
                if (Velocity.X > 0)
                    TransitionTo(3, 4);
            }
            else if (animoffset == 3)
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
            if (gameWorld.GameTimer.ElapsedMilliseconds > transitionTime.TotalMilliseconds && animoffset != transitionTo)
            {
                animoffset = transitionTo;
            }
            // rectangle to crop from the sprite sheet
            Rect sourceRect = new Rect(animoffset * spriteSizeX, 0, spriteSizeX, spriteSizeY);
            // destination where to apply cropped image from the sprite sheet
            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
            // apply cropped image to writablebitmap
            surface.Blit(destRect, spriteSheet, sourceRect);

            if (showHitbox)
            {
                surface.FillEllipseCentered((int)Position.X, (int)Position.Y, (int)hitboxRadius, (int)hitboxRadius, Colors.Red);
            }
        }
    }
}
