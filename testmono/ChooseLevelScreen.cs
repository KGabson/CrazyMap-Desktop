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
    class ChooseLevelScreen
    {
        private Game1 game;
        public Rectangle mainFrame;
        private Texture2D texture;

        public Rectangle level1rect;
        public Rectangle level2rect;
        public Rectangle level3rect;

        public Rectangle backbutton;
        public int Widths;
        public int Heights;
        public Screen current;

        public ChooseLevelScreen(Game1 game)
        {
            this.game = game;
            Widths = game.GraphicsDevice.Viewport.Width;
            Heights = game.GraphicsDevice.Viewport.Height;
            texture = game.Content.Load<Texture2D>("selectlevel");
            mainFrame = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            level1rect = new Rectangle(rw(120), rh(63), rw(73), rh(87));
            level2rect = new Rectangle(rw(230), rh(83), rw(80), rh(87));
            level3rect = new Rectangle(rw(340), rh(54), rw(80), rh(87));
            backbutton = new Rectangle(rw(44), rh(16), rw(60), rh(56));
            current = Screen.ChooseLevelScreen;
        }

        public Screen Update()
        {
            MouseState curmouse = Mouse.GetState();

            if (curmouse.LeftButton == ButtonState.Pressed)
                Mouseclik((int)curmouse.X, (int)curmouse.Y);

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
            if (texture != null)
                spriteBatch.Draw(texture, mainFrame, Color.White);
            // texture de test a  mettre ou placer le rectangle
        //  spriteBatch.Draw(startbutton, level3rect, Color.White);
        }

        public Screen Mouseclik(int x, int y)
        {
            Rectangle mouserec = new Rectangle(x, y, 10, 10);
            if (mouserec.Intersects(level1rect))
            {
                GlobalVar.texture = "textsable";
                GlobalVar.xmlfile = "pets";
                GlobalVar.sound = "grassland";
                current = Screen.GamePlayScreen;
            }
            else if (mouserec.Intersects(level2rect))
            {
                GlobalVar.texture = "textg";
                GlobalVar.xmlfile = "Level";
                GlobalVar.sound = "grassland";
                current = Screen.GamePlayScreen;
            }
            else if (mouserec.Intersects(level3rect))
            {

                GlobalVar.texture = "textsable";
                GlobalVar.xmlfile = "Level";
                GlobalVar.sound = "grassland";
                current = Screen.GamePlayScreen;
            }
            else if (mouserec.Intersects(backbutton))
            {
                current = Screen.ChooseWorldScreen;
            }
            return (current);
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
    }
}
