using SpaceShoote_wpf.GameWorlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
    class Ship : GameObject
    {
        public MainWindow mainWindow;

        // gameworld reference
        public GameWorld gameWorld;
        // sprite variables
        public WriteableBitmap spriteSheet;
        public int spriteSizeX;
        public int spriteSizeY;
        public int spriteCycle;
        public int tiltoffset;
        public int transitionDuration;
        public int transitionTo;
        public TimeSpan transitionTime;
        //input variables
        public bool showHitbox;

        public float verticalSpeed { get; set; }
        public float horizontalSpeed { get; set; }

        public float hitboxRadius { get; set; }

        public virtual void Shoot(int type)
        {

        }
    }
}
