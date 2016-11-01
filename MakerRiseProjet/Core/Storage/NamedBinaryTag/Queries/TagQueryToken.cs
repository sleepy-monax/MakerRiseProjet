using System;
using System.Collections.Generic;
using System.Text;

namespace RiseEngine.Core.Storage.NamedBinaryTag.Queries
{
    public class TagQueryToken
    {
        public TagQuery Query { get; internal set; }
        public string Name { get; internal set; }
    }
}
