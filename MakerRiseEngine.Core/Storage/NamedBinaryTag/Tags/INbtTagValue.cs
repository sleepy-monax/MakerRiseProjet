using System;
using System.Collections.Generic;
using System.Text;

namespace Maker.RiseEngine.Core.Storage.NamedBinaryTag.Tags
{
    internal interface INbtTagValue<T>
    {
        T Value { get; set; }
    }
}
