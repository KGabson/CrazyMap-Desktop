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
    class StartScreen
    {
        private Texture2D texture;
        private Game1 game;
        private KeyboardState lastState;
        public Rectangle mainFrame;

        // definition des buttons de la page

        private Texture2D startbtext;
        private Rectangle startbrect;

        // sound
        private Rectangle soundrect;
        private Texture2D soundtext;
        private Texture2D soundofftext;
        public bool varsound;

        // Help
        private Rectangle lumrect;
        private Texture2D lumtext;

        // join tuto
        private Rectangle infosrect;
        private Texture2D texttuto;

        public int Widths;
        public int Heights;
        public Screen current;
        // spritefont
        // private SpriteFont _font;

        // geotext
        private Texture2D bangeo;

       // MouseState prevmouse;
       // MouseState curmouse;

        public StartScreen(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("StartScreen");
            Widths = game.GraphicsDevice.Viewport.Width;
            Heights = game.GraphicsDevice.Viewport.Height;
            mainFrame = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            lastState = Keyboard.GetState();

            // definition des buttons 
           // startbrect = new Rectangle(rdnW(370), rdnH(258), rdnW(88), rdnH(88));
            startbrect = new Rectangle(rdnW(370), rdnH(258), rdnW(88), rdnH(88));
            startbtext = game.Content.Load<Texture2D>("bplay");

            infosrect = new Rectangle(rdnW(680), rdnH(408), rdnW(48), rdnH(48)); // forme
            texttuto = game.Content.Load<Texture2D>("interro");

           

            //  _font = game.Content.Load<SpriteFont>("ForScore");
            bangeo = game.Content.Load<Texture2D>("banfin");

            // to use
            soundtext = game.Content.Load<Texture2D>("soundonb");
            soundrect = new Rectangle(rdnW(58), rdnH(25), rdnW(48), rdnH(48));

            lumrect = new Rectangle(rdnW(680), rdnH(25), rdnW(48), rdnH(48));
            lumtext = game.Content.Load<Texture2D>("lampb");

           // shoprect = new Rectangle(rdnW(147), rdnH(384), rdnW(48), rdnH(48));
           // settingsrect = new Rectangle(rdnW(600), rdnH(408), rdnW(48), rdnH(48));

            soundofftext = game.Content.Load<Texture2D>("musicoff");
            varsound = true;

            current = Screen.StartScreen;
        }

        public int rdnW(double a)
        {
            double b;

            b = (a / 800 * 100);
            return (int)(b * Widths / 100);
        }

        public int rdnH(double a)
        {
            double b;

            b = (a / 480 * 100);
            return (int)(b * Heights / 100);
        }

        public Screen Update()
        {
            // Mouseclick

            MouseState curmouse = Mouse.GetState();

            if (curmouse.LeftButton == ButtonState.Pressed)
                Mouseclik((int)curmouse.X, (int)curmouse.Y);

            TouchPanel.EnabledGestures =
                      GestureType.Tap |
                      GestureType.DoubleTap |
                      GestureType.FreeDrag;

            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                switch (gesture.GestureType)
                {
                    case GestureType.Tap:
                        Mouseclik((int)gesture.Position.X, (int)gesture.Position.Y);
                        break;
                }
            }        
            return (current);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null)
                spriteBatch.Draw(texture, mainFrame, Color.White);
            spriteBatch.Draw(startbtext, startbrect, Color.White);
            spriteBatch.Draw(texttuto, infosrect, Color.White);
            //  spriteBatch.DrawString(_font, "By", new Vector2(260, 220), Color.Black);
            spriteBatch.Draw(bangeo, new Rectangle(rdnW(405), rdnH(214), rdnW(120), rdnH(25)), Color.White);
            spriteBatch.Draw(lumtext, lumrect, Color.White);
            if (varsound)
                spriteBatch.Draw(soundtext, soundrect, Color.White);
            else
                spriteBatch.Draw(soundofftext, soundrect, Color.White);
        }

        public Screen Mouseclik(int x, int y)
        {
            Rectangle mouserec = new Rectangle(x, y, 10, 10);
            if (mouserec.Intersects(startbrect))
            {
                current = Screen.ChooseWorldScreen;
            }
            else if (mouserec.Intersects(lumrect))
            {
                current = Screen.TutoScreen;
            }
            else if (mouserec.Intersects(soundrect))
            {
                if (varsound)
                {
                    varsound = false;
                }
                else
                {
                    varsound = true;
                }
            }
	    else if (mouserec.Intersects(infosrect))
            {
                current = Screen.CreditScreen;
            }
            return (current);
        }
    }
}

