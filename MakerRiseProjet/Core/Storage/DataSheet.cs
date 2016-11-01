using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiseEngine.Core.Storage
{
    public class DataSheet
    {

        string sheetPath;

        public Dictionary<string, string> Data = new Dictionary<string, string>();

        public DataSheet(string _Path)
        {
            sheetPath = _Path;
        }

        public void Load()
        {

            Debug.DebugLogs.WriteInLogs("[Storage.DataSheet] Load '" + sheetPath + "'", Debug.LogType.Info);

            //Check if the fille existe
            if (System.IO.File.Exists(sheetPath))
            {

                //Reading Sheet from the file
                System.IO.StreamReader sr = new System.IO.StreamReader(sheetPath);
                string RawText = sr.ReadToEnd();
                sr.Close();

                //parse file
                string[] Lines = RawText.Replace(Environment.NewLine, "").Split(';');

                foreach (string line in Lines)
                {

                    string[] SubString = line.Split(':');

                    if (SubString.Count() == 2)
                    {

                        Data.Add(SubString[0], SubString[1]);

                    }
                }
            }
        }

        public void Save()
        {

            Debug.DebugLogs.WriteInLogs("[Storage.DataSheet] Save '" + sheetPath + "'", Debug.LogType.Info);

            string FileText = "";

            foreach (KeyValuePair<string, string> key in Data)
            {
                FileText = FileText + key.Key + ":" + key.Value + ";" + Environment.NewLine;
            }

            System.IO.StreamWriter sw = new System.IO.StreamWriter(sheetPath);
            sw.Write(FileText);
            sw.Close();
        }

        public string GetData(string Key, string DefaultValue)
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
            if (Data.ContainsKey(Key)) {

                Data[Key] = Value;

            } else {

            Data.Add(Key, Value);
            }
        }

    }
}
