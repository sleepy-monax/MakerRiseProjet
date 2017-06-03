using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace Maker.RiseEngine.Core.Storage
{
    public static class SerializationHelper
    {
        public static void SaveToBin(object obj, string path)
        {

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, obj);
            stream.Close();

        }

        public static T LoadFromBin<T>(string path)
        {

            BinaryFormatter formatter = new BinaryFormatter();
            formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
            //formatter.Binder = new VersionConfigToNamespaceAssemblyObjectBinder();

            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            T obj = (T)formatter.Deserialize(stream);
            stream.Close();

            return obj;
        }
    }

    internal sealed class VersionConfigToNamespaceAssemblyObjectBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            Type typeToDeserialize = null;

            try
            {
                string ToAssemblyName = assemblyName.Split(',')[0];
                Assembly[] Assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
                foreach (Assembly ass in Assemblies)
                    if (ass.FullName.Split(',')[0] == ToAssemblyName)
                    {
                        typeToDeserialize = ass.GetType(typeName);
                        break;
                    }
            }
            catch (Exception exception) { throw exception; }

            return typeToDeserialize;
        }
    }
}
