using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GameObjects
{
    /// <summary>
    /// Explosion that happens after bullet impact or death of an enemy.
    /// </summary>
    public class Explosion : GameObject
    {
        /// <summary>
        /// Explosion Constructor with Game World as parameter for reference.
        /// </summary>
        /// <param name="world">World that the object is added to. </param>
        public Explosion(GameWorld world)
        {
            gameWorld = world;
            spriteSizeX = 30;
            spriteSizeY = 30;
            Velocity = Vector2.Zero;
            collisionDamage = 1000;
            hitboxRadius = 20;
            life = 100000;
            spriteCount = 7;
            transitionDuration = 100;

            spriteSheet = BitmapFactory.FromResource("graphics/projectiles/explosion_spreadsheet_x30x30.png");
        }

        /// <summary>
        /// Variation from Draw funciton of GameObject
        /// </summary>
        public override void Draw(WriteableBitmap surface)
        {
            if (gameWorld.GameTimer.ElapsedMilliseconds > transitionTime.TotalMilliseconds)
            {
                transitionTime = TimeSpan.FromMilliseconds(gameWorld.GameTime() + transitionDuration);
                spriteCycle += 1;
                if (spriteCycle + 2 > spriteCount)
                {
                    markedForDeletion = true;
                }
            }

            Rect sourceRect = new Rect(spriteCycle * spriteSizeX, 0, spriteSizeX, spriteSizeY);
            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);

            surface.Blit(destRect, spriteSheet, sourceRect);

            if (showHitbox && checkCollisions)
            {
                surface.FillEllipseCentered((int)Position.X, (int)Position.Y, (int)(hitboxRadius * Scale.X), (int)(hitboxRadius * Scale.Y), Colors.Red);
            }
        }

        /// <summary>
        /// Copies the drawn explosion to increase performance instead of making a new object.
        /// </summary>
        /// <param name="position">Position of the object</param>
        /// <param name="size">Size of the object. </param>
        /// <param name="damaging">Whether the object can damage objects with life or not. </param>
        public Explosion Copy(Vector2 position, float size, bool damaging)
        {
            
            Explosion p = new Explosion(gameWorld);
            p.gameWorld = gameWorld;
            p.parent = parent;
            p.spriteSheet = spriteSheet;
            p.spriteSizeX = spriteSizeX;
            p.spriteSizeY = spriteSizeY;
            
            p.spriteCount = spriteCount;
            p.spriteCycle = 0;
            p.animoffset = 0;
            p.transitionDuration = transitionDuration + (int)(size * 10);
            p.slowFactor = slowFactor;
            p.boundToWindow = boundToWindow;
            p.hitboxRadius = hitboxRadius;

            p.Position = position;

            p.Scale = Vector2.One * size;

            p.Velocity = Velocity;
            p.showHitbox = showHitbox;
            p.checkCollisions = damaging;
            p.tag = tag;
            p.collisionMask = collisionMask;
            p.collisionDamage = collisionDamage;
            p.oneHit = oneHit;
            p.life = life;
            return p;
        }
    }
}
