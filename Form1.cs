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
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XDocumentTest
{


    // test 2015-10-08

    public partial class Form1 : Form
    {

        const string FILE_NAME = "messages.xml";
        int maxCount = 10;
        Queue<MyMessage> messages;


        public Form1()
        {
            InitializeComponent();

            messages = new Queue<MyMessage>(maxCount);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            SetMessage("button 1 pressed    " + (new Random()).Next());
            PrintMessages();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            SetMessage("button 2 pressed    " + (new Random()).Next());
            PrintMessages();
        }


        void SetMessage(string msg)
        {
            if (messages.Count >= maxCount)
                messages.Dequeue();

            messages.Enqueue(new MyMessage() { Date = DateTime.Now, Text = msg });
        }


        void PrintMessages()
        {
            richTextBox1.Clear();
            int i = 0;
            foreach (MyMessage msg in messages)
            {
                richTextBox1.AppendText(string.Format("{2}\t{0}\t\t{1}\r\n", msg.Date, msg.Text, ++i));
            }
        }


        void SaveMessages()
        {

            try
            {

                var xml = new XElement("messages", messages.Select(x => (
                    new XElement("message",
                    new XElement("text", x.Text),
                    new XElement("datestr", x.Date.ToString("yyyy-MM-dd HH:mm")),
                    new XAttribute("date", x.Date)))));

                //XDocument doc = new XDocument(xml);
                xml.Save(FILE_NAME);


            }
            catch (Exception ex)
            {
                PrintError(ex);
            }

        }


        void LoadMessages(string fileName)
        {
            try
            {
                XDocument doc = XDocument.Load(fileName);

                messages.Clear();
                MyMessage msg;

                foreach (XElement el in doc.Root.Elements())
                {
                    msg = new MyMessage();

                    foreach (XElement child in el.Elements())
                    {
                        if (child.Name == "text")
                        {
                            msg.Text = child.Value; 
                        }
                        else if (child.Name == "datestr")
                        {
                            msg.DateStr = child.Value;
                        }
                    }

                    foreach (XAttribute attr in el.Attributes())
                    {
                        if (attr.Name == "date")
                        {
                            msg.Date = DateTime.Parse(attr.Value);
                        }
                    }

                    messages.Enqueue(msg);

                }

            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }


        void PrintError(Exception ex)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText(ex.Message);
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveMessages();
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadMessages(FILE_NAME);
            PrintMessages();

            ShowData();

        }


        void ShowData()
        {

            try
            {

                System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

                FileInfo finfo = new FileInfo(asm.Location);

                string outputFileName = Path.Combine(finfo.DirectoryName, "report.html");

                XPathDocument xpathDoc = new XPathDocument(FILE_NAME);
                XslCompiledTransform xslt = new XslCompiledTransform();

                finfo = new FileInfo("XSLT1.xslt");
                if (!finfo.Exists)
                    throw new FileNotFoundException();

                xslt.Load(finfo.FullName);

                using (XmlTextWriter writer = new XmlTextWriter(outputFileName, null))
                {
                    xslt.Transform(xpathDoc, null, writer);
                }

                webBrowser1.Url = new Uri(outputFileName);
                webBrowser1.Refresh();

            }
            catch (Exception ex)
            {
                PrintError(ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMessages(FILE_NAME);
            ShowData();
        }


    }

}
