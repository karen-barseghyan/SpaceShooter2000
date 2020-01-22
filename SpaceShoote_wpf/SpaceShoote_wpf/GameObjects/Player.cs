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


            // determines velocity for player depending on cursor position
            // ship will fly towards cursor even if mouse is not being moved as long it is in a diffirent position
            
            if (mainWindow.inputs.useMouse && mainWindow.inputs.newMousePos != null)
            {
                Vector2 target = mainWindow.inputs.newMousePos - Position;
                if (target.LengthSquared() > 40)
                    target = Vector2.Normalize(target) * Speed.X;
                else
                    target = Vector2.Zero;
                Velocity = target;
            } else if (mainWindow.inputs.move != Vector2.Zero)
            {
                Velocity = Vector2.Normalize(mainWindow.inputs.move) * Speed;
                if (mainWindow.inputs.slow)
                {
                    Velocity *= slowFactor;
                    //showHitbox = true;
                } //else
                    //showHitbox = false;
            } else
            {
                Velocity = Vector2.Zero;
            }
            


            if (mainWindow.inputs.shoot1)
                Shoot(0);
            if (mainWindow.inputs.shoot2)
                Shoot(1);

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
            if (mainWindow.inputs.newMousePos.X > 0 && mainWindow.inputs.newMousePos.X < mainWindow.width && mainWindow.inputs.newMousePos.Y > 0 && mainWindow.inputs.newMousePos.Y < mainWindow.height && mainWindow.inputs.useMouse)
                surface.FillEllipseCentered((int)mainWindow.inputs.newMousePos.X, (int)mainWindow.inputs.newMousePos.Y, 2, 2, Colors.White);

            // Flames animation

            if (gameWorld.GameTimer.ElapsedMilliseconds > flameTransitionTime.TotalMilliseconds)
            {
                flameTransitionTime = TimeSpan.FromMilliseconds(gameWorld.GameTime() + flameTransitionDuration);
                spriteCycle += 1;
                if (spriteCycle > flameSpriteCount)
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

        protected override void Die()
        {
            base.Die();
            mainWindow.gameState = 4;
            mainWindow.showGameOver = TimeSpan.FromMilliseconds(gameWorld.GameTime() + 1000);
        }
    }
}
