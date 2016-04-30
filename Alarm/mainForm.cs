using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace Alarm
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "HH:mm";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string output;
            string path = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%");
            string task = path + @"\beep.bat";
            if (!File.Exists(task))
            {
                statusBar.Text = task + " not found";
                return;
            }

            Boolean deleteOk = false;
            Boolean createOk = false;


            string process = @"C:\Windows\System32\schtasks.exe";
            string deleteStr = "/Delete /TN Alarm /F ";
            string createStr = "/Create /SC ONCE /TN Alarm /TR " + task + " /ST " + timePicker.Text + " /SD " + datePicker.Text;
            statusBar.Text = "Alarm at " + timePicker.Text + " on " + datePicker.Text;

            //----------------------------------------------------------------------------------
            process1.StartInfo.FileName = process;
            process1.StartInfo.Arguments = deleteStr;

            process1.Start();
            output = process1.StandardOutput.ReadToEnd();
            process1.WaitForExit();
            deleteOk = output.Contains("SUCCESS");
            //----------------------------------------------------------------------------------
            process1.StartInfo.FileName = process;
            process1.StartInfo.Arguments = createStr;

            process1.Start();
            output = process1.StandardOutput.ReadToEnd();
            process1.WaitForExit();
            createOk = output.Contains("SUCCESS");
            //----------------------------------------------------------------------------------

            statusBar.Text += (createOk) ? " Success" : " Error";
        }
    }
}