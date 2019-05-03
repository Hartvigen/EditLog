using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Edit_Log_winforms
{
    public partial class EntryForm : Form
    {
        Video vid;
        public EntryForm(Video vidSend)
        {
            vid = vidSend;
            InitializeComponent();
            label1.Text = "Editing date";
            label2.Text = "Video Name";
            label3.Text = "Start time";
            label4.Text = "End Time";
            textBox1.Text = vid.Name;
            button1.Text = "Create";

            int curEndTimeMin = vid.Entries[vid.Entries.Count - 1].EndTime / 60;
            int curEndTimeSec = vid.Entries[vid.Entries.Count - 1].EndTime % 60;
            textBox2.Text = curEndTimeMin + ":" + curEndTimeSec;
            textBox3.Text = curEndTimeMin + ":" + curEndTimeSec;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newEntry = dateTimePicker1.Value.Date.ToString("MM/dd/yyyy") + " " + 
                              textBox1.Text + " " + 
                              textBox2.Text + " " +
                              textBox3.Text;
            try
            {
                File.AppendAllText(Program.editLogLocation, Environment.NewLine + newEntry);
            }
            catch(Exception)
            {
                MessageBox.Show("Error! Couldn't create entry, please try again");
            }
            this.Close();
        }
    }
}
