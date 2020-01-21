﻿using System;
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
        public GameWorld gameWorld;
        public GameObject parent;
        public MainWindow mainWindow;
        // sprite variables
        public WriteableBitmap spriteSheet;
        public int spriteSizeX = 32;
        public int spriteSizeY = 32;
        public Vector2 Scale = new Vector2(1, 1);
        public float spriteCycle = 0;
        public int animoffset = 0;
        public int transitionDuration = 0;
        public int transitionTo;
        public TimeSpan transitionTime;
        //movement controll variables
        public Vector2 Speed = new Vector2(0, 0);
        public float slowFactor = 0;
        public bool boundToWindow = false;
        public bool deleteOffcreen = true;
        //hitbox / other variables
        public float hitboxRadius = 10;
        public Vector2 Position = new Vector2(0, 0);
        public Vector2 Velocity = new Vector2(0, 0);
        public bool showHitbox = true;
        public bool checkCollisions = true;
        //public bool takeDamageFromCollision = true;
        public string tag = "enemy";
        public string[] collisionMask = { "enemy", "enemy projectile" }; // by tag
        public int group;
        public int collisionDamage = 100;
        public bool oneHit = false; //if oneHit, doesn't multiply damage by deltatime
        public float life = 100;
        public int points = 10;
        private bool markedForDeletion = false;

        // time since last time running this function 
        public virtual void Tick()
        {
            if (life <= 0)
                Die();
        }

        // Draws game objects (...)
        public virtual void Draw(WriteableBitmap surface)
        {
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
        public void TransitionTo(int anim1, int anim2)
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
                                    o.life = 0;
                                    life -= o.collisionDamage;
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
            if ((Position.X > mainWindow.width + 100 || Position.X < -100 || Position.Y > mainWindow.height + 100 || Position.Y < -100) && deleteOffcreen)
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

        public virtual void Die()
        {
            markedForDeletion = true;
            gameWorld.score += points;
            //explosion
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