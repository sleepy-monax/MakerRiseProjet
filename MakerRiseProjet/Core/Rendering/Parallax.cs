using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Globalization;

namespace RiseEngine.Core.Rendering
{
    public class Parallax : BaseObject
    {

        ParallaxLayer[] Layers;
        Rectangle DestinationRectangle;
        float[] LayersPos;

        public Parallax(ParallaxLayer[] _Layers, Rectangle _DistinationRectangle)
        {

            DestinationRectangle = _DistinationRectangle;
            Layers = _Layers;
            LayersPos = new float[_Layers.Length];
            

        }

        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            for (int i = 0; i < Layers.Length; i++)
            {

                LayersPos[i] = (LayersPos[i] + Layers[i].Speed);

                double Factor =  (double)DestinationRectangle.Width / (double)Layers[i].Sprite.Bounds.Width;

                if (LayersPos[i] > Layers[i].Sprite.Bounds.Width * Factor)
                {
                    LayersPos[i] = 0;
                }

            }

            base.Update(Mouse, KeyBoard, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            for (int i = 0; i < Layers.Length; i++)
            {
                double Factor = (double)DestinationRectangle.Width / (double)Layers[i].Sprite.Bounds.Width;
                spriteBatch.Draw(Layers[i].Sprite, new Rectangle(DestinationRectangle.X + (int)LayersPos[i] - (int)(Layers[i].Sprite.Bounds.Width * Factor), DestinationRectangle.Y, DestinationRectangle.Width, DestinationRectangle.Height), Color.White);
                spriteBatch.Draw(Layers[i].Sprite, new Rectangle(DestinationRectangle.X + (int)LayersPos[i], DestinationRectangle.Y, DestinationRectangle.Width, DestinationRectangle.Height), Color.White);

            }

            base.Draw(spriteBatch, gameTime);
        }

    }

    public class ParallaxLayer
    {
        public float Speed;
        public Texture2D Sprite;

        public ParallaxLayer(Texture2D _Sprite, float _Speed)
        {

            Speed = _Speed;
            Sprite = _Sprite;

        }

    }

    public static class ParallaxParse
    {


        public static Parallax Parse(string _PluginName, string _Name, Rectangle _DistinationRectangle)
        {

            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";

            System.IO.StreamReader sr = new System.IO.StreamReader("Data\\" + _PluginName + "\\Textures2D\\Parallax\\" + _Name + ".rise");
            string f = sr.ReadToEnd();
            sr.Close();

            f = f.Replace(System.Environment.NewLine, "");
            string[] Ls = f.Split(';');

            List<ParallaxLayer> Pl = new List<ParallaxLayer>();

            for (int i = 0; i < Ls.Length; i++)
            {

                string[] sub = Ls[i].Split(':');
                if (sub.Length == 2)
                    Pl.Add(new ParallaxLayer(ContentEngine.Texture2D(_PluginName, "Parallax\\" + _Name + "\\" + sub[0]), float.Parse(sub[1], culture)));

            }

            Parallax p = new Parallax(Pl.ToArray(), _DistinationRectangle);
            return p;
        }

    }
}
