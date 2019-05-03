using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edit_Log_winforms
{
    static class Program
    {
        public static string editLogLocation = @"C:\Users\Dick Kickem\Documents\MEGA\Videos\editlog.txt";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<Entry> Entries = Entry.ReadEntries();
            List<Video> Videos = Video.FindVideos(Entries);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(Videos, Entries));
        }
    }
}
