namespace Maker.RiseEngine.Core.Storage
{
    public static class FileFormatHelper
    {

        const char CR = (char)0x0D;
        const char LF = (char)0x0A;

        public static string ToDosLineEnd(this string str) {

            str.Replace(CR.ToString(), "");
            str.Replace(LF.ToString(), new string(new char[] {CR, LF }));

            return str;
        }


    }
}
