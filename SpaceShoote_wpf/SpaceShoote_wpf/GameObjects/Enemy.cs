using SpaceShoote_wpf.GameWorlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
    public class Enemy : Ship
    {
        public int points = 10;

        public Enemy(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            Position = new System.Numerics.Vector2(200, 100);
            spriteSizeX = 32;
            spriteSizeY = 32;
            
            spriteCycle = 0;

            slowFactor = 0.5f;
            hitboxRadius = 10;
            transitionDuration = 200;
            showHitbox = true;

            Speed = new System.Numerics.Vector2(500, 500);

            spriteSheet = BitmapFactory.FromResource("graphics/player/ship_spreadsheet_x32x32.png");
        }
    }
}
