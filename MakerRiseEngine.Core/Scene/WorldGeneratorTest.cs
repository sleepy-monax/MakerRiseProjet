using Maker.RiseEngine.Core.Generator.Polygonal;
using Maker.RiseEngine.Core.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Core.Scene
{
    public class WorldGeneratorTest : Idrawable
    {

        PolygonalWorld w;

        public WorldGeneratorTest() {
            PolygonalWorldGenerator g = new PolygonalWorldGenerator();
            w = g.CreateWorld(10, 10);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Node n in w.NodesGrid) {

                spriteBatch.DrawCircle(new Vector2(n.x * 75, n.y * 75), 2, 4, Color.Black);

            }
        }

        public void Update(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            
        }
    }
}
