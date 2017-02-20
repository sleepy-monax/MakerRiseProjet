
using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.GameComponent;
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
           
            int moveActionIndex = GameComponentManager.GetGameObjectIndex("TWIYOL_Base.Move");
            int attackActionIndex = GameComponentManager.GetGameObjectIndex("TWIYOL_Base.Attack");

            if (e.ParrentEntity.Tags.GetTag("ai_action", -1) == -1)
            {
                if (playerInput.IsKeyBoardKeyDown(Engine.engineConfig.Input_MoveUp))
                {
                    e.ParrentEntity.Tags.SetTag("facing", Facing.Up);
                    e.ParrentEntity.Tags.SetTag("ai_action", moveActionIndex);
                    e.ParrentEntity.Variant = MoveUpVariante;
                }
                else if (playerInput.IsKeyBoardKeyDown(Engine.engineConfig.Input_MoveDown))
                {
                    e.ParrentEntity.Tags.SetTag("facing", Facing.Down);
                    e.ParrentEntity.Tags.SetTag("ai_action", moveActionIndex);
                    e.ParrentEntity.Variant = MoveDownVariante;
                }
                else if (playerInput.IsKeyBoardKeyDown(Engine.engineConfig.Input_MoveLeft))
                {
                    e.ParrentEntity.Tags.SetTag("facing", Facing.Left);
                    e.ParrentEntity.Tags.SetTag("ai_action", moveActionIndex);
                    e.ParrentEntity.Variant = MoveLeftVariante;
                }
                else if (playerInput.IsKeyBoardKeyDown(Engine.engineConfig.Input_MoveRight))
                {
                    e.ParrentEntity.Tags.SetTag("facing", Facing.Right);
                    e.ParrentEntity.Tags.SetTag("ai_action", moveActionIndex);
                    e.ParrentEntity.Variant = MoveRightVariante;
                }
                else
                {

                    if (playerInput.IsKeyBoardKeyPress(Engine.engineConfig.Input_Attack))
                    {
                        e.ParrentEntity.Tags.SetTag("ai_action", attackActionIndex);
                    }

                    switch (e.ParrentEntity.Tags.GetTag("facing", Facing.Down))
                    {
                        case Facing.Up:
                            e.ParrentEntity.Variant = IdleUpVariante;
                            break;
                        case Facing.Down:
                            e.ParrentEntity.Variant = IdleDownVariante;
                            break;
                        case Facing.Left:
                            e.ParrentEntity.Variant = IdleLeftVariante;
                            break;
                        case Facing.Right:
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
