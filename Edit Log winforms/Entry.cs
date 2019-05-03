using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edit_Log_winforms
{
    public class Entry
    {
        private string _name;
        private int _startTime;
        private int _endTime;
        private int _totalTime;
        private DateTime _date;
        private Boolean _done;

        public Entry(string name, int startTime, int endTime, int totalTime, DateTime date)
        {
            _name = name;
            _startTime = startTime;
            _endTime = endTime;
            _totalTime = totalTime;
            _date = date;
        }

        public Entry(string name, int startTime, int endTime, int totalTime, DateTime date, Boolean done)
        {
            _name = name;
            _startTime = startTime;
            _endTime = endTime;
            _totalTime = totalTime;
            _date = date;
            _done = done;
        }

        public string Name { get => _name; set => _name = value; }
        public int StartTime { get => _startTime; set => _startTime = value; }
        public int EndTime { get => _endTime; set => _endTime = value; }
        public int TotalTime { get => _totalTime; set => _totalTime = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public Boolean Done { get => _done; set => _done = value; }

        public static List<Entry> readEntries()
        {
            List<Entry> Entries = new List<Entry>();
            string[] lines = System.IO.File.ReadAllLines(Program.editLogLocation);
            string prevName = "";
            int startTime;
            int endTime;
            int prevEnd = 0;
            int total;
            foreach (string line in lines)
            {
                if (line == "")
                    break;
                string[] entryInfo = line.Split(' ');
                DateTime date = DateTime.Parse(entryInfo[0]);
                string name = entryInfo[1];
                Boolean done = false;

                if (!prevName.Equals(name))
                {
                    prevEnd = 0;
                }

                try
                {
                    if (entryInfo[4].Equals("done") || entryInfo[3].Equals("done"))
                    {
                        done = true;
                    }
                }
                catch (IndexOutOfRangeException e) { }

                //concatanates the entries containing info about the time spent
                try
                {
                    int[] time = TimesToInt(entryInfo[2].Split(':').Concat(entryInfo[3].Split(':')).ToArray(), line, 4);

                    if(time[2] == 0 && time[3] == 0) {
                        throw new Exceptions.SpaceAtEndException();
                    }
                    //multiply minute counters by 60 to get a second value
                    startTime = time[0] * 60 + time[1];
                    endTime = time[2] * 60 + time[3];
                    prevEnd = endTime;

                    total = Math.Abs(startTime - endTime);
                }
                catch(Exception e) {        
                    int[] time = TimesToInt(entryInfo[2].Split(':').ToArray(), line, 2);

                    startTime = prevEnd;
                    endTime = time[0] * 60 + time[1];
                    total = Math.Abs(startTime - endTime);
                    prevEnd = endTime;
   
                }

                Entries.Add(new Entry(name, startTime, endTime, total, date, done));
                prevName = name;
            }
            return Entries;
        }

        public static int[] TimesToInt(string[] times, string line, int size)
        {
            int[] time = new int[size];

            for (int x = 0; x < times.Length; x++)
            {
                if (Int32.TryParse(times[x], out time[x])) { }

                else
                    Console.WriteLine("a problem occured when parsing " + time[x] + " in the line " + line);
            }

            return time;
        }

    }
}
