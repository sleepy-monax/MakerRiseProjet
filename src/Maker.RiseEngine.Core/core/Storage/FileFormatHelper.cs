namespace Maker.RiseEngine.Core.Storage
{
    public static class FileFormatHelper
    {
        public static string ToDosLineEnd(this string str)
        {
            // Convert LF to CRLF.
            var newString = str.Replace("\r", "").Replace("\n", "\r\n");

            return newString;
        }


    }
}
