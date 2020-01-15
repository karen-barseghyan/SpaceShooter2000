using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
    public abstract class GameObject
    {
        // Position of given game object in the game world
        public Vector2 Position { get; set; }

        // velocity of given game object
        public Vector2 Velocity { get; set; }

        // time since last time running this function 
        public virtual void Tick(float deltatime)
        {

        }

        // Draws game objects (...)
        public virtual void Draw(WriteableBitmap surface, float deltatime)
        {

        }
    }
}
