using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edit_Log_winforms
{
    static class Program
    {

        /*TODO:
         Create a "is valid function" for new entries, before they are entered
         Allow users to add multiple Entries at once 
         Allow entry names to contain spacing
         Make it so you can view an entry despite it only having one view*/
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
