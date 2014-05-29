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
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dinfo = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                FileInfo[] Files = dinfo.GetFiles("*.xml");
                foreach (FileInfo file in Files)
                {
                    listBox1.Items.Add(file.Name);
                }

                this.label2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (var file in listBox1.Items)
            {
                String xmlFileToBeFormatted = Path.Combine(this.label2.Text, file.ToString());
                String locationOfFormattedXml = "";
                if (this.checkBox1.Checked)
                {
                    locationOfFormattedXml = xmlFileToBeFormatted;
                }
                string xDoc = LoadXml(xmlFileToBeFormatted);
                XDocument formattedDoc = FormatXml(xDoc);

                if (this.label4.Text.Length > 0)
                {
                    locationOfFormattedXml = Path.Combine(this.label4.Text, file.ToString());
                }
                WriteXml(formattedDoc, locationOfFormattedXml);
                //var boo = formattedDoc;
            }
        }

        public string LoadXml(String filename)
        {
            try
            {
                XDocument doc = XDocument.Load(filename);
                return doc.ToString();
            }
            catch (Exception)
            {
                return filename;
            }

        }

        public XDocument FormatXml(String Xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(Xml);
                return doc;
            }
            catch (Exception)
            {
                return new XDocument();
            }
        }
        public void WriteXml(XDocument formattedXml, String locationOfXml)
        {
            try
            {
                formattedXml.Save(locationOfXml);

            }
            catch (Exception)
            {
            }
        }

        private void destinationPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                this.label4.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckState state = checkBox1.CheckState;

            if (state == CheckState.Checked)
            {
                this.destinationPath.Enabled = false;
            }
            else
            {
                this.destinationPath.Enabled = true;
            }
        }
    }
}
