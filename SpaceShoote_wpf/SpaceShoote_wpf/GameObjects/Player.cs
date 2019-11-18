using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
    class Player : GameObject
    {
        MainWindow mainWindow;

        public Player(MainWindow mainwindow)  
        {
            Position = new System.Numerics.Vector2(100, 100);
            mainWindow = mainwindow;
        }

        public Player(float posx, float posy)
        {
            Position = new System.Numerics.Vector2(posx,posy);
        }

        public override void Tick(float deltatime)
        {
            //Position = Position += Velocity * deltatime / 1000f;
            //Position = 
            //mainWindow.PointToScreen(Mouse.GetPosition(mainWindow.Viewport));
            Position = new System.Numerics.Vector2((float)Mouse.GetPosition(Application.Current.MainWindow).X-32, (float)Mouse.GetPosition(Application.Current.MainWindow).Y-32) ;


            mainWindow.DebugWrite(Position.ToString());
        }

        public override void Draw(WriteableBitmap surface)
        {


            WriteableBitmap wr1 = BitmapFactory.FromResource("graphics/player/ship.png");
            Rect sourceRect = new Rect(0, 0, 64, 64);
            Rect destRect = new Rect((int)Position.X, (int)Position.Y, 64, 64);
            surface.Blit(destRect, wr1, sourceRect);
        }
    }
}
