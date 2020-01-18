using SpaceShoote_wpf.GameWorlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
    //MegaBeetle
    public class PlayerShot : Ship
    {
        private Ship ship;
        private GameWorld world;

        public PlayerShot(MainWindow mainwindow, GameWorld world)
        {
            mainWindow = mainwindow;
            gameWorld = world;
            Position = new System.Numerics.Vector2(100, 100);
            spriteSizeX = 10;
            spriteSizeY = 14;

            spriteCycle = 1;

            slowFactor = 0.5f;
            hitboxRadius = 5;
            transitionDuration = 200;
            showHitbox = true;

            Speed = new System.Numerics.Vector2(500, 500);

            spriteSheet = BitmapFactory.FromResource("graphics/projectiles/projectile2_spreadsheet_x10x14.png");

        }

        public PlayerShot(Ship ship, GameWorld world)
        {
            this.ship = ship;
            this.world = world;
        }
    }

}
