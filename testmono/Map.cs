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
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using MathNet.Numerics;

namespace testmono
{
    class Map
    {
        // Touchscreen enable
        TouchPanelCapabilities touchCap;

        // teXt en dur bouton station et gps
        public Texture2D textstation;
        public Texture2D textgps;
        public Texture2D textgpssel;
        public Texture2D textstationsel;

        // button add and less
        public Texture2D butless;
        public Rectangle btlessrect;
        public Rectangle butaddrect;
        public Rectangle addgpsrect;
        // bracket
        public Texture2D bracket;
        public Rectangle bracketRect;


        // Button pause
        Texture2D ButtonPauseText;
        private Rectangle ButtonPauseRect;

        //Pause Pop Up
        public bool PausePopUp = false;
        Texture2D PauseScreen;
        private Rectangle PauseRect;

        private int save;
        string[] listimg = { "arb", // 0 Arbre base
                               "sap", // 1 Arbre Sapin
                               "hai", // 2 Haies
                               "mais", // 3 maison noir base
                               "roadt", // 4 road terre
                               "clot", // 5 Cloture
                               "Borne", // !!! 6 Bornes
                               "dune", // 7 dune
                               "roche", // 8 roche
                               "caban", // 9 cabane
                               "caban2", // 10 petite cabane
                               "imme", // 11 immeuble
                               "immep", // 12 petit immeuble
                                "Cactus", // 13 cactus
                                "Palm",  // 14 palmier
                                "routebase", // 15 route de base
                                "routemont", // 16 route montagne
                                "entrepot",
                                "caban"}; // clot vertical

        Vector2 TamponPosition;
        public List<Obstacle> ListObs;
        public List<Borne> ListBrn;
        public List<XmlData> ListElemXml;
        public List<DragObj> ListDragObj;

        bool istake = false;
        public List<Vise> ListVise;
        GraphicsDevice gphc;

        public int counter = 0;
        //SoundEffect _ballBounceWall;
        public int Widths;
        public int Heights;
        //private bool statebutton;
        // if state button == true mettre dans draw le menu pause + jeux sur pause dans update.

        MouseState prevmouse;
        MouseState curmouse;
        MouseState ppmouse;
        MouseState pppmouse;

        public static List<XmlData> DeserializeFromXml(string inputFile)
        {
            List<XmlData> mydata = new List<XmlData>();
            XmlSerializer s = new XmlSerializer(typeof(List<XmlData>), new XmlRootAttribute("Asset"));
            //XmlData[] deserializedObject = default(XmlData[]);
           // byte[] byteArray = Encoding.UTF8.GetBytes(inputFile);
         //   byte[] byteArray = Encoding.ASCII.GetBytes(inputfile);
           // MemoryStream stream = new MemoryStream(byteArray);
            
            Stream test = TitleContainer.OpenStream("pets.xml");
            using (TextReader txtReader = new StreamReader(test))
            {
                mydata = (List<XmlData>)s.Deserialize(txtReader);
            }

            return mydata;
        }


        public void Initialize(string path, ContentManager Content, GraphicsDevice Graphics)
        {
            gphc = Graphics;
            Widths = Graphics.Viewport.Width;
            Heights = Graphics.Viewport.Height;
            ListElemXml = DeserializeFromXml(path);

            ListObs = new List<Obstacle>();
            ListBrn = new List<Borne>();
            FillList(Content);
            textstation = Content.Load<Texture2D>("Gps");
            textgps = Content.Load<Texture2D>("Station");
            textgpssel = Content.Load<Texture2D>("Stationsed");
            textstationsel = Content.Load<Texture2D>("Gpssed");
            // test de rajout de station en dur
            ListDragObj = new List<DragObj>();
            //  ListDragObj.Add(Station);
            ButtonPauseText = Content.Load<Texture2D>("buttonpause");
            ButtonPauseRect = new Rectangle(730, 30, 48, 48);
            // Fin partie bucvket dragobj

            // panier de station + poubelle
            butless = Content.Load<Texture2D>("trash");
            butaddrect = new Rectangle(rw(325), rh(408), rw(50), rh(50));
            addgpsrect = new Rectangle(rw(390), rh(408), rw(50), rh(50));
            // bouton trash :
            btlessrect = new Rectangle(rw(455), rh(408), rw(50), rh(50));

            // definition pop up pause screen
            PauseScreen = Content.Load<Texture2D>("PauseScreen");
            PauseRect = new Rectangle(rw(50), rh(200), rw(693), rh(205));


            ListVise = new List<Vise>();

            bracketRect = new Rectangle(rw(315), rh(400), rw(210), rh(70));
            bracket = Content.Load<Texture2D>("panier");
           // _ballBounceWall = Content.Load<SoundEffect>(GlobalVar.sound);
           // _ballBounceWall.Play();
            curmouse = Mouse.GetState();
        }

        public void AddToDragObjList(DragObj ToAdd)
        {
            ListDragObj.Add(ToAdd);
        }

        // Separate Borne and Obstacle from XML file to List;
        // List Collidable object a creer puis checker les collision plus tard 
        public void FillList(ContentManager Content)
        {
            for (int i = 0; i < ListElemXml.Count(); i++)
            {
                if (ListElemXml[i].id == 6)
                {
                    ListBrn.Add(new Borne(Content.Load<Texture2D>(listimg[ListElemXml[i].id]),
                    new Vector2((float)rw(ListElemXml[i].posx), (float)rh(ListElemXml[i].posy)), Content.Load<Texture2D>("Brn_eclai"), Widths, Heights));
                }
                else
                {
                    ListObs.Add(new Obstacle(Content.Load<Texture2D>(listimg[ListElemXml[i].id]),
                        new Vector2((float)rw(ListElemXml[i].posx), (float)rh(ListElemXml[i].posy)), ListElemXml[i].rot, ListElemXml[i].id, rw(ListElemXml[i].Width), rh(ListElemXml[i].Height), Widths, Heights));
                    // bug avec GridMap // desctiver puis reessayer petit a petit
                }
            }
        }

                public bool IsItFinish()
        {
           

            // tte lzq bornes sont elle allumé
            // check coli entre objet et obs et enlver le check au dessus
            int i = 0;
            foreach (Borne bn in ListBrn)
            {
                if (bn.islight == false)
                    return (false);
            }
            foreach (DragObj dob in ListDragObj)
            {
                // if at least 2 gps.
                if (dob.IsGps == true)
                    i += 1;
            }
            foreach (Vise vs in ListVise)
            {
                if (vs.nice == false)
                    return false;
            }


         

            // mettre une pause
            if (i >= 2 && ListVise.Count == ListDragObj.Count - 1 && curmouse.LeftButton == ButtonState.Released)
            {

                // ----> mettre la visée en vert.
                // metre in timer afind'avoir le temps de voir le changement de couleur de la visé
                foreach (Vise vs in ListVise)
                {
                    vs.colorvise = Color.Red; // red pour voir sis a marche pour le moment.
                }
                return (true); // Un peu trop brutal comme fin

            }//&&  verifier qu'ils ne sont pas relié ? enfaite they can ?
            else
                return (false);
            // ++ check que toute les lignes sont rouge entre les stations.
            // effet fade in au moment de la win ( comment to del);

            // chemin entre les drag OBj ---> pas a verifier
        }


        public void Update(GameTime gameTime)
        {
            double ClickTimer = 0;
            const double TimerDelay = 500;

            if (ListDragObj.Count == 0)
            {
                foreach (Borne br in ListBrn)
                {
                    br.islight = false;
                }
            }

            //prevmouse = curmouse;
    
            curmouse = Mouse.GetState();
            ClickTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            // line to use with mouse on tablet
            if (curmouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released)
            {
                    Mouseclik((int)curmouse.X, (int)curmouse.Y);
            }
            else if (curmouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Pressed)
            {
                 CheckMoove(new Vector2(curmouse.X, curmouse.Y));
            }
            if (curmouse.LeftButton == ButtonState.Pressed && prevmouse.LeftButton == ButtonState.Released && ClickTimer < TimerDelay)
                    HandleButtonVise((int)curmouse.X, (int)curmouse.Y);
            // released et press differement

            prevmouse = curmouse;
            ppmouse = prevmouse;
            pppmouse = ppmouse;

                 touchCap = TouchPanel.GetCapabilities();
           
                    TouchPanel.EnabledGestures =
                   GestureType.Tap |
                   GestureType.DoubleTap |
                   GestureType.FreeDrag;

                    while (TouchPanel.IsGestureAvailable)
                    {
                        GestureSample gesture = TouchPanel.ReadGesture();

                        switch (gesture.GestureType)
                        {
                            case GestureType.DoubleTap:
                                HandleButtonVise((int)gesture.Position.X, (int)gesture.Position.Y);
                                break;
                            case GestureType.Tap:
                                Mouseclik((int)gesture.Position.X, (int)gesture.Position.Y);
                                break;
                            case GestureType.FreeDrag:
                                CheckMoove(gesture.Position);
                                break;
                          }
                    }
            foreach (Vise vs in ListVise)
            {
                vs.Update();
            }
        }

        public void HandleButtonVise(int x, int y)
        {
            for (int i = 0; i < ListDragObj.Count(); i++)
            {
                if (ListDragObj[i].Shape.Contains(x, y))
                {
                    if (istake == true)
                    {
                        // si doubletap surstation deja sel
                        if (ListDragObj[i].isvise == true)
                        {
                            ListDragObj[i].isvise = false;
                        }
                        else // creer vise
                        {
                            ListDragObj[i].isvise = true;
                            Vise vst;
                            vst = Vise_Ready();
                            if (vst != null)
                                ListVise.Add(vst);
                            else
                            {

                            }
                            // tracer la ligne entre les 2
                        }
                        ListDragObj[i].isvise = false;
                        istake = false;
                    }
                    else
                    {
                        ListDragObj[i].isvise = true;
                        istake = true; // get isvisedragobj
                    }
                    break;
                }
            }
        }

        // declare une vle vise
        public Vise Vise_Ready()
        {
            DragObj tempo1 = null;
            DragObj tempo2 = null;
            bool check = false;

            foreach (DragObj db in ListDragObj)
            {
                if (db.isvise == true && check == false)
                {
                    tempo1 = db;
                    db.isvise = false;
                    check = true;
                }
                else if (db.isvise == true && check == true)
                {
                    tempo2 = db;
                    db.isvise = false;
                }
            }
            // verif que les 2 station n'ont pas deja une vise en elle.
            for (int i = 0; i < ListVise.Count(); i++)
            {
                if ((ListVise[i].st1 == tempo1 && ListVise[i].st2 == tempo2) || (ListVise[i].st1 == tempo2 && ListVise[i].st2 == tempo1))
                    return (null);
            }
            return (new Vise(new Vector2(tempo1.Shape.Center.X, tempo1.Shape.Center.Y), new Vector2(tempo2.Shape.Center.X, tempo2.Shape.Center.Y), tempo1, tempo2));
        }

        // fct qui selectionne une station et qui update la pos en fct.
        public void CheckMoove(Vector2 PositionTouch)
        {
            for (int i = 0; i < ListDragObj.Count(); i++)
            {
                if (ListDragObj[i].Shape.Contains((int)PositionTouch.X, (int)PositionTouch.Y))
                {
                    //Permit to avoid several Obj to moove in the same time
                    if (!OnlyOneSelected(ListDragObj))
                    {
                        ListDragObj[i].selected = true;
                        TamponPosition = ListDragObj[i].Position; // la ! envoyer 1 pos, 
                        save = i;
                    }
                }
                else
                {
                    ListDragObj[i].selected = false;
                }
                ListDragObj[i].Update(PositionTouch, ListObs);
                // enlever le selected et mettre un break : 
                // 
                // collision mettre dans une fonction
                // soucis dans le deplacement refaire , plutot refaire la grid et le systeme de check dedans
            }
        }

        public void add_station_list(DragObj justcreated)
        {
            // Limitation a 30 station pour le moment
            if (ListDragObj.Count() < 30)
                ListDragObj.Add(justcreated);
        }

        // not used fonction
        public void rm_station_list()
        {
            // ListDragObj.RemoveRange(0, ListDragObj.Count());
            if (ListDragObj.Count() > 0)
            {
                ListDragObj.RemoveAt(ListDragObj.Count() - 1);
            }
        }

        // button trash
        public void rmone_station_list(DragObj sel)
        {
            // line de limitation du nombre de gps, du coup on peut pas les supprimer c'est plus simple ?
            if (sel.IsGps == false)
            {
                if (sel.isvise == true)
                {
                    istake = false;
                }
                if (ListDragObj.Count() > 0)
                {
                    // boucle qui supprime une visé si on suppr une station visé
                    // bug si plusieur visé sur la station removed
                    // verifié chaque visé avec le drag obj
                    foreach (Vise vs in ListVise)
                    {
                        if ((sel == vs.st1) || (sel == vs.st2))
                        {
                            ListVise.Remove(vs);
                            break;
                        }
                    }
                    ListDragObj.Remove(sel);
                }
            }
        }

        public void Mouseclik(int x, int y)
        {
            Rectangle mouserec = new Rectangle(x, y, 10, 10);
            //Ester Egg geosat
            //if (mouserec.Intersects(ButtonPauseRect))
            // {
            //   if (PausePopUp)
            //   PausePopUp = false;
            // else
            //   PausePopUp = true;
            // }
            if (butaddrect.Intersects(mouserec))
            {
                DragObj newone;

                newone = new DragObj();
                newone.Initialize(textgps, new Vector2(rw(320), rh(335)), textgpssel, false);
                add_station_list(newone);
            }
            else if (addgpsrect.Intersects(mouserec))
            {
                DragObj newone;

                if (counter < 2)
                {
                    counter += 1;
                    newone = new DragObj();
                    newone.Initialize(textstation, new Vector2(rw(380), rh(335)), textstationsel, true);
                    add_station_list(newone);
                }
            }
            for (int i = 0; i < ListDragObj.Count(); i++)
            {
                if (ListDragObj[i].Shape.Intersects(btlessrect))
                {
                    rmone_station_list(ListDragObj[i]);

                }
            }
        }

        // verifie qu'on ne puisse déplacer qu'une station a la fois
        public bool OnlyOneSelected(List<DragObj> ListDO)
        {
            for (int i = 0; i < ListDO.Count(); i++)
            {
                if (ListDO[i].selected)
                    return (true);
            }
            return (false);
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

        // ensemble des Draw sur la map
        public void Draw(SpriteBatch spritebatch)
        {
            Texture2D blank = new Texture2D(gphc, 1, 1, false, SurfaceFormat.Color);
            blank.SetData(new[] { Color.Red });
            Point A;
            Point B;
            Color col = Color.White;

            if (PausePopUp)
            {
                spritebatch.Draw(PauseScreen, PauseRect, Color.White);
            }
            else
            {
                for (int i = 0; i < ListObs.Count(); i++)
                {
                    ListObs[i].Draw(spritebatch);
                }
                for (int i = 0; i < ListBrn.Count(); i++)
                {
                    ListBrn[i].Draw(spritebatch);
                    // boucle permettant de débuger Drag --> borne 
                    // DEBUGAGE HERE IF NEEDED 
                    if (ListDragObj.Count < 0)
                    {
                        foreach (Obstacle ob in ListObs)
                        {
                            A = new Point(ListBrn[i].rectborn.Center.X, ListBrn[i].rectborn.Center.Y);
                            B = new Point(ListDragObj[0].Shape.Center.X, ListDragObj[0].Shape.Center.Y);
                            if (interclass.LineIntersectsRect(A, B, ob.Shape))
                            {
                                col = Color.Black;
                                break;
                            }
                            else
                                col = Color.Red;
                        }
                        DrawLine(spritebatch, blank, 3, col, new Vector2(ListDragObj[0].Shape.Center.X, ListDragObj[0].Shape.Center.Y), new Vector2(ListBrn[i].rectborn.Center.X, ListBrn[i].rectborn.Center.Y));
                    }
                }

                foreach (Vise vs in ListVise)
                {
                    vs.Draw(spritebatch, blank, this.ListObs);
                }

                // dessin des dragobj (station, gps)

                spritebatch.Draw(bracket, bracketRect, Color.White);
                spritebatch.Draw(textstation, addgpsrect, Color.White);
                spritebatch.Draw(textgps, butaddrect, Color.White);
                spritebatch.Draw(butless, btlessrect, Color.White);
                for (int a = 0; a < ListDragObj.Count(); a++)
                {
                    ListDragObj[a].Draw(spritebatch);
                }
            }
            //  spritebatch.Draw(ButtonPauseText, ButtonPauseRect, Color.White);
        }

        void DrawLine(SpriteBatch batch, Texture2D blank, float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            batch.Draw(blank, point1, null, color, angle, Vector2.Zero, new Vector2(length, width), SpriteEffects.None, 0);
        }
    }
}
