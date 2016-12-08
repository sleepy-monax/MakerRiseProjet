using System.IO;

namespace Maker.RiseEngine.Core.Game.GameUtils
{
    public class SaveFile
    {
        GameScene G;

        public SaveFile(GameScene _WorldScene)
        {
            G = _WorldScene;
        }

        public void SaveAll()
        {

        }

        public void SaveChunk(int x, int y, World.ObjChunk _Chunk)
        {

            EngineDebug.DebugLogs.WriteInLogs("Saving chunk " + x + "," + y, EngineDebug.LogType.Info, "IO");



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
