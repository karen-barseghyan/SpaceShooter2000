using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SpaceShoote_wpf.GameObjects
{
    public class Ball : GameObject
    {
        public float radius { get; set; }

        public override void Draw(WriteableBitmap surface)
        {
            surface.FillEllipse((int)Position.X, (int)Position.Y, (int)(Position.X + radius), (int)(Position.Y + radius), Colors.Blue);
        }
    }
}
