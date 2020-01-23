using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameObjects
{
    /// <summary>
    /// Responsible for either shooting primary or secondary shot.
    /// </summary>
    public class Ship : GameObject
    {
        /// <summary>
        /// Checks if you can shoot first projectile.
        /// </summary>
        public bool canShoot1 = true;
        /// <summary>
        /// Checks if you can shoot second projectile.
        /// </summary>
        public bool canShoot2 = true;
        /// <summary>
        /// Sets fire rate of the first projectile.
        /// </summary>
        public float fireRate1 = 0.1f;
        /// <summary>
        /// Sets fire rate of the second projectile.
        /// </summary>
        public float fireRate2 = 0.1f;
        /// <summary>
        /// Controls time before you can shoot the first projectile again.
        /// </summary>
        public TimeSpan canShootNext1;
        /// <summary>
        /// Controls time before you can shoot the second projectile again.
        /// </summary>
        public TimeSpan canShootNext2;

        /// <summary>
        /// Projectile 1 reference
        /// </summary>
        public Projectile projectile;
        /// <summary>
        /// Projectile 2 reference
        /// </summary>
        public Projectile projectile1;

        /// <summary>
        /// Adds a check if the player can shoot a projectiles, to every Tick function of game object for ships
        /// </summary> 
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

        /// <summary>
        /// Shoots a projectile depending on type.
        /// </summary>
        /// <param name="type">Which projectile to shoot.</param>
        public virtual void Shoot(int type)
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
