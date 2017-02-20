using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.twiyol.Game.WorldDataStruct.Tags
{
    [Serializable]
    public class Tag
    {

        public Tag(object value) {
            Value = value;
        }
        public object Value { get; set; }

    }
}
