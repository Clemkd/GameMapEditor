using System;
using System.IO;

namespace GameMapEditor.Objects
{
    class ErrorLog
    {
        private const string ERROR_LOG_FILE = "logs.txt";

        public static void Write(Exception ex)
        {
            File.AppendAllText(ERROR_LOG_FILE, String.Format("{0}{5}{0}Erreur {1} {2}{0}{3}{0}{4}", Environment.NewLine, ex.HResult, ex.Source, ex.Message, ex.ToString(), DateTime.Now.ToString()));
        }
    }
}
