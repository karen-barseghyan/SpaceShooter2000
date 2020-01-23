using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameObjects
{
    /// <summary>
    /// Creating projectile that ships shoot.
    /// </summary>
    public class Projectile : GameObject
    {
        /// <summary>
        /// Preparing a projectile.
        /// </summary>
        /// <param name="world">World that the object is added to. </param>
        public Projectile(GameWorld world)
        {
            gameWorld = world;
            points = 0;
            explosionSize = 1;
            spriteSizeX = 12;
            spriteSizeY = 12;
            spriteCycle = 1;
            hitboxRadius = 10;
            transitionDuration = 50;
            showHitbox = false;
            oneHit = true;
            collisionMask = new string[] { "enemy", "enemy projectile" };
            Velocity = new Vector2(0, 800);
            spriteSheet = BitmapFactory.FromResource("graphics/projectiles/projectile5_spreadsheet_x11x11.png");
        }

        /// <summary>
        /// Copying projectile to not put a heavy load on the PC.
        /// </summary>
        /// <param name="position">Position of the projectile spawned</param>
        public Projectile Copy(Vector2 position)
        {
            Projectile p = new Projectile(gameWorld);
            p.gameWorld = gameWorld;
            p.parent = parent;
            p.spriteSheet = spriteSheet;
            p.spriteSizeX = spriteSizeX;
            p.spriteSizeY = spriteSizeY;
            p.Scale = Scale;
            p.spriteCount = spriteCount;
            p.spriteCycle = spriteCycle;
            p.animoffset = 0;
            p.transitionDuration = transitionDuration;
            p.slowFactor = slowFactor;
            p.boundToWindow = boundToWindow;
            p.hitboxRadius = hitboxRadius;

            p.Position = position;

            p.Velocity = Velocity;
            p.showHitbox = showHitbox;
            p.checkCollisions = checkCollisions;
            p.tag = tag;
            p.collisionMask = collisionMask;
            p.collisionDamage = collisionDamage;
            p.oneHit = oneHit; 
            p.life = life;
            return p;
        }
    }
}
