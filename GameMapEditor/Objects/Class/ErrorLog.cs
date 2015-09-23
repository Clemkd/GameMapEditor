using System;
using System.IO;

namespace GameMapEditor.Objects
{
    class ErrorLog
    {
        private const string ERROR_LOG_FILE = "error-log.txt";

        /// <summary>
        /// Écrit dans le fichier de logs, les informations sur l'exception jetée
        /// </summary>
        /// <param name="ex">L'exception jetée</param>
        public static void Write(Exception ex)
        {
            File.AppendAllText(ERROR_LOG_FILE, string.Format("{0}{5}{0}Erreur {1} {2}{0}{3}{0}{4}{0}", Environment.NewLine, ex.HResult, ex.Source, ex.Message, ex.ToString(), DateTime.Now.ToString()));
        }
    }
}
