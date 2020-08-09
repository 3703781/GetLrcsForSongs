using System;
using System.IO;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Ape;

namespace GetLrcsForSongs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GetLrcForm());
        }
        
    }
}
