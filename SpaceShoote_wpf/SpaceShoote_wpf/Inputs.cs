using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaceShoote_wpf
{
    class Input
    {
        string name {get; set;}
        Key key { get; set; }
        Key alt { get; set; }

        MouseAction mouse { get; set; }

        public Input(string _name, Key _key)
        {
            name = _name;
            key = _key;
        }

        public Input(string _name, Key _key, Key _alt)
        {
            name = _name;
            key = _key;
            alt = _alt;
        }
        public Input(string _name, MouseAction _mouse)
        {
            name = _name;
            mouse = _mouse;
        }
        public Input(string _name, Key _key, Key _alt, MouseAction _mouse)
        {
            name = _name;
            key = _key;
            alt = _alt;
            mouse = _mouse;
        }

        public override string ToString()
        {
            string r = name + ": ";
            if (key != Key.None)
                r += key + " ";
            if (alt != Key.None)
                r += alt + " ";
            if (mouse != MouseAction.None)
                r += mouse + " ";
            return r;
        }
    }
}
