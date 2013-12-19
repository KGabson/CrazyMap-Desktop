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
    class CreditScreen
    {
        private Texture2D background;
        private Rectangle Mainframe;
        private Game1 game;
        private Texture2D background2;

        // definition des buttons
        private Rectangle Gobackbut;
        private Texture2D textgobackb;

        // spritefont
       // private SpriteFont _font;
        public int Widths;
        public int Heights;
        Screen Current;
        private Rectangle creditrect;
        private Texture2D textcredit;
        private bool bolos;
        // FontFile _font

        // s'occuper de faire la page de fin
        public CreditScreen(Game1 game)
        {
            this.game = game;
            Widths = game.GraphicsDevice.Viewport.Width;
            Heights = game.GraphicsDevice.Viewport.Height;
            background = game.Content.Load<Texture2D>("CreditScreen");
            Mainframe = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            background2 = game.Content.Load<Texture2D>("CreditScreen2");

            textgobackb = game.Content.Load<Texture2D>("backb");
            Gobackbut = new Rectangle(rw(44), rh(16), rw(60), rh(56));

            creditrect = new Rectangle(rw(680), rh(25), rw(50), rh(50));
            textcredit = game.Content.Load<Texture2D>("infob");

            Current = Screen.CreditScreen;
            bolos = false;
           // _font = game.Content.Load<SpriteFont>("ForScore");
          //  _font = FontLoader.Load("ForScore.xml");
        }

        public Screen Update()
        {
            MouseState curmouse = Mouse.GetState();

            if (curmouse.LeftButton == ButtonState.Pressed)
               return Mouseclik((int)curmouse.X, (int)curmouse.Y);


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

        public Screen Mouseclik(int px, int py)
        {
            if (Gobackbut.Contains(px, py))
            {
                Current = Screen.StartScreen;
            }
            else if (creditrect.Contains(px, py) && bolos == false)
            {
                if (!bolos)
                    bolos = true;
                else
                    bolos = false;
            }
            return Current;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (bolos == false)
            {
                spriteBatch.Draw(background, Mainframe, Color.White);
                spriteBatch.Draw(textcredit, creditrect, Color.White);
            }
            else
            {
                spriteBatch.Draw(background2, Mainframe, Color.White);
            }
            spriteBatch.Draw(textgobackb, Gobackbut, Color.White);
        }
    }
}
