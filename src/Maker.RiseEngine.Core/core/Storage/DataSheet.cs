using Maker.RiseEngine.Core.EngineDebug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Maker.RiseEngine.Core.Storage
{
    public class DataSheet
    {
        string DataSheetPath;
        public Dictionary<string, string> Data = new Dictionary<string, string>();

        public DataSheet(string dataSheetPath)
        {
            DataSheetPath = dataSheetPath;
            DebugLogs.WriteLog($"Load '{dataSheetPath}'", LogType.Info, nameof(DataSheet));

            if (File.Exists(dataSheetPath))
            {
                StreamReader sr = new StreamReader(dataSheetPath);
                string RawText = sr.ReadToEnd().ToDosLineEnd();
                sr.Close();

                string[] Lines = RawText.Replace(Environment.NewLine, "").Split(';');
                foreach (string line in Lines)
                {
                    string[] SubString = line.Split(':');

                    if (SubString.Count() == 2)
                        Data.Add(SubString[0], SubString[1]);
                }
            }
        }

        public void Save()
        {
            DebugLogs.WriteLog("Save '" + DataSheetPath + "'", LogType.Info, "Storage.DataSheet");

            string fileContent = "";

            foreach (KeyValuePair<string, string> key in Data)
            {
                fileContent = $"{fileContent}{key.Key}:{key.Value};{Environment.NewLine}";
            }

            StreamWriter sw = new StreamWriter(DataSheetPath);
            sw.Write(fileContent);
            sw.Close();
        }

        public string GetData(string Key, string DefaultValue = "null")
        {
            if (Data.ContainsKey(Key))
            {
                return Data[Key];

            }
            else {
                Data.Add(Key, DefaultValue);
                return DefaultValue;
            }
        }

        public void SetData(string Key, string Value)
        {
            if (Data.ContainsKey(Key))
            { 
                Data[Key] = Value;
            }
            else
            {

                Data.Add(Key, Value);
            }
        }
    }
}
