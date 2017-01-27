
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameObject;
using Maker.RiseEngine.Core.Input;
using Maker.twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.twiyol.AI.Entites
{
    public class PlayerAI : AIbase
    {


        int IdleUpVariante { get; set; }
        int IdleDownVariante { get; set; }
        int IdleLeftVariante { get; set; }
        int IdleRightVariante { get; set; }

        int MoveUpVariante { get; set; }
        int MoveDownVariante { get; set; }
        int MoveLeftVariante { get; set; }
        int MoveRightVariante { get; set; }

        int Speed { get; set; }
        public PlayerAI(int _MoveUpVariante, int _MoveDownVariante, int _MoveLeftVariante, int _MoveRightVariante, int _IdleUpVariante, int _IdleDownVariante, int _IdleLeftVariante, int _IdleRightVariante)
        {
            MoveUpVariante = _MoveUpVariante;
            MoveDownVariante = _MoveDownVariante;
            MoveLeftVariante = _MoveLeftVariante;
            MoveRightVariante = _MoveRightVariante;

            IdleUpVariante = _IdleUpVariante;
            IdleDownVariante = _IdleDownVariante;
            IdleLeftVariante = _IdleLeftVariante;
            IdleRightVariante = _IdleRightVariante;

        }

        public override void Tick(GameObjectEventArgs e, GameInput playerInput, GameTime gameTime)
        {
           
            int moveActionIndex = GameObjectManager.GetGameObjectIndex("twiyol_Base.Move");
            int attckActionIndex = GameObjectManager.GetGameObjectIndex("twiyol_Base.Attack");

            if (e.ParrentEntity.Action == -1)
            {
                if (playerInput.IsKeyBoardKeyDown(Engine.engineConfig.Input_MoveUp))
                {
                    e.ParrentEntity.Facing = Utils.Facing.Up;
                    e.ParrentEntity.Action = moveActionIndex;
                    e.ParrentEntity.Variant = MoveUpVariante;
                }
                else if (playerInput.IsKeyBoardKeyDown(Engine.engineConfig.Input_MoveDown))
                {
                    e.ParrentEntity.Facing = Utils.Facing.Down;
                    e.ParrentEntity.Action = moveActionIndex;
                    e.ParrentEntity.Variant = MoveDownVariante;
                }
                else if (playerInput.IsKeyBoardKeyDown(Engine.engineConfig.Input_MoveLeft))
                {
                    e.ParrentEntity.Facing = Utils.Facing.Left;
                    e.ParrentEntity.Action = moveActionIndex;
                    e.ParrentEntity.Variant = MoveLeftVariante;
                }
                else if (playerInput.IsKeyBoardKeyDown(Engine.engineConfig.Input_MoveRight))
                {
                    e.ParrentEntity.Facing = Utils.Facing.Right;
                    e.ParrentEntity.Action = moveActionIndex;
                    e.ParrentEntity.Variant = MoveRightVariante;
                }
                else
                {

                    if (playerInput.IsKeyBoardKeyDown(Engine.engineConfig.Input_Attack))
                    {
                        e.ParrentEntity.Action = attckActionIndex;
                    }

                    switch (e.ParrentEntity.Facing)
                    {
                        case Utils.Facing.Up:
                            e.ParrentEntity.Variant = IdleUpVariante;
                            break;
                        case Utils.Facing.Down:
                            e.ParrentEntity.Variant = IdleDownVariante;
                            break;
                        case Utils.Facing.Left:
                            e.ParrentEntity.Variant = IdleLeftVariante;
                            break;
                        case Utils.Facing.Right:
                            e.ParrentEntity.Variant = IdleRightVariante;
                            break;
                        default:
                            break;
                    }

                }

            }
        }

    }
}
