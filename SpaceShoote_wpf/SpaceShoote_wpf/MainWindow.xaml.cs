using SpaceShoote_wpf.GameObjects;
using SpaceShoote_wpf.GameWorlds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
        public Player player;

        /// initializing Viewport and writable bitmap
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
            InputDisplays.Add(Shoot);
            InputDisplays.Add(Shoot2);
            InputDisplays.Add(Slow);
            InputDisplays.Add(Bomb);

            DebugLine.Text += "Viewport loaded\n";
        }
        

        /// main game loop
        /// calls every frame (same as monitor refresh rate)
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            foreach (Rectangle b in InputDisplays)
                b.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));

            //await Task.Run(new Action(world.GameTick));
            world.GameTick();
            writeableBmp.Clear(Colors.Black);

            foreach (GameObject o in world.gameObjects)
            {
                o.Draw(writeableBmp);
            }
            Object_Counter.Text = world.gameObjects.Count.ToString();
            FPS_Counter.Text = world.GetFPS().ToString();
        }
        
        /// initializing game world
        private void CreateWorld()
        {
            world = new GameWorld(this);  

            player = new Player(this, world);
            var backgroundlayer1 = new BackgroundLayer1(this, world);
            var backgroundlayer2 = new BackgroundLayer2(this, world);
            var health = new Health(this, world);
            DebugLine.Text += "loading settings";
            //await LoadSettings(player);
            world.AddObject(backgroundlayer1);
            world.AddObject(backgroundlayer2);
            world.AddObject(player);
            world.AddObject(health);
        }

        /// Textbox used for debugging
        private void DebugLine_Loaded(object sender, RoutedEventArgs e)
        {
            DebugLine.Text += "loaded\n";
            DebugLine.ScrollToEnd();

        }

        public void DebugPlayerLife(int life)
        {
            Player_Life.Text = life.ToString();
        }

        public void DebugPlayerScore(int score)
        {
            Player_Score.Text = score.ToString();
        }

        public void DebugPlayerPos(System.Numerics.Vector2 pos)
        {
            PlayerPosX.Text = pos.X.ToString();
            PlayerPosY.Text = pos.Y.ToString();
        }
        public void DebugPlayerV(System.Numerics.Vector2 v)
        {
            PlayerVX.Text = v.X.ToString();
            PlayerVY.Text = v.Y.ToString();
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
        private void GoUp_Button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoDown_Button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoLeft_Button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoRight_Button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Shoot_bt1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Shoot_bt2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Shoot_bt3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Shoot2_bt1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Shoot2_bt2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PauseBt_Click(object sender, RoutedEventArgs e)
        {
            if (world.pause)
            {
                world.UnPause();
                PauseBt.Content = "Pause";
            } else
            {
                world.Pause();
                PauseBt.Content = "Unpause";
            }
        }

        private void Bomb_bt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Slow_bt_Click(object sender, RoutedEventArgs e)
        {

        }

        public void DebugWrite(string text)
        {
            DebugLine.Text += text + "\n";
            DebugLine.ScrollToEnd();
        }
    }
}
