using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edit_Log_winforms
{
    public class Video
    {
        private String _name;
        private List<Entry> _entries;
        private int _total;
        private Boolean _done;
        private double _average;

        public Video(string name, List<Entry> entries, int total, bool done)
        {
            _name = name;
            _entries = entries;
            _total = total;
            _done = done;
            _average = total / entries.Count;
        }

        public string Name { get => _name; set => _name = value; }
        public int Total { get => _total; set => _total = value; }
        public bool Done { get => _done; set => _done = value; }
        public List<Entry> Entries { get => _entries; set => _entries = value; }
        public double Average { get => _average; set => _average = value; }


        public static List<Video> FindVideos(List<Entry> entries)
        {
            List<Video> videos = new List<Video>();
            List<string> videoNames = new List<string>();
            double Average;

            //first we find the names of all videos in the list
            foreach (Entry e in entries)
            {
                if (!videoNames.Contains(e.Name))
                    videoNames.Add(e.Name);
            }

            foreach (string vidName in videoNames)
            {
                List<Entry> EntriesWithName = new List<Entry>();
                int total = 0;
                Boolean done = false;
                foreach (Entry e in entries)
                {
                    if (e.Name.Equals(vidName))
                    {
                        EntriesWithName.Add(e);
                        total += e.TotalTime;
                        if (e.Done == true)
                            done = true;
                    }
                }
                videos.Add(new Video(vidName, EntriesWithName, total, done));
            }

            return videos;

        }
    }
}
