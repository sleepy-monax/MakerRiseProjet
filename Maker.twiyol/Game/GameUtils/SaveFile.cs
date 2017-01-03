using System.IO;

namespace Maker.twiyol.Game.GameUtils
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
