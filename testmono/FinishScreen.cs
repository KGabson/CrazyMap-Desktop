using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace testmono
{
    class FinishScreen
    {
        private Texture2D background;
        private Rectangle Mainframe;
        private Game1 game;

        // definition des buttons
        private Rectangle Gobackbut;
        private Texture2D textgobackb;
        private Texture2D textbutpl;
        private Rectangle KeepPlayin;

        // spritefont
      //  private SpriteFont _font;

        // bracket
        public Texture2D bracket;
        public Rectangle bracketRect;
        public Rectangle bracketRect2;

        // starshiptrooper
        public Texture2D Starship1;
        public Texture2D Starship2;
        public Texture2D Starship3;
        public Rectangle Troopers;
        
        public int Widths;
        public int Heights;

        Screen Current;

        // s'occuper de faire la page de fin
        // changer le BPLAY qui retourne sur choose level pas assez d'animation.
        public FinishScreen(Game1 game)
        {
            this.game = game;
            background = game.Content.Load<Texture2D>("endgame");
            Mainframe = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            Widths = game.GraphicsDevice.Viewport.Width;
            Heights = game.GraphicsDevice.Viewport.Height;
            textbutpl = game.Content.Load<Texture2D>("bplay");
            KeepPlayin = new Rectangle(rw(355), rh(290), rw(80), rh(80));
            textgobackb = game.Content.Load<Texture2D>("backb");
            Gobackbut = new Rectangle(rw(44), rh(16), rw(60), rh(56));
           // _font = game.Content.Load<SpriteFont>("ForScore");
            bracketRect = new Rectangle(rw(190), rh(110), rw(150), rh(40));
            bracket = game.Content.Load<Texture2D>("panier");
            bracketRect2 = new Rectangle(rw(190), rh(170), rw(150), rh(40));
            Starship1 = game.Content.Load<Texture2D>("13star");
            Starship2 = game.Content.Load<Texture2D>("23star");
            Starship3 = game.Content.Load<Texture2D>("33star");
            Troopers = new Rectangle(rw(450), rh(130), rw(150), rh(70));
            Current = Screen.GameOverScreen;
        }

        public Screen Update()
        {
            MouseState curmouse = Mouse.GetState();

            if (curmouse.LeftButton == ButtonState.Pressed)
                Mouseclik((int)curmouse.X, (int)curmouse.Y);

            TouchPanel.EnabledGestures =
                      GestureType.Tap;
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                switch (gesture.GestureType)
                {
                    case GestureType.Tap:
                        return Mouseclik((int)gesture.Position.X, (int)gesture.Position.Y);
                }
            }
            return Current;
        }

        public Screen Mouseclik(int px, int py)
        {
            if (KeepPlayin.Contains(px, py))
            {
                Current = Screen.ChooseLevelScreen;
            }
            else if (Gobackbut.Contains(px, py))
            {
                Current =  (Screen.ChooseLevelScreen);
            }
            return (Current);
        }

        public int rw(double a)
        {
            double b;

            b = (a / 800 * 100);
            return (int)(b * Widths / 100);
        }

        public int rh(double a)
        {
            double b;

            b = (a / 480 * 100);
            return (int)(b * Heights / 100);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Mainframe, Color.White);
            spriteBatch.Draw(textbutpl, KeepPlayin, Color.White);
            spriteBatch.Draw(textgobackb, Gobackbut, Color.White);
            spriteBatch.Draw(bracket, bracketRect, Color.White);
            spriteBatch.Draw(bracket, bracketRect2, Color.White);
            spriteBatch.Draw(Starship1, Troopers, Color.White);
        }
    }
}
