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
    class ChooseWorldScreen
    {

        private Texture2D texture;
        private Game1 game;
        public Rectangle mainFrame;

        public Rectangle world1rect;
        public Rectangle world2rect;
        public Rectangle world3rect;
        public Rectangle backbuttonw;
        public int Widths;
        public int Heights;
        public Screen current;

        public ChooseWorldScreen(Game1 game)
        {
            this.game = game;
            Widths = game.GraphicsDevice.Viewport.Width;
            Heights = game.GraphicsDevice.Viewport.Height;
            texture = game.Content.Load<Texture2D>("selectworld");
            mainFrame = new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            world1rect = new Rectangle(rW(185), rH(48), rW(143), rH(159));
            world2rect = new Rectangle(rW(444), rH(81), rW(141), rH(155));
            world3rect = new Rectangle(rW(200), rH(258), rW(134), rH(166));
            backbuttonw = new Rectangle(rW(44), rH(16), rW(60), rH(56));
            current = Screen.ChooseWorldScreen;
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
        }


        public Screen Mouseclik(int x, int y)
        {
            Rectangle mouserec = new Rectangle(x, y, 10, 10);
            if (mouserec.Intersects(world1rect))
            {
                current = Screen.ChooseLevelScreen;
                return (current);
            }
            else if (mouserec.Intersects(world2rect))
            {
                current = Screen.ChooseLevelScreen2;
                return (current);
            }
            else if (mouserec.Intersects(world3rect))
            {
                current = Screen.ChooseLevelScreen3;
                return (current);
            }
            else if (mouserec.Intersects(backbuttonw))
            {
                current = Screen.StartScreen;
                return (current);
            }
            return (current);
        }


        public int rW(double a)
        {
            double b;

            b = (a / 800 * 100);
            return (int)(b * Widths / 100);
        }

        public int rH(double a)
        {
            double b;

            b = (a / 480 * 100);
            return (int)(b * Heights / 100);
        }
    }
}
