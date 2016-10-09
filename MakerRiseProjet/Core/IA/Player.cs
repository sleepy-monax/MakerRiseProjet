using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiseEngine.Core.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RiseEngine.Core.IA
{
    public class Player : IAbase
    {
        int IdleVariant { get; set; }
        int MoveUpVariante { get; set; }
        int MoveDownVariante { get; set; }
        int MoveLeftVariante { get; set; }
        int MoveRightVariante { get; set; }

        int Speed { get; set; }
        public Player(int _MoveUpVariante, int _MoveDownVariante, int _MoveLeftVariante, int _MoveRightVariante,int _IdleVariant) {
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
            else {
                Speed = 5;
            }

            if (e.ParrentEntity.IAToDo.Count == 0)
            {
                e.ParrentEntity.Variant = IdleVariant;   
                if (KeyBoard.IsKeyDown(Config.Controls.MoveUp))
                {
                    e.ParrentEntity.IAToDo.Add(new Helper.Action(Helper.ActionType.Move, Helper.Direction.Up, Speed));
                    e.ParrentEntity.Variant = MoveUpVariante;
                }
                else if (KeyBoard.IsKeyDown(Config.Controls.MoveDown))
                {
                    e.ParrentEntity.IAToDo.Add(new Helper.Action(Helper.ActionType.Move, Helper.Direction.Down, Speed));
                    e.ParrentEntity.Variant = MoveDownVariante;
                }
                else if (KeyBoard.IsKeyDown(Config.Controls.MoveLeft))
                {
                    e.ParrentEntity.IAToDo.Add(new Helper.Action(Helper.ActionType.Move, Helper.Direction.Left, Speed));
                    e.ParrentEntity.Variant = MoveLeftVariante;
                }
                else if (KeyBoard.IsKeyDown(Config.Controls.MoveRight))
                {
                    e.ParrentEntity.IAToDo.Add(new Helper.Action(Helper.ActionType.Move, Helper.Direction.Right, Speed));
                    e.ParrentEntity.Variant = MoveRightVariante;
                }

            }

        }

    }
}
