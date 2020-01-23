
using GameObjects;
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
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public int height, width;
        WriteableBitmap writeableBmp;
        GameWorld world;

        private string language = "ENG";
        private float difficultyIncrease = 0.02f;
        bool pauseHeld = false;

        /// initializing Viewport and writable bitmap
        private async void Viewport_Loaded(object sender, RoutedEventArgs e)
        {
            width = (int)this.ViewPortCointainer.ActualWidth;
            height = (int)this.ViewPortCointainer.ActualHeight;

            writeableBmp = BitmapFactory.New(width, height);
            Viewport.Cursor = Cursors.None;
            Viewport.Source = writeableBmp;

            await LoadSettings();

            CreateStartScreen();
            //CreateWorld();
            world.StartTimer(false);
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            //DebugLine.Text += "Viewport loaded\n";
        }
        
        public void StartGame()
        {
            CreateWorld();
            world.StartTimer(true);
            world.gameState = 1;
        }

        private async Task LoadSettings()
        {
            DebugLine.Text += Directory.GetCurrentDirectory() + "\n";
            try
            {
                using (StreamReader sr = new StreamReader("../../config.txt"))
                {
                    string line = await sr.ReadToEndAsync();
                    //DebugLine.Text += line + "\n";
                    string[] lines = line.Split('\n');
                    DebugWrite(line);

                    if ((language != "ENG" && language != "PL"))
                    {
                        language = "ENG";
                        throw new ArgumentException("Wrong language setting: line 2");
                    }
                   
                    difficultyIncrease = float.Parse(lines[3], System.Globalization.CultureInfo.InvariantCulture);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed loading settings. Error:\n" + ex.Message);
                DebugWrite(ex.Message);
                difficultyIncrease = 0.02f;
                language = "ENG";
            }
        }
        /// main game loop
        /// calls every frame (same as monitor refresh rate)
         private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //await Task.Run(new Action(world.GameTick));
            world.GameTick();

            writeableBmp.Clear(Colors.Black);

            foreach (GameObject o in world.gameObjects)
            {
                o.Draw(writeableBmp);
            }

            if (Keyboard.IsKeyDown(Key.Escape))
            {
                if (!pauseHeld)
                {
                    if (!world.pause)
                        world.Pause();
                    else
                        world.UnPause();
                }
                pauseHeld = true;
            }
            else
            {
                pauseHeld = false;
            }

            switch (world.gameState)
            {
                case 0:
                    if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        StartGame();
                    }
                    break;
                case 1:
                    world.bossHealth.Draw(writeableBmp);
                    world.playerHealth.Draw(writeableBmp);
                    world.waveTimer.Draw(writeableBmp);
                    world.realTimeScore.Draw(writeableBmp);
                    world.rtScoreText.Draw(writeableBmp);

                    break;
                case 2:
                    world.pauseText.Draw(writeableBmp);
                    break;
                case 3:
                    world.gameOverText.Draw(writeableBmp);
                    world.pressStart.Draw(writeableBmp);
                   
                    world.scoreText.Draw(writeableBmp);
                    world.scoreNumber.Draw(writeableBmp);

                    if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        StartGame();
                    }
                    break;
            }

            if (world.showGameOver != null && world.gameState == 4)
            {
                DebugWrite("game over");
                if (world.GameTime() > world.showGameOver.TotalMilliseconds)
                {

                    world.gameState = 3;
                    TextImage gameOverText = new TextImage(world, "GameOver");
                    gameOverText.Position = new Vector2(width / 2, height / 2 - 64);
                    gameOverText.name = "GameOver";
                    gameOverText.spriteSizeX = 105;
                    gameOverText.spriteSizeY = 16;
                    gameOverText.Scale = new Vector2(4, 4);
                    world.gameOverText = gameOverText;

                    TextImage scoreText = new TextImage(world, "Points");
                    scoreText.name = "Points";
                    scoreText.spriteSizeX = 71;
                    scoreText.spriteSizeY = 16;
                    scoreText.Position = new Vector2(width / 2 - scoreText.spriteSizeX, height / 2);
                    scoreText.Scale = new Vector2(2, 2);
                    world.scoreText = scoreText;

                    TextImage scoreNumber = new TextImage(world, world.score, false);
                    scoreNumber.name = "font_spreadsheet_x11x16.png";
                    scoreNumber.spriteSizeX = 11;
                    scoreNumber.spriteSizeY = 16;
                    scoreNumber.Position = new Vector2(width / 2 + 44, height / 2);
                    scoreNumber.Scale = new Vector2(2, 2);
                    world.scoreNumber = scoreNumber;

                    world.pressStart.Position = new Vector2(width / 2, height / 2 + 256);
                }
            }

            Object_Counter.Text = world.gameObjects.Count.ToString();
            FPS_Counter.Text = world.GetFPS().ToString();
        }

        public void CreateStartScreen()
        {
            world = new GameWorld(new Vector2(width, height), language);
            world.gameState = 0;
            var backgroundlayer1 = new BackgroundLayer1(world);
            var backgroundlayer2 = new BackgroundLayer2(world);

            world.AddObject(backgroundlayer1);
            world.AddObject(backgroundlayer2);

            TextImage title = new TextImage(world, "Title");
            title.Position = new Vector2(width / 2, height / 2 - 64);
            title.name = "Title";
            title.spriteSizeX = 190;
            title.spriteSizeY = 16;
            title.Scale = new Vector2(3, 3);
            world.AddObject(title);


            TextImage pressStart = new TextImage(world, "Press");
            pressStart.Position = new Vector2(width / 2, height / 2  + 64);
            pressStart.name = "Press";
            pressStart.spriteSizeX = 275;
            pressStart.spriteSizeY = 47;
            pressStart.Scale = new Vector2(2, 2);
            world.AddObject(pressStart);
            world.pressStart = pressStart;
        }

        /// initializing game world
        public void CreateWorld()
        {
            world = null;
            world = new GameWorld(new Vector2(width, height), language);

            world.player = new Player(world);
            world.player.Position = new Vector2(width / 2, height - world.player.spriteSizeY * world.player.Scale.Y);
            var backgroundlayer1 = new BackgroundLayer1(world);
            var backgroundlayer2 = new BackgroundLayer2(world);
            world.playerHealth = new Bar(world);
            world.playerHealth.startColor = new Vector3(50, 150, 50);

            world.bossHealth = new Bar(world);
            world.bossHealth.Position = new Vector2(0, world.bossHealth.spriteSizeY);
            world.bossHealth.startColor = new Vector3(255, 80, 50);

            world.waveTimer = new Bar(world);
            world.waveTimer.Position = new Vector2(0, 0);
            world.waveTimer.startColor = new Vector3(250, 250, 100);
            world.waveTimer.endColor = new Vector3(50, 50, 0);

            world.AddObject(backgroundlayer1);
            world.AddObject(backgroundlayer2);
            world.AddObject(world.player);
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

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (world.gameState == 0 || world.gameState == 3)
                StartGame();
        }

        public void DebugWrite(string text)
        {
            DebugLine.Text += text + "\n";
            DebugLine.ScrollToEnd();
        }

    }
}
