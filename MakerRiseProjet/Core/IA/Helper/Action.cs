using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.IA.Helper
{
    [Serializable]
    public class Action
    {

        public int aSpeed;
        public ActionType aType;
        public Direction aDirection;

        public int aDamage;

        public Action(ActionType Type, Direction _Direction, int _Speed) {

            aType = Type;
            aDirection = _Direction;
            aSpeed = _Speed;

        }

        public Action(Direction _Direction, int _Damage) {

            aType = ActionType.Atack;
            aDirection = _Direction;
            aDamage = _Damage;

        }
    }
}
