using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Media.Imaging;
using SpaceShoote_wpf.GameWorlds;
using System.Windows;
using System.Windows.Media;

namespace SpaceShoote_wpf.GameObjects
{
    public abstract class GameObject
    {
        public List<GameObject> children = new List<GameObject>();

        // parent reference
        protected GameWorld gameWorld;
        protected GameObject parent;
        protected MainWindow mainWindow;
        // sprite variables
        public WriteableBitmap spriteSheet;
        public int spriteSizeX = 32;
        public int spriteSizeY = 32;
        public Vector2 Scale = new Vector2(1, 1);
        protected int spriteCycle = 0;
        public int spriteCount = 1;
        protected int animoffset = 0;
        protected int transitionDuration = 100;
        protected int transitionTo;
        protected TimeSpan transitionTime = TimeSpan.FromMilliseconds(0);
        //movement controll variables
        protected Vector2 Speed = new Vector2(0, 0);
        protected float slowFactor = 0;
        protected bool boundToWindow = false;
        //hitbox / other variables
        public float hitboxRadius = 10;
        public Vector2 Position = new Vector2(0, 0);
        public Vector2 Velocity = new Vector2(0, 0);
        protected bool showHitbox = true;
        protected bool checkCollisions = true;
        //public bool takeDamageFromCollision = true;
        public string tag = "enemy";
        public string[] collisionMask = { "enemy", "enemy projectile" }; // by tag
        public int group;
        public int collisionDamage = 500;
        public bool oneHit = false; //if oneHit, doesn't multiply damage by deltatime
        public float life = 100;
        public int points = 10;

        protected float explosionSize = 2;
        protected bool explosionDamage = false;

        protected bool markedForDeletion = false;
        protected bool deleteOffscreen = true;

        // time since last time running this function 
        public virtual void Tick()
        {
            if (life <= 0)
                Die();
        }

        // Draws game objects (...)
        public virtual void Draw(WriteableBitmap surface)
        {
            // timed animation cycle
            if (gameWorld.GameTimer.ElapsedMilliseconds > transitionTime.TotalMilliseconds)
            {
                transitionTime = TimeSpan.FromMilliseconds(gameWorld.GameTime() + transitionDuration);
                spriteCycle += 1;
                if (spriteCycle > spriteCount)
                {
                    spriteCycle = 0;
                }
            }


            // rectangle to crop from the sprite sheet
            Rect sourceRect = new Rect(spriteCycle * spriteSizeX, 0, spriteSizeX, spriteSizeY);
            // destination where to apply cropped image from the sprite sheet
            Rect destRect = new Rect((int)Position.X - spriteSizeX * Scale.X / 2, (int)Position.Y - spriteSizeY * Scale.Y / 2, spriteSizeX * Scale.X, spriteSizeY * Scale.Y);
            // apply cropped image to writablebitmap
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

        //changes spritesheet index after a delay. selects sprite in first parameter when called and sprite in second paramater after transitionTime time has elapsed
        // don't use with cyclical timed animations
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
                else if (Position.X + hitboxRadius > mainWindow.width)
                {
                    Position = new Vector2(mainWindow.width - hitboxRadius, Position.Y);
                    Velocity = new Vector2(0, Velocity.Y);
                }
                if (Position.Y - hitboxRadius < 0)
                {
                    Position = new Vector2(Position.X, hitboxRadius);
                    Velocity = new Vector2(Velocity.X, 0);
                }
                if (Position.Y + hitboxRadius > mainWindow.height)
                {
                    Position = new Vector2(Position.X, mainWindow.height - hitboxRadius);
                    Velocity = new Vector2(Velocity.X, 0);
                }
            }

            // delete the object if it goes too far offscreen
            if ((Position.X > mainWindow.width + 100 || Position.X < -100 || Position.Y > mainWindow.height + 100 || Position.Y < -100) && deleteOffscreen)
                markedForDeletion = true;
        }

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

        protected virtual void Die()
        {
            markedForDeletion = true;
            gameWorld.score += points;
            //explosion
            
            Explosion copy = gameWorld.explosionPrefab.Copy(Position, explosionSize, explosionDamage);
            gameWorld.gameObjects.Add(copy);
        }

        public bool CleanUp()
        {
            if (!markedForDeletion)
                return false;
            gameWorld.gameObjects.Remove(this);
            return true;
        }
    }
}