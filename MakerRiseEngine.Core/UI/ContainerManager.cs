using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Maker.RiseEngine.Core.UI
{
    public class ContainerManager : Idrawable
    {

        Dictionary<string, Container> Containers;
        public string CurrentContainerKey;

        public ContainerManager() {

            Containers = new Dictionary<string, Container>();
            CurrentContainerKey = "null";

        }

        public void SwitchContainer(string containerKey) {

            if (Containers.ContainsKey(containerKey)){
                CurrentContainerKey = containerKey;
            }

        }

        public void AddContainer(string key, Container container) {

            Containers.Add(key, container);

        }

        public void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            if (!(CurrentContainerKey == "null")) {
                Containers[CurrentContainerKey].Update(Mouse, KeyBoard, gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (!(CurrentContainerKey == "null"))
            {
                Containers[CurrentContainerKey].Draw(spriteBatch, gameTime);
            }

        }
    }
}
