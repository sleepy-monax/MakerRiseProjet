using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.RiseEngine.Storage
{
    public static class ListSheet
    {
        /// <summary>
        /// Parse a list sheet file into a list of string.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <returns></returns>
        public static List<string> ParseListSheet(string path)
        {
            Debug.WriteLog("Load '" + path + "'", LogType.Info, "Storage.ListSheetParser");

            //Check if the fille existe
            if (System.IO.File.Exists(path))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(path);
                string RawText = sr.ReadToEnd().ToDosLineEnd();
                sr.Close();

                string[] Lines = RawText.Replace(Environment.NewLine, "").Split(';');
                List<string> strings = new List<string>();
                strings.AddRange(Lines);

                return strings;
            }
            else return new List<string>();
        }

    }
}
