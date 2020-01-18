using SpaceShoote_wpf.GameWorlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
    public class Ship : GameObject
    {
        public bool canShoot = true;
        public float fireRate = 0.5f;
        private TimeSpan canShootNext;
        public List<SpawnData> Projectiles;

        public override void Tick()
        {
            base.Tick();
            if (!canShoot)
            {
                if (gameWorld.GameTimer.Elapsed > canShootNext)
                    canShoot = true;
            }
        }

        public virtual void Shoot(int type)
        {
            
            if (canShoot)
            {
                canShoot = false;
                mainWindow.DebugWrite("pew");
              //  PlayerShot playshot = new PlayerShot(mainWindow, gameWorld);
             //   Projectiles.Add(new SpawnData(playshot, 100));
                canShootNext = TimeSpan.FromMilliseconds(gameWorld.GameTime() + fireRate*1000);
            }
        }
    }
}
