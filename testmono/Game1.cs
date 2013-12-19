using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace testmono
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    public enum Screen
    {
        StartScreen,
        GamePlayScreen,
        GameOverScreen,
        ChooseLevelScreen,
        ChooseLevelScreen2,
        ChooseLevelScreen3,
        ChooseWorldScreen,
        TutoScreen,
        LoadingScreen,
        CreditScreen
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        StartScreen startScreen;
        GamePlayScreen gamePlayScreen;
        ChooseWorldScreen chooseWorldScreen;
        ChooseLevelScreen chooseLevelScreen;
        ChooseLevelScreen2 chooseLevelScreen2;
        ChooseLevelScreen3 chooseLevelScreen3;
        TutoScreen tutoScreen;
        CreditScreen creditScreen;
        FinishScreen endScreen;
        // test;
       // SoundEffect _ballBounceWall;
        public Rectangle mainFrame;
        public Screen currentScreen;

        public Game1()
        {
            // virer le curseur ou pas
            IsMouseVisible = true;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
          //  plus compliqué MediaQueue sa pour jouer unchecked Song
            //_ballBounceWall = Content.Load<SoundEffect>("Forst Land");
           // _ballBounceWall.Play(); // ok sa marche. Load les musique en sopuhd effect.
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
            protected override void Update(GameTime gameTime)
            {
          
            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                    {
                        currentScreen = startScreen.Update();
                        if (currentScreen == Screen.ChooseWorldScreen)
                        {
                            StartSelWorld();
                        }
                        else if (currentScreen == Screen.TutoScreen)
                            StartTutoScreen();
                        else if (currentScreen == Screen.CreditScreen)
                            Startcredits();
                    }
                    break;
                case Screen.GamePlayScreen:
                    if (gamePlayScreen != null)
                        gamePlayScreen.Update(gameTime);
                    break;
                case Screen.ChooseWorldScreen:
                    if (chooseWorldScreen != null)
                    {
                        currentScreen = chooseWorldScreen.Update();
                        if (currentScreen == Screen.ChooseLevelScreen)
                            StartSelLevel();
                        else if (currentScreen == Screen.ChooseLevelScreen2)
                            StartSelLevel2();
                        else if (currentScreen == Screen.ChooseLevelScreen3)
                            StartSelLevel3();
                        else if (currentScreen == Screen.StartScreen)
                            StartStartScreen();
                    }
                    break;
                case Screen.ChooseLevelScreen:
                    if (chooseLevelScreen != null)
                    {
                        currentScreen = chooseLevelScreen.Update();
                        if (currentScreen == Screen.ChooseWorldScreen)
                            StartSelWorld();
                        else if (currentScreen == Screen.GamePlayScreen)
                            Startgame();
                    }
                    break;
                case Screen.GameOverScreen:
                    if (endScreen == null)
                    {
                        startendScreen();
                    }
                    else
                    {
                        currentScreen = endScreen.Update();
                        if (currentScreen == Screen.ChooseLevelScreen)
                            StartSelLevel();
                    }
                    break;
                case Screen.ChooseLevelScreen2:
                    if (chooseLevelScreen2 != null)
                    {
                        currentScreen = chooseLevelScreen2.Update();
                        if (currentScreen == Screen.ChooseWorldScreen)
                            StartSelWorld();
                        else if (currentScreen == Screen.GamePlayScreen)
                            Startgame();
                    }
                    break;
                case Screen.ChooseLevelScreen3:
                    if (chooseLevelScreen3 != null)
                    {
                        currentScreen = chooseLevelScreen3.Update();
                        if (currentScreen == Screen.ChooseWorldScreen)
                            StartSelWorld();
                        else if (currentScreen == Screen.GamePlayScreen)
                            Startgame();
                    }
                    break;
                case Screen.TutoScreen:
                    if (tutoScreen != null)
                    {
                        currentScreen = tutoScreen.Update();
                        if (currentScreen == Screen.StartScreen)
                            StartStartScreen();
                    }
                    break;
                case Screen.CreditScreen:
                    if (creditScreen != null)
                    {
                        currentScreen = creditScreen.Update();
                        if (currentScreen == Screen.StartScreen)
                            StartStartScreen();
                    }
                    break;
                // effet fade-in/out dans le menu de fin
            }
            /////////////////////////////////////////////////////////////

          // tracer droite between drag Obj.
                // check between dragobj.
                // --> algorithme de chemin pas station avec toutes les autres
              //  CheckForTheWin();


            base.Update(gameTime);
        }

        public void StartTutoScreen()
        {

            startScreen = null;
            tutoScreen = new TutoScreen(this);
            currentScreen = Screen.TutoScreen;
        }

        public void startendScreen()
        {
            currentScreen = Screen.GameOverScreen;
           // gamePlayScreen = null;
           endScreen = new FinishScreen(this);
        }

        public Screen ManageTheEnd()
        {

            return Screen.ChooseLevelScreen;
            // return Screen.StartScreen;
        }

        public void Startgame()
        {
            startScreen = null; // toute les autres a null ?
           // loadingScreen = null;
            gamePlayScreen = new GamePlayScreen(this);
            //  gamePlayScreen = new GamePlayScreen(this);
            currentScreen = Screen.GamePlayScreen;
        }

        public void StartSelWorld()
        {
            startScreen = null;
            //endScreen = null;
            chooseLevelScreen = null;

            chooseWorldScreen = new ChooseWorldScreen(this);
            //currentScreen = Screen.ChooseWorldScreen;
        }

        public void StartSelLevel()
        {
            chooseWorldScreen = null;
            startScreen = null;

            chooseLevelScreen = new ChooseLevelScreen(this);
            currentScreen = Screen.ChooseLevelScreen;
        }

        public void StartSelLevel2()
        {
            chooseWorldScreen = null;
            startScreen = null;
            chooseLevelScreen = null;
            chooseLevelScreen3 = null;

            chooseLevelScreen2 = new ChooseLevelScreen2(this);
            currentScreen = Screen.ChooseLevelScreen2;
        }

        public void StartSelLevel3()
        {
            chooseWorldScreen = null;
            startScreen = null;
            chooseLevelScreen = null;
            chooseLevelScreen2 = null;

            chooseLevelScreen3 = new ChooseLevelScreen3(this);
            currentScreen = Screen.ChooseLevelScreen3;
        }

        public void StartStartScreen()
        {
            chooseLevelScreen = null;
            chooseWorldScreen = null;

            startScreen = new StartScreen(this);
            currentScreen = Screen.StartScreen;
        }

        public void Startcredits()
        {
            startScreen = null;
            creditScreen = new CreditScreen(this);
            currentScreen = Screen.CreditScreen;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            switch (currentScreen)
            {
                case Screen.StartScreen:
                    if (startScreen != null)
                        startScreen.Draw(_spriteBatch);
                    break;
                case Screen.GamePlayScreen:
                    if (gamePlayScreen != null)
                        gamePlayScreen.Draw(_spriteBatch);
                    break;
                case Screen.ChooseWorldScreen:
                    if (chooseWorldScreen != null)
                        chooseWorldScreen.Draw(_spriteBatch);
                    break;
                case Screen.ChooseLevelScreen:
                    if (chooseLevelScreen != null)
                        chooseLevelScreen.Draw(_spriteBatch);
                    break;
                case Screen.GameOverScreen:
                    if (endScreen != null)
                        endScreen.Draw(_spriteBatch);
                    break;
                case Screen.ChooseLevelScreen2:
                    if (chooseLevelScreen2 != null)
                        chooseLevelScreen2.Draw(_spriteBatch);
                    break;
                case Screen.ChooseLevelScreen3:
                    if (chooseLevelScreen3 != null)
                        chooseLevelScreen3.Draw(_spriteBatch);
                    break;
                case Screen.TutoScreen:
                    if (tutoScreen != null)
                        tutoScreen.Draw(_spriteBatch);
                    break;
                case Screen.CreditScreen:
                    if (creditScreen != null)
                        creditScreen.Draw(_spriteBatch);
                    break;
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
