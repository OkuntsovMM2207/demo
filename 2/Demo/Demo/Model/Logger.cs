using System;
using System.IO;
using System.Windows;

namespace Demo.Model
{
    public static class Logger
    {
        private static readonly string LogFilePath = "logs.txt";

        public static void Log(string message, params object[] args)
        {
            try
            {
                string logMessage = $"{DateTime.Now:G} = {message}{Environment.NewLine}";
                File.AppendAllText(LogFilePath, logMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка записи логов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}