using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseEngine.Core.World.Utils
{
    public class SaveFile
    {
        WorldScene W;

        public SaveFile(WorldScene _WorldScene)
        {
            W = _WorldScene;
        }

        public void SaveAll()
        {

        }

        public void SaveChunk(int x, int y, WorldObj.ObjChunk _Chunk)
        {

            Debug.DebugLogs.WriteInLogs("[IO] Saving chunk " + x + "," + y, Debug.LogType.Info);



        }



        public void LoadChunk()
        {

        }



        protected bool SaveData(string FileName, byte[] Data)
        {
            BinaryWriter Writer = null;
            string Name = FileName;

            try
            {
                // Create a new stream to write to the file
                Writer = new BinaryWriter(File.OpenWrite(Name));

                // Writer raw data                
                Writer.Write(Data);
                Writer.Flush();
                Writer.Close();
            }
            catch
            {
                //...
                return false;
            }

            return true;
        }

    }
}
