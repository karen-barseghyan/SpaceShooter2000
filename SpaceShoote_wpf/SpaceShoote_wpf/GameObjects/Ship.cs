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
        public bool canShoot1 = true;
        public bool canShoot2 = true;
        public float fireRate1 = 0.1f;
        public float fireRate2 = 0.1f;
        public TimeSpan canShootNext1;
        public TimeSpan canShootNext2;

        public Projectile projectile;
        public Projectile projectile1;


        public override void Tick()
        {
            base.Tick();
            if (!canShoot1)
            {
                if (gameWorld.GameTimer.Elapsed > canShootNext1)
                    canShoot1 = true;
            }
            if (!canShoot2)
            {
                if (gameWorld.GameTimer.Elapsed > canShootNext2)
                    canShoot2 = true;
            }
        }

        public void Shoot(int type)
        {
            switch (type)
            {
                case 0:
                    if (canShoot1)
                    {
                        canShoot1 = false;
                        canShootNext1 = TimeSpan.FromMilliseconds(gameWorld.GameTime() + fireRate1 * 1000);
                        Projectile copy = projectile.Copy(Position);
                        gameWorld.gameObjects.Add(copy);
                    }
                    break;
                default:
                    if (canShoot2)
                    {
                        canShoot2 = false;
                        canShootNext2 = TimeSpan.FromMilliseconds(gameWorld.GameTime() + fireRate2 * 1000);
                        Projectile copy = projectile1.Copy(Position);
                        gameWorld.gameObjects.Add(copy);
                    }
                    break;
            }

        }
    }
}
