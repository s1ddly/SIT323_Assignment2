using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Ass2;
using System.Net;
using System.Windows.Forms;

namespace App1
{
    public partial class Form1 : Form
    {
        public string config = "";
        public string output = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string srcfile = textBox1.Text;
                HttpWebRequest getdoc = (HttpWebRequest)WebRequest.Create(srcfile);
                Stream Response = getdoc.GetResponse().GetResponseStream();
                StreamReader ResponseReader = new StreamReader(Response);
                config = ResponseReader.ReadToEnd();
                //WCF code below
                Service1Client wcfout = new Service1Client();
                //MessageBox.Show(wcfout.Heuristicout(config));
                output = wcfout.Greedyout(config) + wcfout.Heuristicout(config) + wcfout.Heuristicout2(config);
                wcfout.Close();
                //end WCF call
                textBox2.Text += output;
                MessageBox.Show("URL Parsed Successfully! Check the Processing output TAB");
                //make wcf call here
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Parsing Testbox contents: " + ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }
}
