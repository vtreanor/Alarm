using System;
using System.Collections.Generic;
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

        //private void button1_Click(object sender, EventArgs e)
        //{

        //    process1.Start();

        //    // To avoid deadlocks, always read the output stream first and then wait.
        //    string output = process1.StandardOutput.ReadToEnd();
        //    process1.WaitForExit();
        //    //textBox1.Text = output;

        //    System.IO.TextReader rdr = new StringReader(output);
        //    XmlReader reader = XmlReader.Create(rdr);
        //    try
        //    {
        //        while (reader.Read())
        //        {
        //            if (reader.NodeType == XmlNodeType.Element)
        //            {
        //                if (reader.Name == "UserId") { textBox1.Text += reader.ReadString(); textBox1.Text += "\r\n"; }
        //                if (reader.Name == "Date") { textBox1.Text += reader.ReadString(); textBox1.Text += "\r\n"; }
        //                if (reader.Name == "Command") { textBox1.Text += reader.ReadString(); textBox1.Text += "\r\n\r\n"; }

        //            }
        //        }
        //    }
        //    catch (System.Xml.XmlException exc)
        //    {
        //        String str = exc.Message;
        //        Console.WriteLine(str);
        //        //Console.WriteLine(e.Message);
        //        //Console.WriteLine("Exception object Line, pos: (" + e.LineNumber + "," + e.LinePosition + ")");
        //        //Console.WriteLine("XmlReader Line, pos: (" + tr.LineNumber + "," + tr.LinePosition + ")");

        //    }

        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
            
        //    if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        string path = openFileDialog1.FileName;
        //        string xml = File.ReadAllText(path);
                
        //        System.IO.TextReader rdr = new StringReader(xml);
        //        XmlReader reader = XmlReader.Create(rdr);                
        //        try
        //        {
        //            while (reader.Read())
        //            {
        //                if (reader.NodeType == XmlNodeType.Element)
        //                {
        //                    if (reader.Name == "UserId") { textBox1.Text += reader.ReadString(); textBox1.Text += "\r\n"; }
        //                    if (reader.Name == "Date") { textBox1.Text += reader.ReadString(); textBox1.Text += "\r\n"; }
        //                    if (reader.Name == "Command") { textBox1.Text += reader.ReadString(); textBox1.Text += "\r\n\r\n"; }

        //                }
        //            }
        //        }
        //        catch (System.Xml.XmlException exc)
        //        {

        //            String str = exc.Message;
        //            Console.WriteLine(str);
        //            //Console.WriteLine(e.Message);
        //            //Console.WriteLine("Exception object Line, pos: (" + e.LineNumber + "," + e.LinePosition + ")");
        //            //Console.WriteLine("XmlReader Line, pos: (" + tr.LineNumber + "," + tr.LinePosition + ")");

        //        }



        //    }
        
        //}


        private void button1_Click(object sender, EventArgs e)
        {
            Boolean deleteOk = false;
            Boolean createOk = false;
            string output;
            string task = @"c:\users\vincent\beep.bat";
            string process = @"C:\Windows\System32\schtasks.exe";
            string deleteStr = "/Delete /TN Alarm /F ";
            string createStr = "/Create /SC ONCE /TN Alarm /TR " + task + " /ST " + timePicker.Text + " /SD " + datePicker.Text;
            statusBar.Text = "Alarm at " + timePicker.Text + " on " + datePicker.Text; 
            //textBox1.Text += deleteStr;
            //textBox1.Text += "\r\n";
            //textBox1.Text += createStr;
            //textBox1.Text += "\r\n";

            process1.StartInfo.FileName  = process;
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
            statusBar.Text += (deleteOk && createOk) ? " Success" : " Error";
        }
    }
}
