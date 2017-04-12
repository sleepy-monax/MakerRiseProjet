using Microsoft.Xna.Framework;

namespace Maker.twiyol.AI
{
    public enum Facing
    {
        Up, Down, Left, Right
    }

    public static class Utils
    {


        public static Point ToPoint(this Facing _Facing)
        {
            Point pt = Point.Zero;

            switch (_Facing)
            {
                case Facing.Up:

                    pt = new Point(0, -1);

                    break;
                case Facing.Down:

                    pt = new Point(0, 1);

                    break;
                case Facing.Left:

                    pt = new Point(-1, 0);

                    break;
                case Facing.Right:

                    pt = new Point(1, 0);

                    break;
                default:
                    break;
            }

            return pt;
        }

        public static Vector2 ToVector2(this Facing _Facing, float ActionProgress) {
            Vector2 Vc2 = Vector2.Zero;

            switch (_Facing)
            {
                case Facing.Up:

                    Vc2 = new Vector2(0, -(ActionProgress / 100));

                    break;
                case Facing.Down:

                    Vc2 = new Vector2(0, ActionProgress / 100);

                    break;
                case Facing.Left:

                    Vc2 = new Vector2(-(ActionProgress / 100), 0);

                    break;
                case Facing.Right:

                    Vc2 = new Vector2(ActionProgress / 100, 0);

                    break;
                default:
                    break;
            }

            return Vc2;
        }

    }
}
