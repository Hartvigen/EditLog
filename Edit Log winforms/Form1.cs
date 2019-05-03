using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Edit_Log_winforms
{
    public partial class Form1 : Form
    {

        Video video;
        
        
        public Form1(List<Video> videos, List<Entry> entries)
        {
            InitializeComponent();
            comboBox1.DataSource = videos;
            comboBox1.DisplayMember = "Name";
            label1.Text = "Total Time";
            label2.Text = "Average Time";
            button1.Text = "New Entry";
            button2.Text = "Choose Log";
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox3.BackColor = Color.LightGray;
            textBox3.Text = Program.editLogLocation;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            video = comboBox1.SelectedItem as Video;

            var s = new Series();
            s.ChartType = SeriesChartType.Line;
            s.Name = "Editing time";
            foreach(Entry entry in video.Entries)
            {
                s.Points.AddXY(entry.Date, entry.TotalTime);
            }

            chart1.Series.Clear();
            chart1.Series.Add(s);

            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MM-dd";
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
            chart1.ChartAreas[0].AxisX.IntervalOffset = 1;

            chart1.Series[0].XValueType = ChartValueType.DateTime;
            DateTime minDate = video.Entries[0].Date;
            DateTime maxDate = video.Entries[video.Entries.Count - 1].Date.AddSeconds(-1);  // or DateTime.Now;
            chart1.ChartAreas[0].AxisX.Minimum = minDate.ToOADate();
            chart1.ChartAreas[0].AxisX.Maximum = maxDate.ToOADate();

            int tot = video.Total % 60;
            double avg = video.Average/60;
            double avgSeconds = video.Average % 60;

            textBox1.Text = video.Total/60 + ":" + tot.ToString("00");
            if(avg >= 1)
                textBox2.Text = avg.ToString() + ":" + avgSeconds.ToString();
            else
                textBox2.Text = video.Average.ToString("00");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var formPopup = new EntryForm(video);
            formPopup.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
        }
    }
}
