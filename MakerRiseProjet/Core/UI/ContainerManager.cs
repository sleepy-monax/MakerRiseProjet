using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RiseEngine.Core.UI
{
    public class ContainerManager : BaseObject
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

        public override void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            if (!(CurrentContainerKey == "null")) {
                Containers[CurrentContainerKey].Update(Mouse, KeyBoard, gameTime);
            }

            base.Update(Mouse, KeyBoard, gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            if (!(CurrentContainerKey == "null"))
            {
                Containers[CurrentContainerKey].Draw(spriteBatch, gameTime);
            }

            base.Draw(spriteBatch, gameTime);
        }
    }
}
