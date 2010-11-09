using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace AutoMusic
{
    static class Tools
    {
        public const string ApplicationKey = @"HKEY_CURRENT_USER\Software\Solymosi\AutoMusic";

        static public string GetRegistry(string Name, string Default)
        {
            return (string)Registry.GetValue(ApplicationKey, Name, (object)Default);
        }
        static public void SetRegistry(string Name, string Value)
        {
            Registry.SetValue(ApplicationKey, Name, (object)Value);
        }
        static public DialogResult Message(string Message, MessageBoxIcon Icon)
        {
            return MessageBox.Show(Message, Application.ProductName, 0, Icon);
        }
        static public DialogResult Question(string Message, MessageBoxIcon Icon)
        {
            return MessageBox.Show(Message, Application.ProductName, MessageBoxButtons.YesNo, Icon);
        }
    }
}
