using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace GameObjects
{
    public abstract class GameObject
    {
        /// <summary>
        /// List of Game Objects.
        /// </summary>
        public List<GameObject> children = new List<GameObject>();
        /// <summary>
        /// Game World in which it is generated.
        /// </summary>
        protected GameWorld gameWorld;
        /// <summary>
        /// Parent of the explosion.
        /// </summary>
        protected GameObject parent;
        /// <summary>
        /// Setting the original file that the sprite is supposed to use.
        /// </summary>
        public WriteableBitmap spriteSheet;
        /// <summary>
        /// Wideness of the sprite in original file.
        /// </summary>
        public int spriteSizeX = 32;
        /// <summary>
        /// Height of the sprite in original file.
        /// </summary>
        public int spriteSizeY = 32;
        /// <summary>
        /// Scale of the object.
        /// </summary>
        public Vector2 Scale = new Vector2(1, 1);
        /// <summary>
        /// Which frame from spritesheet it starts at.
        /// </summary>
        public int spriteCycle = 0;
        /// <summary>
        /// Amount of frames in animation.
        /// </summary>
        public int spriteCount = 1;
        /// <summary>
        /// Used if the sprite sheet has vertical sprites.
        /// </summary>
        protected int animoffset = 0;
        /// <summary>
        /// Transition speed between frames.
        /// </summary>
        public int transitionDuration = 100;
        /// <summary>
        /// Which frame to transition to
        /// </summary>
        protected int transitionTo;
        /// <summary>
        /// Time of transition
        /// </summary>
        protected TimeSpan transitionTime = TimeSpan.FromMilliseconds(0);
        /// <summary>
        /// Speed of the object.
        /// </summary>
        protected Vector2 Speed = new Vector2(0, 0);
        /// <summary>
        /// Slowing down the object.
        /// </summary>
        protected float slowFactor = 0;
        /// <summary>
        /// Setting whether the object is bound to window or not.
        /// </summary>
        protected bool boundToWindow = false;
        /// <summary>
        /// Radius of the hitbox.
        /// </summary>
        public float hitboxRadius = 10;
        /// <summary>
        /// Position of the object.
        /// </summary>
        public Vector2 Position = new Vector2(0, 0);
        /// <summary>
        /// Velocity of the object.
        /// </summary>
        public Vector2 Velocity = new Vector2(0, 0);
        /// <summary>
        /// Whether the hitbox is visible or not.
        /// </summary>
        protected bool showHitbox = true;
        /// <summary>
        /// Whether the object collides with others.
        /// </summary>
        protected bool checkCollisions = true;
        /// <summary>
        /// Tag to control which objects collide with which.
        /// </summary>
        public string tag = "enemy";
        /// <summary>
        /// Setting the collission mask.
        /// </summary>
        public string[] collisionMask = { "enemy", "enemy projectile" };
        /// <summary>
        /// Group the object belongs to.
        /// </summary>
        public int group;
        /// <summary>
        /// How much damage it deals on impact.
        /// </summary>
        public int collisionDamage = 1000;
        /// <summary>
        /// If One Hit enabled, doesn't multiply damage by deltatime.
        /// </summary>
        public bool oneHit = false;
        /// <summary>
        /// How much damage can be taken before object dies.
        /// </summary>
        public float life = 100;
        /// <summary>
        /// How much points granted on destruction.
        /// </summary>
        public int points = 10;
        /// <summary>
        /// Size of the explosion effect after destruction.
        /// </summary>
        protected float explosionSize = 2;
        /// <summary>
        /// Does the explosiond deal damage.
        /// </summary>
        protected bool explosionDamage = false;
        /// <summary>
        /// If marked for deletion then it gets removed.
        /// </summary>
        protected bool markedForDeletion = false;
        /// <summary>
        /// If true, gets removed after getting off screen.
        /// </summary>
        protected bool deleteOffscreen = true;

        /// <summary>
        /// Time since last time running this function 
        /// </summary>
        public virtual void Tick()
        {
            if (life <= 0)
                Die();
        }

        /// <summary>
        /// Draws game objects, WriteableBitmap library has been taken from here: https://github.com/reneschulte/WriteableBitmapEx
        /// </summary>
        public virtual void Draw(WriteableBitmap surface)
        {
            /// <summary>
            /// Timed animation cycle
            /// </summary>
            if (transitionDuration > 0)
            {
                
                if (gameWorld.GameTimer.ElapsedMilliseconds > transitionTime.TotalMilliseconds)
                {
                    transitionTime = TimeSpan.FromMilliseconds(gameWorld.GameTime() + transitionDuration);
                    spriteCycle += 1;
                    if (spriteCycle > spriteCount)
                    {
                        spriteCycle = 0;
                    }
                }
            }
            /// <summary>
            /// rectangle to crop from the sprite sheet
            /// </summary>

            Rect sourceRect = new Rect(spriteCycle * spriteSizeX, 0, spriteSizeX, spriteSizeY);
            /// <summary>
            /// Destination where to apply cropped image from the sprite sheet
            /// </summary>
            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
            /// <summary>
            /// Apply cropped image to writablebitmap
            /// </summary>
            surface.Blit(destRect, spriteSheet, sourceRect);

            if (showHitbox && checkCollisions)
            {
                surface.FillEllipseCentered((int)Position.X, (int)Position.Y, (int)(hitboxRadius * Scale.X), (int)(hitboxRadius * Scale.Y), Colors.Red);
            }
            foreach (GameObject o in children)
            {
                o.Draw(surface);
            }
        }

        /// <summary>
        /// changes spritesheet index after a delay. selects sprite in first parameter when called and sprite in second paramater after transitionTime time has elapsed
        /// don't use with cyclical timed animations
        /// </summary>

        protected void TransitionTo(int anim1, int anim2)
        {
            animoffset = anim1;
            transitionTo = anim2;
            transitionTime = TimeSpan.FromMilliseconds((int)gameWorld.GameTimer.ElapsedMilliseconds + transitionDuration);
        }

        /// <summary>
        /// Applies velocity to position, checks boundaries and collisions
        /// </summary>
        public void Move()
        {
            float deltatime = gameWorld.deltatime;
            Position += Velocity * deltatime / 1000f;
            
            foreach (GameObject o in children)
            {
                o.Position += Velocity * deltatime / 1000f;
            }
            
            if (checkCollisions)
            {
                foreach (GameObject o in gameWorld.gameObjects)
                {
                    if (o != this && o.checkCollisions)
                    {
                        if (CheckCollision(o.Position, o.hitboxRadius * o.Scale.X, o.tag))
                        {
                            if (oneHit)
                            {
                                life = 0;
                                o.life -= collisionDamage;
                            } else
                            {
                                if (o.oneHit)
                                {
                                    life -= o.collisionDamage;
                                    o.life = 0;
                                }
                                else
                                    life -= o.collisionDamage * gameWorld.deltatime / 1000;
                            }
                        }
                    }
                }
                    
            }

            if (boundToWindow)
            {
                if (Position.X - hitboxRadius < 0)
                {
                    Position = new Vector2(hitboxRadius, Position.Y);
                    Velocity = new Vector2(0, Velocity.Y);
                }
                else if (Position.X + hitboxRadius > gameWorld.windowSize.X)
                {
                    Position = new Vector2(gameWorld.windowSize.X - hitboxRadius, Position.Y);
                    Velocity = new Vector2(0, Velocity.Y);
                }
                if (Position.Y - hitboxRadius < 0)
                {
                    Position = new Vector2(Position.X, hitboxRadius);
                    Velocity = new Vector2(Velocity.X, 0);
                }
                if (Position.Y + hitboxRadius > gameWorld.windowSize.Y)
                {
                    Position = new Vector2(Position.X, gameWorld.windowSize.Y - hitboxRadius);
                    Velocity = new Vector2(Velocity.X, 0);
                }
            }

            /// <summary>
            /// Delete the object if it goes too far offscreen
            /// </summary>
            if ((Position.X > gameWorld.windowSize.X + 100 || Position.X < -100 || Position.Y > gameWorld.windowSize.Y + 100 || Position.Y < -100) && deleteOffscreen)
                markedForDeletion = true;
        }

        /// <summary>
        /// Checks collision between objects.
        /// </summary>
        /// <param name="centre">Centre of the object.</param>
        /// <param name="radius">Radius of the gameobject.</param>
        /// <param name="tagMask">Tag Mask of the object.</param>
        public bool CheckCollision(Vector2 centre, float radius, string tagMask)
        {
            foreach (string s in collisionMask)
            {
                if (tagMask == s)
                    return false;
            }

            if (Vector2.Distance(centre, Position) <= hitboxRadius + radius)
            {
                return true;
            }
            foreach (GameObject o in children)
                CheckCollision(centre, radius, tagMask);
            return false;
        }

        /// <summary>
        /// Spawns explosion if destroyed.
        /// </summary>
        protected virtual void Die()
        {
            markedForDeletion = true;
            gameWorld.score += points;                      
            Explosion copy = gameWorld.explosionPrefab.Copy(Position, explosionSize, explosionDamage);
            gameWorld.gameObjects.Add(copy);
        }

        /// <summary>
        /// Removes the object.
        /// </summary>
        public bool CleanUp()
        {
            if (!markedForDeletion)
                return false;
            gameWorld.gameObjects.Remove(this);
            return true;
        }
    }
}