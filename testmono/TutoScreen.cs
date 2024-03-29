﻿using System;
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
    class TutoScreen
    {
        private Texture2D texture;
        //  private Texture2D texture1;
        //  private Texture2D texture2;
        private Game1 game;
        private KeyboardState lastState;
        public Rectangle mainFrame;

        // definition des buttons de la page

        private Texture2D startbtext;
        private Rectangle startbrect;
        private Texture2D folbtext;
        private Rectangle folrect;
        private Texture2D pagina2;

        private bool pagina;
        public int Widths;
        public int Heights;
        Screen current;
         MouseState prevmouse;
         MouseState curmouse;


        public TutoScreen(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("pagina2");
            Widths = game.GraphicsDevice.Viewport.Width;
            Heights = game.GraphicsDevice.Viewport.Height;
            mainFrame = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            lastState = Keyboard.GetState();

            // definition des buttons :
            startbrect = new Rectangle(rw(44), rh(16), rw(60), rh(56));
            startbtext = game.Content.Load<Texture2D>("backb");
            folbtext = game.Content.Load<Texture2D>("otherside");
            folrect = new Rectangle(rw(696), rh(16), rw(60), rh(56));
            pagina2 = game.Content.Load<Texture2D>("Tuto");
            pagina = false;
            current = Screen.TutoScreen;
            curmouse = Mouse.GetState();
        }

        public Screen Update()
        {
            curmouse = Mouse.GetState();
            if (curmouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released)            
                return (Mouseclik((int)curmouse.X, (int)curmouse.Y));
             prevmouse = curmouse;

            TouchPanelCapabilities touchCap = TouchPanel.GetCapabilities();
            if (touchCap.IsConnected)
            {
                TouchCollection touches = TouchPanel.GetState();

                if (touches.Count >= 1)
                {
                    Vector2 PositionTouch = touches[0].Position;
                    if (touches[0].State == TouchLocationState.Released)
                        return (Mouseclik((int)PositionTouch.X, (int)PositionTouch.Y));
                }
            }
            return (current);
        }
       
        public void Draw(SpriteBatch spriteBatch)
        {
            if (pagina == false)
            {
                spriteBatch.Draw(texture, mainFrame, Color.White);
                spriteBatch.Draw(folbtext, folrect, Color.White);
            }
            else
            {
                spriteBatch.Draw(pagina2, mainFrame, Color.White);
            }
            spriteBatch.Draw(startbtext, startbrect, Color.White);

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

        public Screen Mouseclik(int x, int y)
        {
            Rectangle mouserec = new Rectangle(x, y, 10, 10);
            if (mouserec.Intersects(startbrect))
            {
                if (pagina)
                    pagina = false;
                else
                    current = Screen.StartScreen;
            }
            else if (mouserec.Intersects(folrect) && pagina == false)
            {
                if (pagina)
                    pagina = false;
                else
                    pagina = true;
            }
            return (current);
            //current = Screen.TutoScreen;
        }
    }
}
