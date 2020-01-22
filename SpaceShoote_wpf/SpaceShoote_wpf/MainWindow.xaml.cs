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
        public Inputs inputs;

        public TimeSpan showGameOver;

        public int gameState = 0; // 0 before start, 1 playing, 2 pouse, 3 game over, 4 awaiting for game over

        

        /// initializing Viewport and writable bitmap
        private void Viewport_Loaded(object sender, RoutedEventArgs e)
        {
            width = (int)this.ViewPortCointainer.ActualWidth;
            height = (int)this.ViewPortCointainer.ActualHeight;

            writeableBmp = BitmapFactory.New(width, height);
            Viewport.Cursor = Cursors.None;
            Viewport.Source = writeableBmp;


            CreateStartScreen();
            //CreateWorld();
            world.StartTimer(false);
            CompositionTarget.Rendering += CompositionTarget_Rendering;
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);

            inputs = new Inputs(this);
            DebugLine.Text += "Viewport loaded\n";
        }
        
        public void StartGame()
        {
            CreateWorld();
            world.StartTimer(true);
            gameState = 1;
        }
        /// main game loop
        /// calls every frame (same as monitor refresh rate)
        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            //Collect inputs
            inputs.CollectInputs();

            //await Task.Run(new Action(world.GameTick));

            world.GameTick();

            writeableBmp.Clear(Colors.Black);

            foreach (GameObject o in world.gameObjects)
            {
                o.Draw(writeableBmp);
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
            title.Position = new Vector2(width / 2, height / 2 - 32);
            title.name = "Title";
            title.language = "ENG";
            title.spriteSizeX = 188;
            title.spriteSizeY = 16;
            title.Scale = new Vector2(3, 3);
            world.AddObject(title);


            pressStart = new TextImage(this, world, "Press");
            pressStart.Position = new Vector2(width / 2, height / 2  + 32);
            pressStart.name = "Press";
            pressStart.spriteSizeX = 239;
            pressStart.spriteSizeY = 16;
            pressStart.Scale = new Vector2(2, 2);
            world.AddObject(pressStart);

            pauseText = new TextImage(this, world, "Pause");
            pauseText.Position = new Vector2(width / 2, height / 2 + 32);
            pauseText.name = "Pause";
            pauseText.spriteSizeX = 65;
            pauseText.Scale = new Vector2(3, 3);
            pauseText.spriteSizeY = 16;

            gameOverText = new TextImage(this, world, "GameOver");
            gameOverText.Position = new Vector2(width / 2, height / 2 - 32);
            gameOverText.name = "GameOver";
            gameOverText.spriteSizeX = 95;
            gameOverText.spriteSizeY = 16;
            gameOverText.Scale = new Vector2(4, 4);
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

        public void DebugWrite(string text)
        {
            //DebugLine.Text += text + "\n";
            //DebugLine.ScrollToEnd();
        }

        public class Inputs
        {
            MainWindow mainWindow;

            Key goLeftKey1;
            Key goLeftKey2;
            Key goRightKey1;
            Key goRightKey2;
            Key goUpKey1;
            Key goUpKey2;
            Key goDownKey1;
            Key goDownKey2;
            Key shootKey1;
            Key shootKey2;
            Key bombKey;
            Key pauseKey;
            Key slowKey1;

            
            public Vector2 newMousePos;
            private Vector2 previousMousePos = new Vector2(0, 0);
            public Vector2 move = new Vector2(0, 0);
            public bool shoot1 = false;
            public bool shoot2 = false;
            public bool slow = false;
            public bool useMouse = false;
            private bool pauseHeld = false;

            public Inputs(MainWindow window)
            {
                mainWindow = window;

                goLeftKey1 = Key.A;
                goLeftKey2 = Key.Left;

                goRightKey1 = Key.D;
                goRightKey2 = Key.Right;

                goUpKey1 = Key.W;
                goUpKey2 = Key.Up;

                goDownKey1 = Key.S;
                goDownKey2 = Key.Down;

                shootKey1 = Key.E;
                shootKey2 = Key.F;
                //shoot1mouse = MouseAction.LeftClick;
                //shoot2mouse = MouseAction.RightClick;

                bombKey = Key.Space;
                pauseKey = Key.Escape;

                slowKey1 = Key.LeftShift;
            }

            public void CollectInputs()
            {
                float x = 0;
                float y = 0;

                //newMousePos = mainWindow.mousePos;
                newMousePos = new Vector2((float)Mouse.GetPosition(Application.Current.MainWindow).X, (float)Mouse.GetPosition(Application.Current.MainWindow).Y);

                // doesn't update cursor position if it would be outside of game window
                if (!(newMousePos.X < mainWindow.width && newMousePos.Y > 0 && newMousePos.Y < mainWindow.height))
                    newMousePos = previousMousePos;

                // makes game use mouse logic for movement rather than keybord if mouse movement was detected
                if (newMousePos != previousMousePos && previousMousePos != null && newMousePos.X > 0)
                {
                    useMouse = true;
                }

                // movement actions
                if (Keyboard.IsKeyDown(goLeftKey1) || Keyboard.IsKeyDown(goLeftKey2))
                {
                    x += -1;
                    useMouse = false;
                }

                if (Keyboard.IsKeyDown(goRightKey1) || Keyboard.IsKeyDown(goRightKey2))
                {
                    x += 1;
                    useMouse = false;
                }

                if (Keyboard.IsKeyDown(goUpKey1) || Keyboard.IsKeyDown(goUpKey2))
                {
                    y += -1;
                    useMouse = false;
                }

                if (Keyboard.IsKeyDown(goDownKey1) || Keyboard.IsKeyDown(goDownKey2))
                {
                    y += 1;
                    useMouse = false;
                }

                move = new Vector2(x, y);
                //mainWindow.DebugWrite(x.ToString());

                // shoot actions

                if (Keyboard.IsKeyDown(shootKey1) || Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    shoot1 = true;
                }
                else
                    shoot1 = false;

                if (Keyboard.IsKeyDown(shootKey2) || Mouse.RightButton == MouseButtonState.Pressed)
                {
                    shoot2 = true;
                }
                else
                    shoot2 = false;

                // misc actions

                if (Keyboard.IsKeyDown(slowKey1))
                {
                    slow = true;
                }
                else
                    slow = false;
                if (Keyboard.IsKeyDown(bombKey))
                {
                }

                if (Keyboard.IsKeyDown(pauseKey))
                {
                    if (!pauseHeld)
                    {
                        if (!mainWindow.world.pause)
                        {
                            mainWindow.world.Pause();
                        }
                        else
                        {
                            mainWindow.world.UnPause();
                        }
                    }
                    pauseHeld = true;
                } else
                {
                    pauseHeld = false;
                }

                previousMousePos = newMousePos;
            }
        }


    }
}
