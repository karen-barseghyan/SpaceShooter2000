using SpaceShoote_wpf.GameObjects;
using SpaceShoote_wpf.GameWorlds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpaceShoote_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>https://www.twitch.tv/d irectory/following
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public int height, width;
        WriteableBitmap writeableBmp;
        GameWorld world;
        List<Rectangle> InputDisplays;

        // initializing Viewport and writable bitmap
        private void Viewport_Loaded(object sender, RoutedEventArgs e)
        {
            width = (int)this.ViewPortCointainer.ActualWidth;
            height = (int)this.ViewPortCointainer.ActualHeight;

            writeableBmp = BitmapFactory.New(width, height);
            Viewport.Cursor = Cursors.None;
            Viewport.Source = writeableBmp;
            CreateWorld();
            world.StartTimer();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            InputDisplays = new List<Rectangle>();
            InputDisplays.Add(Go_Up);
            InputDisplays.Add(Go_Down);
            InputDisplays.Add(Go_Left);
            InputDisplays.Add(Go_Right);

            DebugLine.Text += "Viewport loaded\n";
        }


        // main game loop
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            foreach (Rectangle b in InputDisplays)
                b.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
            world.GameTick();

            writeableBmp.Clear(Colors.BlueViolet);
            

            foreach (GameObject o in world.gameObjects)
            {
                o.Draw(writeableBmp);
            }
        }

        // initializing game world
        private void CreateWorld()
        {
            world = new GameWorld();  

            var player = new Player(this, world);
            DebugLine.Text += "loading settings";
            //await LoadSettings(player);
            world.AddObject(player);
        }

        // Textbox used for debugging
        private void DebugLine_Loaded(object sender, RoutedEventArgs e)
        {
            DebugLine.Text += "loaded\n";
            DebugLine.ScrollToEnd();

        }

        public void DebugPlayerPos(System.Numerics.Vector2 pos)
        {
            PlayerPosX.Text = pos.X.ToString();
            PlayerPosY.Text = pos.Y.ToString();
        }

        public void LightUpInput(string name)
        {
            foreach (Rectangle b in InputDisplays)
            {
                if (b.Name == name)
                {
                    b.Fill = new SolidColorBrush(Color.FromRgb(0, 163, 255));
                }
            }
        }

        private void GoUp_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoDown_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoLeft_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoRight_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void DebugWrite(string text)
        {
            DebugLine.Text += text + "\n";
            DebugLine.ScrollToEnd();
        }
    }
}
