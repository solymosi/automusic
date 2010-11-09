using System;
using System.Windows.Forms;
using System.Security;
using System.IO;

namespace AutoMusic
{
    static class Program
    {
        [STAThread]
        static void Main(string[] CommandLine)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                string SecurityTest = Environment.MachineName;
            }
            catch
            {
                Tools.Message("AutoMusic can only run from a trusted location. Check the Properties panel and make sure it is on a local drive and is not being blocked!", MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (!File.Exists(FMOD.VERSION.dll + ".dll")) { throw new FileNotFoundException(); }
            }
            catch
            {
                Tools.Message("Could not load the FMOD sound system. Make sure that " + FMOD.VERSION.dll + ".dll is present on your system!", MessageBoxIcon.Error);
                return;
            }
            Global.Context = new Context(CommandLine);
            //try
            //{
                Application.Run(Global.Context);
            //}
            //catch(Exception e)
            //{
            //    try
            //    {
            //        if (Playlist.Active != null)
            //        {
            //            foreach (Track Track in Playlist.Active.Tracks) { Track.Reset(); }
            //        }
            //    }
            //    catch { }
            //    MessageBox.Show("An unexpected error has occured which requires AutoMusic to close.\r\nWe are sorry for the inconvenience. Please report the error to Solymosi Software!\r\n\r\nError details: " + e.Message, Application.ProductName, 0, MessageBoxIcon.Error);
            //    Environment.Exit(1);
            //}
        }
    }
}