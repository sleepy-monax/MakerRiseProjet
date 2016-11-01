using RiseEngine.Core.Storage.NamedBinaryTag.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.Storage
{
    public static class NbtHelper
    {

        public static T GetNbtTags<T>(this NbtCompound rootCompound, string path) where T : NbtTag
        {
            //Setup path.
            List<string> subPath = path.Split('.').ToList<string>();
            string tagName = subPath.Last();
            subPath.RemoveAt(subPath.Count - 1);

            //Finding compound.
            NbtCompound currentCompound = rootCompound;

            foreach (string i in subPath)
            {

                NbtTag thisTag = currentCompound.Get(i);

                if (thisTag.GetType() == typeof(NbtCompound))
                {
                    currentCompound = (NbtCompound)thisTag;
                }

            }

            //returs value.
            NbtTag findTags = currentCompound.Get(tagName);
            return (T)findTags;

        }

        public static void SetNbtTags(this NbtCompound rootCompound, string path, NbtTag value)
        {

            //Setup path.
            List<string> subPath = path.Split('.').ToList<string>();

            //Finding compound.
            NbtCompound currentCompound = rootCompound;

            foreach (string i in subPath)
            {
                try
                {
                    NbtTag thisTag = currentCompound.Get(i);

                    if (thisTag.GetType() == typeof(NbtCompound))
                    {
                        currentCompound = (NbtCompound)thisTag;
                    }
                }
                catch (KeyNotFoundException)
                {

                    NbtCompound newCompound = new NbtCompound(i);
                    currentCompound.Set(i, newCompound);
                }
            }
        }


    }
}
