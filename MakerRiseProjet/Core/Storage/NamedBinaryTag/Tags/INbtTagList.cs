using System;
using System.Collections.Generic;
using System.Text;
using RiseEngine.Core.Storage.NamedBinaryTag.Queries;

namespace RiseEngine.Core.Storage.NamedBinaryTag.Tags
{
    internal interface INbtTagList
    {
        List<NbtTag> Tags { get; }

        T Get<T>(int tagIdx) where T : NbtTag;
    }
}
