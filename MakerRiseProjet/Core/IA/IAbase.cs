
using RiseEngine.Core.World.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.IA
{
    public class IAbase
    {

        public virtual void Tick(GameObject.Event.GameObjectEventArgs e, KeyboardState _KeyBoard, MouseState _Mouse, GameTime _GameTime)
        {
            //do nothing

        }

        public void ExecuteAction(GameObject.Event.GameObjectEventArgs e, GameTime gametime)
        {

            if (e.ParrentEntity.IAToDo.Count > 0)
            {

                Helper.Action a = e.ParrentEntity.IAToDo[0];
                Point pt = new Point(0, 0);
                Vector2 Vc2 = Vector2.Zero;
                e.ParrentEntity.ActionProgress += a.aSpeed;
                float P = e.ParrentEntity.ActionProgress;



                switch (a.aType)
                {
                    case Helper.ActionType.Move:

                        switch (a.aDirection)
                        {
                            case Helper.Direction.Up:

                                pt = new Point(0, -1);
                                Vc2 = new Vector2(0, -P / 100);

                                break;
                            case Helper.Direction.Down:

                                pt = new Point(0, 1);
                                Vc2 = new Vector2(0, P / 100);

                                break;
                            case Helper.Direction.Left:

                                pt = new Point(-1, 0);
                                Vc2 = new Vector2(-P / 100, 0);

                                break;
                            case Helper.Direction.Right:

                                pt = new Point(1, 0);
                                Vc2 = new Vector2(P / 100, 0);

                                break;
                            default:
                                break;
                        }

                        if (!(e.World.entityManager.TileIsFree(Location.AddPoint(e.CurrentLocation, pt))))
                        {
                            e.ParrentEntity.IAToDo.Remove(a);
                            e.ParrentEntity.ActionProgress = 0;
                            e.ParrentEntity.OnTileLocation = Vector2.Zero;
                        }
                        else
                        {

                            
                            if (e.ParrentEntity.ActionProgress == 100)
                            {
                                e.ParrentEntity.ActionProgress = 0;
                                e.ParrentEntity.OnTileLocation = Vector2.Zero;
                                e.World.entityManager.MoveEntity(e.CurrentLocation, Location.AddPoint(e.CurrentLocation, pt));
                                e.ParrentEntity.IAToDo.Remove(a);

                                GameObjectsManager.Tiles[e.ParrentTile.ID].OnEntityWalkIn(e, gametime);

                               
                            }
                            else
                            {

                                e.ParrentEntity.OnTileLocation = Vc2;
                            }

                            if (e.ParrentEntity.IsFocus)
                            {
                                e.World.Camera.FocusLocation = e.ParrentEntity.Location.ToPoint();
                                e.World.Camera.PreciseFocusLocation = e.ParrentEntity.OnTileLocation;
                            }

                        }
                        break;
                    case Helper.ActionType.Atack:
                        break;
                    case Helper.ActionType.Destroy:
                        break;
                    default:
                        break;
                }



                




            }

        }

    }
}
