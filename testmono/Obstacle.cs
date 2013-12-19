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
    class Obstacle
    {
        public Texture2D TOb;
        public Vector2 VOb;
        public Rectangle Shape;
        public int rotation;
        public int id;
        public int Width;
        public int Height;
        public int Widths;
        public int Heights;
        // rename sans danger a tester ? Shape en fonctiob de 'lid d'objet.
        public Obstacle(Texture2D A, Vector2 B, int rot, int di, int width, int height, int W, int H)
        {
            TOb = A;
            VOb = B;
            rotation = rot;
            Width = width;
            Height = height;
            Widths = W;
            Heights = H;
            // rotation = 0;
            id = di;
            Shape = new Rectangle((int)B.X, (int)B.Y, Width, Height);
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

        public Rectangle DefineShape()
        {
            return (Shape);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TOb, Shape, Color.White);
       }
    }
}
