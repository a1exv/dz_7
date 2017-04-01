using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace ClassLibrary1
{
    public class Logger
    {
        public enum type { info, warn, error, test };
        type defaultType = type.info;
        public FileInfo ini = new FileInfo(inipath);
        public Logger()
        {
            if (ini.Exists) ;
            else
            {
                ini.Create();
            }
        }
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WritePrivateString(string section, string key, string str, string path);
        private const int SIZE = 1024;
        private static string inipath = "E:/Programms/step/c3/logger.ini";
        public void WritePrivateString(string section, string key, string value)
        {
            WritePrivateString(section, key, value, inipath);
        }
        public string GetPrivateString(string section, string key)
        {
            StringBuilder buffer = new StringBuilder(SIZE);
            GetPrivateString(section, key, null, buffer, SIZE, inipath);
            return buffer.ToString();
        }
        public static string path = "E:/Programms/step/c3/logger.txt";
        public StreamWriter streamlogger = new StreamWriter(path, false);
        public  void logwrite(string message, type t)
        {
            streamlogger.WriteLine(DateTime.Now + "\t" +Environment.UserName+"\t"+ t.ToString() + "\t" + message);

        }
        public void logwrite(string message)
        {
            logwrite(message, defaultType);
        }
        public void Close()
        {
            streamlogger.Close();
        }
        
    }
}
