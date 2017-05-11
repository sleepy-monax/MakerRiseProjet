using Maker.RiseEngine.Core.Ressources;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Globalization;

namespace Maker.RiseEngine.Core.Rendering
{
    public class Parallax : IDrawable
    {

        ParallaxLayer[] Layers;
        public Rectangle DestinationRectangle;
        float[] LayersPos;

        public Parallax(ParallaxLayer[] _Layers, Rectangle _DistinationRectangle)
        {

            DestinationRectangle = _DistinationRectangle;
            Layers = _Layers;
            LayersPos = new float[_Layers.Length];

        }

        public void Update(GameInput playerInput, GameTime gameTime)
        {

            for (int i = 0; i < Layers.Length; i++)
            {

                LayersPos[i] = (LayersPos[i] + Layers[i].Speed);

                double Factor = (double)DestinationRectangle.Width / Layers[i].Sprite.Bounds.Width;

                if (LayersPos[i] > Layers[i].Sprite.Bounds.Width * Factor)
                {
                    LayersPos[i] = 0;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            for (int i = 0; i < Layers.Length; i++)
            {
                double Factor = (double)DestinationRectangle.Width / Layers[i].Sprite.Bounds.Width;
                spriteBatch.Draw(Layers[i].Sprite, new Rectangle(DestinationRectangle.X + (int)LayersPos[i] - (int)(Layers[i].Sprite.Bounds.Width * Factor), DestinationRectangle.Y, DestinationRectangle.Width, DestinationRectangle.Height), Color.White);
                spriteBatch.Draw(Layers[i].Sprite, new Rectangle(DestinationRectangle.X + (int)LayersPos[i], DestinationRectangle.Y, DestinationRectangle.Width, DestinationRectangle.Height), Color.White);

            }


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

            System.IO.StreamReader sr = new System.IO.StreamReader("Plugins\\" + _PluginName + "\\assets\\images\\parallax\\" + _Name + ".rise");
            string f = sr.ReadToEnd().ToDosLineEnd();
            sr.Close();
            f = f.Replace(System.Environment.NewLine, "");
            f = f.Replace("\n", "");
            string[] Ls = f.Split(';');

            List<ParallaxLayer> paralaxeLayerList = new List<ParallaxLayer>();

            for (int i = 0; i < Ls.Length; i++)
            {

                string[] sub = Ls[i].Split(':');
                if (sub.Length == 2)
                    paralaxeLayerList.Add(new ParallaxLayer(rise.ENGINE.RESSOUCES.GetTexture2D(_PluginName, "parallax\\" + _Name + "\\" + sub[0]), float.Parse(sub[1], culture)));

            }

            Parallax p = new Parallax(paralaxeLayerList.ToArray(), _DistinationRectangle);
            return p;
        }

    }
}
