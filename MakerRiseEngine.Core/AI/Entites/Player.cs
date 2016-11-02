using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RiseEngine.Core.GameObject.Event;
using Microsoft.Xna.Framework.Input;
using RiseEngine.Core.AI;

namespace RiseEngine.Core.AI.Entites
{
    public class Player : AIbase
    {
        int IdleVariant { get; set; }
        int MoveUpVariante { get; set; }
        int MoveDownVariante { get; set; }
        int MoveLeftVariante { get; set; }
        int MoveRightVariante { get; set; }

        int Speed { get; set; }
        public Player(int _MoveUpVariante, int _MoveDownVariante, int _MoveLeftVariante, int _MoveRightVariante, int _IdleVariant)
        {
            IdleVariant = _IdleVariant;

            MoveUpVariante = _MoveUpVariante;
            MoveDownVariante = _MoveDownVariante;
            MoveLeftVariante = _MoveLeftVariante;
            MoveRightVariante = _MoveRightVariante;

            Speed = 5;
        }

        public override void Tick(GameObjectEventArgs e, KeyboardState KeyBoard, MouseState Mouse, GameTime gameTime)
        {

            if (KeyBoard.IsKeyDown(Config.Controls.MoveRun))
            {
                Speed = 10;
            }
            else
            {
                Speed = 5;
            }

            int MoveActionIndex = GameObjectsManager.GetGameObjectIndex("Base.Move");

            if (e.ParrentEntity.Action == -1)
            {
                if (KeyBoard.IsKeyDown(Config.Controls.MoveUp))
                {
                    e.ParrentEntity.Facing = Utils.Facing.Up;
                    e.ParrentEntity.Action = MoveActionIndex;
                    e.ParrentEntity.Variant = MoveUpVariante;
                }
                else if (KeyBoard.IsKeyDown(Config.Controls.MoveDown))
                {
                    e.ParrentEntity.Facing = Utils.Facing.Down;
                    e.ParrentEntity.Action = MoveActionIndex; 
                    e.ParrentEntity.Variant = MoveDownVariante;
                }
                else if (KeyBoard.IsKeyDown(Config.Controls.MoveLeft))
                {
                    e.ParrentEntity.Facing = Utils.Facing.Left;
                    e.ParrentEntity.Action = MoveActionIndex; 
                    e.ParrentEntity.Variant = MoveLeftVariante;
                }
                else if (KeyBoard.IsKeyDown(Config.Controls.MoveRight))
                {
                    e.ParrentEntity.Facing = Utils.Facing.Right;
                    e.ParrentEntity.Action = MoveActionIndex; 
                    e.ParrentEntity.Variant = MoveRightVariante;
                }

            }
        }

    }
}
