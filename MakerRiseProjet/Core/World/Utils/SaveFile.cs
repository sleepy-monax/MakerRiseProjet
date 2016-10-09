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

        public void SaveChunk(int x, int y, Obj.ObjChunk _Chunk)
        {

            Debug.Logs.Write("[IO] Saving chunk " + x + "," + y, Debug.LogType.Info);

            List<byte> Chunk = new List<byte>();

            for (int tX = 0; tX <= 15; tX++)
            {
                for (int tY = 0; tY <= 15; tY++)
                {

                    // Localisation
                    Chunk.Add((byte)(tY * 16 + tX));

                    Obj.ObjTile Tile = _Chunk.Tiles[tX, tY];

                    bool AsEntity = Tile.Entity > -1;

                    //Contien une entité ?
                    if (AsEntity)
                    {
                        Chunk.Add(1);
                    }
                    else
                    {
                        Chunk.Add(0);
                    }



                    byte[] TileBytes = Tile.ToBytes();

                    if (AsEntity)
                    {
                        Chunk.AddRange(_Chunk.Entities[Tile.Entity].ToBytes().ToList());
                    }

                    Chunk.AddRange(TileBytes.ToList());



                }
            }

            SaveData("Saves\\" + W.worldProperty.WorldName + "\\Chunk_" + x + "_" + y + ".bytes", Chunk.ToArray());

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
