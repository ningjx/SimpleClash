using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleClash.Helpers
{
    public static class FileHelper
    {
        private static Regex DicRegex = new Regex("^(.+)/$");
        public static string Read(string path)
        {
            if (!File.Exists(path))
                return null;

            return File.ReadAllText(path);
        }

        public static void Write(string path, string data)
        {
            var dicPath = Path.GetDirectoryName(path);

            if (!Directory.Exists(dicPath))
                Directory.CreateDirectory(dicPath);

            File.WriteAllText(path, data);
        }

        public static void Create(string path)
        {

        }

        public static int GetHashCode(string path)
        {
            return File.ReadAllBytes(path).GetHashCode();
        }

        public static void DownloadFile(string url,string path)
        {
            var stream = HttpHelper.GetStream(url).Data;
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            File.WriteAllBytes(path, bytes);
        }
    }
}
