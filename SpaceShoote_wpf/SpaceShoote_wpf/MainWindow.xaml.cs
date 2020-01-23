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
        public Player player;
        public Bar playerHealth;
        public Bar bossHealth;
        public Bar waveTimer;
        public TextImage pauseText;
        public TextImage gameOverText;
        public TextImage pressStart;
        public TextImage scoreText;
        public TextImage scoreNumber;
        public string language = "ENG";
        public TimeSpan showGameOver;

        public int gameState = 0; // 0 before start, 1 playing, 2 pouse, 3 game over, 4 awaiting for game over

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
            gameState = 1;
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
                    language = lines[1];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DebugWrite(ex.Message);
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

            switch (gameState)
            {
                case 0:
                    if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        StartGame();
                    }
                    break;
                case 1:
                    bossHealth.Draw(writeableBmp);
                    playerHealth.Draw(writeableBmp);
                    waveTimer.Draw(writeableBmp);
                    break;
                case 2:
                    pauseText.Draw(writeableBmp);
                    break;
                case 3:
                    gameOverText.Draw(writeableBmp);
                    pressStart.Draw(writeableBmp);

                    scoreText.Draw(writeableBmp);
                    scoreNumber.Draw(writeableBmp);

                    if (Mouse.LeftButton == MouseButtonState.Pressed || Mouse.LeftButton == MouseButtonState.Pressed)
                    {
                        StartGame();
                    }
                    break;
            }

            if (showGameOver != null && gameState == 4)
            {
                DebugWrite("game over");
                if (world.GameTime() > showGameOver.TotalMilliseconds)
                {
                    
                    gameState = 3;
                    gameOverText = new TextImage(this, world, "GameOver");
                    gameOverText.Position = new Vector2(width / 2, height / 2 - 64);
                    gameOverText.name = "GameOver";
                    gameOverText.spriteSizeX = 105;
                    gameOverText.spriteSizeY = 16;
                    gameOverText.Scale = new Vector2(4, 4);

                    scoreText = new TextImage(this, world, "Points");
                    scoreText.name = "Points";
                    scoreText.spriteSizeX = 71;
                    scoreText.spriteSizeY = 16;
                    scoreText.Position = new Vector2(width / 2 - scoreText.spriteSizeX, height / 2);
                    scoreText.Scale = new Vector2(2, 2);

                    scoreNumber = new TextImage(this, world, world.score, false);
                    scoreNumber.name = "font_spreadsheet_x11x16.png";
                    scoreNumber.spriteSizeX = 11;
                    scoreNumber.spriteSizeY = 16;
                    scoreNumber.Position = new Vector2(width / 2 + 44, height / 2);
                    scoreNumber.Scale = new Vector2(2, 2);

                    pressStart.Position = new Vector2(width / 2, height / 2 + 256);
                }
            }

            Object_Counter.Text = world.gameObjects.Count.ToString();
            FPS_Counter.Text = world.GetFPS().ToString();
        }

        public void CreateStartScreen()
        {
            gameState = 0;
            world = new GameWorld(this);
            var backgroundlayer1 = new BackgroundLayer1(this, world);
            var backgroundlayer2 = new BackgroundLayer2(this, world);

            world.AddObject(backgroundlayer1);
            world.AddObject(backgroundlayer2);

            TextImage title = new TextImage(this, world, "Title");
            title.Position = new Vector2(width / 2, height / 2 - 64);
            title.name = "Title";
            title.spriteSizeX = 190;
            title.spriteSizeY = 16;
            title.Scale = new Vector2(3, 3);
            world.AddObject(title);


            pressStart = new TextImage(this, world, "Press");
            pressStart.Position = new Vector2(width / 2, height / 2  + 64);
            pressStart.name = "Press";
            pressStart.spriteSizeX = 275;
            pressStart.spriteSizeY = 47;
            pressStart.Scale = new Vector2(2, 2);
            world.AddObject(pressStart);

            pauseText = new TextImage(this, world, "Pause");
            pauseText.Position = new Vector2(width / 2, height / 2 + 64);
            pauseText.name = "Pause";
            pauseText.spriteSizeX = 65;
            pauseText.Scale = new Vector2(3, 3);
            pauseText.spriteSizeY = 16;
        }

        /// initializing game world
        public void CreateWorld()
        {
            world = null;
            world = new GameWorld(this);  

            player = new Player(this, world);
            player.Position = new Vector2(width / 2, height - player.spriteSizeY * player.Scale.Y);
            var backgroundlayer1 = new BackgroundLayer1(this, world);
            var backgroundlayer2 = new BackgroundLayer2(this, world);
            playerHealth = new Bar(this, world);

            bossHealth = new Bar(this, world);
            bossHealth.Position = new Vector2(0, bossHealth.spriteSizeY);
            bossHealth.startColor = new Vector3(255, 80, 50);

            waveTimer = new Bar(this, world);
            waveTimer.Position = new Vector2(0, 0);
            waveTimer.startColor = new Vector3(200, 200, 200);
            waveTimer.endColor = new Vector3(255, 100, 0);

            world.AddObject(backgroundlayer1);
            world.AddObject(backgroundlayer2);
            world.AddObject(player);
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
            if (gameState == 0 || gameState == 3)
                StartGame();
        }

        public void DebugWrite(string text)
        {
            DebugLine.Text += text + "\n";
            DebugLine.ScrollToEnd();
        }

    }
}
