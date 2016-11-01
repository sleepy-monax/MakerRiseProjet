using System;
using System.Collections.Generic;
using System.Text;

namespace RiseEngine.Core.Storage.NamedBinaryTag.Tags
{
    internal interface INbtTagValue<T>
    {
        T Value { get; set; }
    }
}
