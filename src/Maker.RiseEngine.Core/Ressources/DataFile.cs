using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Ressources
{
    public class DataFile
    {
        public List<string> Entries;
        Dictionary<string, string> Values;
        public string DataType = "none";

        public DataFile(string dataSheetPath)
        {
            Values = new Dictionary<string, string>();
            Entries = new List<string>();

            Debug.WriteLog($"Loading: {dataSheetPath}", LogType.Info, nameof(DataFile));

            if (File.Exists(dataSheetPath))
            {
                // Read content from the data file.
                StreamReader fileStreamReader = new StreamReader(dataSheetPath);
                string fileContent = fileStreamReader.ReadToEnd();
                fileStreamReader.Close();

                fileContent = fileContent.ToDosLineEnd().Replace("\r\n", "");

                string[] fileLines = fileContent.Split(';');
                int lineIndex = 0;
                foreach (string line in fileLines)
                {
                    lineIndex++;
                    if (!line.StartsWith("##"))
                    {
                        string[] Token = line.Split(':');

                        if (Token.Count() == 2)
                        {
                            Entries.Add(Token[0]);
                            Values.Add(Token[0], Token[1].Replace("\"", ""));
                        }
                        else Debug.WriteLog($"Syntaxe error at line n°{lineIndex}", LogType.Warning, nameof(DataFile));
                    }
                }

                if (Values.ContainsKey("DataType"))
                {
                    DataType = Values["DataType"];
                }
                else
                {
                    Debug.WriteLog($"No data type", LogType.Error, nameof(DataFile));
                }
            }
            else
            {
                Debug.WriteLog($"Not found !", LogType.Error, nameof(DataFile));
            }
        }

        public string GetDataAsString(string key, string defaultValue = "none")
        {
            if (Values.ContainsKey(key))
            {
                return Values[key];
            }
            Debug.WriteLog($"key: {key} is not found !", LogType.Warning, nameof(DataFile));
            return defaultValue;
        }

        public int GetDataAsInt(string key, int defaultValue = 0)
        {
            if (Values.ContainsKey(key) && int.TryParse(Values[key], out int i))
            {
                return i;
            }
            Debug.WriteLog($"key: {key} is not found !", LogType.Warning, nameof(DataFile));
            return defaultValue;
        }

        public bool GetDataAsBool(string key, bool defaultValue = false)
        {
            if (Values.ContainsKey(key))
            {
                return Convert.ToBoolean(Values[key]);
            }
            Debug.WriteLog($"key: {key} is not found !", LogType.Warning, nameof(DataFile));
            return defaultValue;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
